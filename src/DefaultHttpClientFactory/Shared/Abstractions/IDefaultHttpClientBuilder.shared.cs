using Polly;
using System;
using System.Net.Http;

namespace Plugin.DefaultHttpClientFactory.Shared.Abstractions
{
    /// <summary>
    /// A fully customization of HttpClient Factory
    /// </summary>
    public interface IDefaultHttpClientBuilder
    {
        /// <summary>
        /// Configure the newly created HttpClient
        /// </summary>
        /// <param name="httpClientConfigurator">A function that receive the newly created HttpClient and returns the newly created HttpClient modified</param>
        /// <returns>The current builder</returns>
        IDefaultHttpClientBuilder ConfigureHttpClient(Func<HttpClient, HttpClient> httpClientConfigurator);

        /// <summary>
        /// Configure the HttpMessageHandler pipeline
        /// </summary>
        /// <param name="pipelineFactory">A function that receive the primary HttpMessageHandler and return the constructed pipeline </param>
        /// <returns>The current builder</returns>
        IDefaultHttpClientBuilder ConfigureMessageHandlers(Func<HttpMessageHandler, HttpMessageHandler> pipelineFactory);

        /// <summary>
        /// Configure the execution Resilience Policy
        /// </summary>
        /// <param name="pipelineFactory">A function that receive the Common Http Policy Resilient and return the constructed pipeline</param>
        /// <returns>The current builder</returns>
        IDefaultHttpClientBuilder ConfigureExecutionPolicy(Func<IAsyncPolicy<HttpResponseMessage>, IAsyncPolicy<HttpResponseMessage>> pipelineFactory);

        /// <summary>
        /// Aplies the configurations and runs the build process
        /// </summary>
        /// <returns>The configured HttpClient</returns>
        HttpClient Build();
    }
}
