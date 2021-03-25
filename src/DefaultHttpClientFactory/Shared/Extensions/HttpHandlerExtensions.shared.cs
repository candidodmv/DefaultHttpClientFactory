using System.Net.Http;

namespace Plugin.DefaultHttpClientFactory.Shared.Extensions
{
    /// <summary>
    /// Common Extensions
    /// </summary>
    public static class HttpMessageHandlerExtensions
    {
        /// <summary>
        /// Decorate HttpMessageHandler with it's inner handler
        /// </summary>
        /// <param name="innerHandler"></param>
        /// <param name="decorator"></param>
        /// <returns></returns>
        public static DelegatingHandler DecorateWith(this HttpMessageHandler innerHandler, DelegatingHandler decorator)
        {
            decorator.InnerHandler = innerHandler;
            return decorator;
        }
        
        /// <summary>
        /// Decorate DelegatingHandler with it's inner handler
        /// </summary>
        /// <param name="innerHandler"></param>
        /// <param name="decorator"></param>
        /// <returns></returns>
        public static DelegatingHandler DecorateWith(this DelegatingHandler innerHandler, DelegatingHandler decorator)
        {
            decorator.InnerHandler = innerHandler;
            return decorator;
        }
    }
}
