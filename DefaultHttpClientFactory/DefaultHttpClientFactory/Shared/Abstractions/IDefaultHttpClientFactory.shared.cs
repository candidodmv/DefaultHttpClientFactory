using System;
using System.Net.Http;

namespace Plugin.DefaultHttpClientFactory
{
    public interface IDefaultHttpClientFactory
    {
        HttpClient Create();
        HttpClient Create(string clientName);
        HttpClient Create(string clientName, Func<HttpClient, HttpClient> httpClientFactory);
        HttpClient Create(string clientName, Func<HttpMessageHandler, HttpMessageHandler> pipelineFactory, Func<HttpClient, HttpClient> httpClientFactory = null);
    }
}
