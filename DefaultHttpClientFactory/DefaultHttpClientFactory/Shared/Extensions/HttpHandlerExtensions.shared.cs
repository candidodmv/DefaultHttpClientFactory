using System.Net.Http;

namespace Plugin.DefaultHttpClientFactory.Shared.Extensions
{
    public static class HttpMessageHandlerExtensions
    {
        public static DelegatingHandler DecorateWith(this HttpMessageHandler innerHandler, DelegatingHandler decorator)
        {
            decorator.InnerHandler = innerHandler;
            return decorator;
        }

        public static DelegatingHandler DecorateWith(this DelegatingHandler innerHandler, DelegatingHandler decorator)
        {
            decorator.InnerHandler = innerHandler;
            return decorator;
        }
    }
}
