using System;
using System.Net.Http;

namespace Plugin.DefaultHttpClientFactory
{
    /// <summary>
    /// Default HttpClientFactory
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
        /// Create a new named HttpClient with fully customization of creation new HttpClient
        /// </summary>
        /// <param name="clientName">The name of the client</param>
        /// <param name="httpClientFactory">A factory method of HttpClient receive the early created client and return the modified HttpClient</param>
        /// <returns></returns>
        HttpClient Create(string clientName, Func<HttpClient, HttpClient> httpClientFactory);

        /// <summary>
        ///  Create a new named HttpClient with fully customization of creation new HttpClient and HttpMessagehandler
        /// </summary>
        /// <param name="clientName">The name of the client</param>
        /// <param name="pipelineFactory">A factory method of HttpMessageHandler pipeline. Receive the first handler and return the final pipeline</param>
        /// <param name="httpClientFactory">A factory method of HttpClient receive the early created client and return the modified HttpClient</param>
        /// <returns></returns>
        HttpClient Create(string clientName, Func<HttpMessageHandler, HttpMessageHandler> pipelineFactory, Func<HttpClient, HttpClient> httpClientFactory = null);
    }
}
