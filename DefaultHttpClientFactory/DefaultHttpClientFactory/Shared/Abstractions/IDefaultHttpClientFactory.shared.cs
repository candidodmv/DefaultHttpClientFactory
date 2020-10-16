using Plugin.DefaultHttpClientFactory.Shared.Abstractions;
using Polly;
using System;
using System.Net.Http;

namespace Plugin.DefaultHttpClientFactory
{
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
    public interface IDefaultHttpClientFactory
    {
        /// <summary>
        /// Create a new HttpClient
        /// </summary>
        /// <returns></returns>
        HttpClient Create();

        /// <summary>
        /// Create a new named HttpClient
        /// </summary>
        /// <param name="clientName">The name of the client</param>
        /// <returns></returns>
        HttpClient Create(string clientName);

        /// <summary>
        /// Create a new HttpClientBuilder
        /// </summary>
        /// <param name="clientName">The new httpClient Name</param>
        /// <returns></returns>
        IDefaultHttpClientBuilder CreateBuilder(string clientName);

        internal HttpClient Create(string clientName, Func<IAsyncPolicy<HttpResponseMessage>, IAsyncPolicy<HttpResponseMessage>> policyFactory, Func<HttpMessageHandler, HttpMessageHandler> pipelineFactory = null, Func<HttpClient, HttpClient> httpClientFactory = null);
    }
}
