using Plugin.DefaultHttpClientFactory.Shared;
using Plugin.DefaultHttpClientFactory.Shared.Abstractions;
using Plugin.DefaultHttpClientFactory.Shared.Extensions;
using Plugin.DefaultHttpClientFactory.Shared.HttpPolly;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Plugin.DefaultHttpClientFactory
{
    /// <summary>
    /// Default HttpClientFactory
    /// </summary>
    internal class DefaultHttpClientFactory : IDefaultHttpClientFactory
    {
        private ISocketsHttpHandlerFactory _httpHandlerFactory;
        private IDictionary<string, HttpMessageHandler> _storeHttpMessageHandler;
        private IDefaultHttpClientFactory Instance => this;

        internal DefaultHttpClientFactory(ISocketsHttpHandlerFactory socketsHttpHandlerFactory)
        {
            _httpHandlerFactory = socketsHttpHandlerFactory;
            _storeHttpMessageHandler = new Dictionary<string, HttpMessageHandler>(StringComparer.InvariantCultureIgnoreCase);
        }

        public HttpClient Create()
        {
            var primary = Instance.GetOrInstantiateHttpMessageHanlder();
            return Instance.InstantiateHttpClient(primary);
        }

        public HttpClient Create(string clientName)
        {
            var primary = Instance.GetOrInstantiateHttpMessageHanlder(clientName);
            return Instance.InstantiateHttpClient(primary);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientName"></param>
        /// <returns></returns>
        public IDefaultHttpClientBuilder CreateBuilder(string clientName)
        {
            return new DefaultHttpClientBuilder(this, clientName);
        }

        HttpClient IDefaultHttpClientFactory.Create(string clientName, Func<IAsyncPolicy<HttpResponseMessage>, IAsyncPolicy<HttpResponseMessage>> policyFactory, Func<HttpMessageHandler, HttpMessageHandler> pipelineFactory, Func<HttpClient, HttpClient> httpClientFactory)
        {
            var pipeline = Instance.GetOrInstantiateHttpMessageHanlder(clientName, pipelineFactory, policyFactory);
            var httpClient = Instance.InstantiateHttpClient(pipeline);
            return httpClientFactory?.Invoke(httpClient) is HttpClient httpClientChanged ? httpClientChanged : httpClient;
        }

        DelegatingHandler IDefaultHttpClientFactory.GetResilientHandler(Func<IAsyncPolicy<HttpResponseMessage>, IAsyncPolicy<HttpResponseMessage>> policyFactory = null)
        {
            return new PolicyHttpMessageHandler(Instance.GetResilientPolicy(policyFactory));
        }

        IAsyncPolicy<HttpResponseMessage> IDefaultHttpClientFactory.GetResilientPolicy(Func<IAsyncPolicy<HttpResponseMessage>, IAsyncPolicy<HttpResponseMessage>> policyFactory = null)
        {
            //var authEnsuringPolicy = HttpPolicyExtensions
            //    .HandleTransientHttpError()
            //    .OrResult(r => r.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            //    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), onRetryAsync: async (e, i, c) => await Task.Delay(TimeSpan.FromSeconds(5)),);

            var commonNetworkResilience = HttpPolicyExtensions
                                            .HandleTransientHttpError()
                                            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                                            .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            if (policyFactory != null)
                return policyFactory.Invoke(commonNetworkResilience);
            else
                return commonNetworkResilience;

        }

        HttpClient IDefaultHttpClientFactory.InstantiateHttpClient(HttpMessageHandler httpMessageHandler)
            => new HttpClient(httpMessageHandler, disposeHandler: false);

        HttpMessageHandler IDefaultHttpClientFactory.GetOrInstantiateHttpMessageHanlder(string key = "default", Func<HttpMessageHandler, HttpMessageHandler> pipelineFactory = null, Func<IAsyncPolicy<HttpResponseMessage>, IAsyncPolicy<HttpResponseMessage>> policyFactory = null)
        {
            if (_storeHttpMessageHandler.TryGetValue(key, out var recovered))
                return recovered;
            else
            {
                var socketsHttpHandlerBuilder = _httpHandlerFactory.Create();

                //we are decorating the primary message handler with a retry policy HandleTransientHttpError
                var retryHanlder = Instance.GetResilientHandler();
                var newOne = socketsHttpHandlerBuilder
                                .SetPooledConnectionLifetime(TimeSpan.FromMinutes(15))
                                .Build()
                                .DecorateWith(retryHanlder);

                if(pipelineFactory?.Invoke(newOne) is HttpMessageHandler pipeline)
                    _storeHttpMessageHandler.Add(key, pipeline);
                else
                    _storeHttpMessageHandler.Add(key, newOne);
                return newOne;
            }
        }
    }
}
