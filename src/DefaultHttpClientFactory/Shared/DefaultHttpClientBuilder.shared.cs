using Plugin.DefaultHttpClientFactory.Shared.Abstractions;
using Polly;
using System;
using System.Net.Http;

namespace Plugin.DefaultHttpClientFactory.Shared
{
    internal class DefaultHttpClientBuilder : IDefaultHttpClientBuilder
    {
        private readonly IDefaultHttpClientFactory _defaultHttpClientFactory;
        private readonly string _httpClientName;
        private Func<IAsyncPolicy<HttpResponseMessage>, IAsyncPolicy<HttpResponseMessage>> _policyPipelineFactory;
        private Func<HttpClient, HttpClient> _httpClientConfigurator;
        private Func<HttpMessageHandler, HttpMessageHandler> _messageHandlerPipelineFactory;

        public DefaultHttpClientBuilder(IDefaultHttpClientFactory defaultHttpClientFactory, string httpClientName)
        {
            if (string.IsNullOrWhiteSpace(_httpClientName)) throw new ArgumentNullException(nameof(httpClientName));
            _defaultHttpClientFactory = defaultHttpClientFactory;
            _httpClientName = httpClientName;
        }

        public HttpClient Build()
        {
            return _defaultHttpClientFactory.Create(_httpClientName, _policyPipelineFactory, _messageHandlerPipelineFactory, _httpClientConfigurator);
        }

        public IDefaultHttpClientBuilder ConfigureExecutionPolicy(Func<IAsyncPolicy<HttpResponseMessage>, IAsyncPolicy<HttpResponseMessage>> pipelineFactory)
        {
            _policyPipelineFactory = pipelineFactory;
            return this;
        }

        public IDefaultHttpClientBuilder ConfigureHttpClient(Func<HttpClient, HttpClient> httpClientConfigurator)
        {
            _httpClientConfigurator = httpClientConfigurator;
            return this;
        }

        public IDefaultHttpClientBuilder ConfigureMessageHandlers(Func<HttpMessageHandler, HttpMessageHandler> pipelineFactory)
        {
            _messageHandlerPipelineFactory = pipelineFactory;
            return this;
        }
    }
}
