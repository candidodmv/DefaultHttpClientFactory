using Plugin.DefaultHttpClientFactory.Shared.Extensions;
using Plugin.DefaultHttpClientFactory.Shared.HttpPolly;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Plugin.DefaultHttpClientFactory
{
    public class DefaultHttpClientFactory : IDefaultHttpClientFactory
    {
        private ISocketsHttpHandlerFactory _httpHandlerFactoryLazy;
        private IDictionary<string, HttpMessageHandler> _storePrimaryHttpMessageHandler;


        /// <summary>
        /// It's part of best practicles implemented on ASP.NET Core as described/discussed on the following referenes bellow.
        /// Use IHttpClientFactory to implement resilient HTTP requests
        ///     * https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
        /// 
        /// Using IHttpClientFactory in a DI-enabled app avoids:
        ///     * Resource exhaustion problems by pooling HttpMessageHandler instances.
        ///     * Stale DNS problems by cycling HttpMessageHandler instances at regular intervals.
        ///     
        /// There are alternative ways to solve the preceding problems using a long-lived SocketsHttpHandler instance.
        ///     * Create an instance of SocketsHttpHandler when the app starts and use it for the life of the app.
        ///     * Configure PooledConnectionLifetime to an appropriate value based on DNS refresh times.
        ///     * Create HttpClient instances using new HttpClient(handler, disposeHandler: false) as needed.
        ///     
        /// The preceding approaches solve the resource management problems that IHttpClientFactory solves in a similar way.
        ///     * The SocketsHttpHandler shares connections across HttpClient instances.This sharing prevents socket exhaustion.
        ///     * The SocketsHttpHandler cycles connections according to PooledConnectionLifetime to avoid stale DNS problems.
        ///    
        /// Github:
        ///     1) Make DefaultHttpClientFactory not depend on MS DI #148
        ///         * https://github.com/aspnet/HttpClientFactory/issues/148
        ///     2) Using HttpClientFactory without dependency injection #1345
        ///         * https://github.com/dotnet/extensions/issues/1345
        ///         
        /// Benefits of using IHttpClientFactory 
        ///     * https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests#benefits-of-using-ihttpclientfactory
        /// The current implementation of IHttpClientFactory, that also implements IHttpMessageHandlerFactory, offers the following benefits:
        /// 
        /// * Provides a central location for naming and configuring logical HttpClient objects. For example, you may configure a client (Service Agent) that's pre-configured to access a specific microservice.
        /// * Codify the concept of outgoing middleware via delegating handlers in HttpClient and implementing Polly-based middleware to take advantage of Polly's policies for resiliency.
        /// * HttpClient already has the concept of delegating handlers that could be linked together for outgoing HTTP requests.You can register HTTP clients into the factory and you can use a Polly handler to use Polly policies for Retry, CircuitBreakers, and so on.
        /// * Manage the lifetime of HttpMessageHandler to avoid the mentioned problems/issues that can occur when managing HttpClient lifetimes yourself.
        /// 
        /// The HttpClient instances injected by DI, can be disposed of safely, because the associated HttpMessageHandler is managed by the factory. 
        /// As a matter of fact, injected HttpClient instances are Scoped from a DI perspective.
        /// Detailed documented solution: Microsoft Docs - Alternatives to IHttpClientFactory
        /// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-3.1#alternatives-to-ihttpclientfactory
        /// 
        /// SocketsHttpHandler source code
        /// https://github.com/dotnet/corefx/blob/master/src/System.Net.Http/src/System/Net/Http/SocketsHttpHandler/SocketsHttpHandler.cs
        /// </summary>
        public DefaultHttpClientFactory(ISocketsHttpHandlerFactory socketsHttpHandlerFactory)
        {
            _httpHandlerFactoryLazy = socketsHttpHandlerFactory;
            _storePrimaryHttpMessageHandler = new Dictionary<string, HttpMessageHandler>(StringComparer.InvariantCultureIgnoreCase);
        }

        public HttpClient Create()
        {
            var primary = GetOrInstantiatePrimaryHttpMessageHanlder();
            return InstantiateHttpClient(primary);
        }

        public HttpClient Create(string clientName)
        {
            var primary = GetOrInstantiatePrimaryHttpMessageHanlder(clientName);
            return InstantiateHttpClient(primary);
        }

        public HttpClient Create(string clientName, Func<HttpClient, HttpClient> httpClientFactory)
        {
            var primary = GetOrInstantiatePrimaryHttpMessageHanlder(clientName);
            return httpClientFactory.Invoke(InstantiateHttpClient(primary));
        }

        public HttpClient Create(string clientName, Func<HttpMessageHandler, HttpMessageHandler> pipelineFactory, Func<HttpClient, HttpClient> httpClientFactory = null)
        {
            var pipeline = GetOrInstantiatePrimaryHttpMessageHanlder(clientName, pipelineFactory);
            var httpClient = InstantiateHttpClient(pipeline);
            return httpClientFactory?.Invoke(httpClient) is HttpClient httpClientChanged ? httpClientChanged : httpClient;
        }

        private DelegatingHandler GetRetryHandler() =>
            new PolicyHttpMessageHandler(GetRetryPolicy());

        private IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        private HttpClient InstantiateHttpClient(HttpMessageHandler httpMessageHandler)
            => new HttpClient(httpMessageHandler, disposeHandler: false);

        /// <summary>
        /// It's a memory cache of HttpMessageHandler, here we are storing the entire pipeline for a give HttpClient based on given name
        /// </summary>
        /// <param name="key">The key that represent the HtttpClient</param>
        /// <param name="pipelineFactory">A factory method to build customs pipeline</param>
        /// <returns></returns>
        private HttpMessageHandler GetOrInstantiatePrimaryHttpMessageHanlder(string key = "default", Func<HttpMessageHandler, HttpMessageHandler> pipelineFactory = null)
        {
            if (_storePrimaryHttpMessageHandler.TryGetValue(key, out var recovered))
                return recovered;
            else
            {
                var socketsHttpHandlerBuilder = _httpHandlerFactoryLazy.Create();

                //we are decorating the primary message handler with a retry policy HandleTransientHttpError
                var retryHanlder = GetRetryHandler();
                var newOne = socketsHttpHandlerBuilder
                                .SetPooledConnectionLifetime(TimeSpan.FromMinutes(15))
                                .Build()
                                .DecorateWith(retryHanlder);

                if(pipelineFactory?.Invoke(newOne) is HttpMessageHandler pipeline)
                    _storePrimaryHttpMessageHandler.Add(key, pipeline);
                else
                    _storePrimaryHttpMessageHandler.Add(key, newOne);
                return newOne;
            }
        }
    }
}
