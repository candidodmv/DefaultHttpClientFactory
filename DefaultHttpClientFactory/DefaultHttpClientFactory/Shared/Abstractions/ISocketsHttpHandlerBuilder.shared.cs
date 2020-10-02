using System;
using System.Net;
using System.Net.Http;
using System.Net.Security;

namespace Plugin.DefaultHttpClientFactory
{
    public interface ISocketsHttpHandlerBuilder
    {
        public ISocketsHttpHandlerBuilder SetAllowAutoRedirect(bool value);
        public ISocketsHttpHandlerBuilder SetAllowAutoRedirect(Func<bool, bool> setterAction);

        public ISocketsHttpHandlerBuilder SetConnectTimeout(TimeSpan value);
        public ISocketsHttpHandlerBuilder SetConnectTimeout(Func<TimeSpan, TimeSpan> setterAction);

        public ISocketsHttpHandlerBuilder SetPooledConnectionLifetime(TimeSpan pooledLifeTime);
        public ISocketsHttpHandlerBuilder SetPooledConnectionLifetime(Func<TimeSpan, TimeSpan> setterAction);

        // System.Net.Security it's not present on .netStandard
        //public ISocketsHttpHandlerBuilder SetSslOptions(SslClientAuthenticationOptions value);
        //public ISocketsHttpHandlerBuilder SetSslOptions(Func<SslClientAuthenticationOptions, SslClientAuthenticationOptions> setterAction);

        public ISocketsHttpHandlerBuilder SetMaxResponseHeadersLength(int value);
        public ISocketsHttpHandlerBuilder SetMaxResponseHeadersLength(Func<int, int> setterAction);

        public ISocketsHttpHandlerBuilder SetResponseDrainTimeout(TimeSpan value);
        public ISocketsHttpHandlerBuilder SetResponseDrainTimeout(Func<TimeSpan, TimeSpan> setterAction);

        public ISocketsHttpHandlerBuilder SetMaxResponseDrainSize(int value);
        public ISocketsHttpHandlerBuilder SetMaxResponseDrainSize(Func<int, int> setterAction);

        public ISocketsHttpHandlerBuilder SetMaxConnectionsPerServer(int value);
        public ISocketsHttpHandlerBuilder SetMaxConnectionsPerServer(Func<int, int> setterAction);

        public ISocketsHttpHandlerBuilder SetMaxAutomaticRedirections(int value);
        public ISocketsHttpHandlerBuilder SetMaxAutomaticRedirections(Func<int, int> setterAction);

        public ISocketsHttpHandlerBuilder SetCredentials(ICredentials value);
        public ISocketsHttpHandlerBuilder SetCredentials(Func<ICredentials, ICredentials> setterAction);

        public ISocketsHttpHandlerBuilder SetPreAuthenticate(bool value);
        public ISocketsHttpHandlerBuilder SetPreAuthenticate(Func<bool, bool> setterAction);

        public ISocketsHttpHandlerBuilder SetDefaultProxyCredentials(ICredentials value);
        public ISocketsHttpHandlerBuilder SetDefaultProxyCredentials(Func<ICredentials, ICredentials> setterAction);

        public ISocketsHttpHandlerBuilder SetProxy(IWebProxy value);
        public ISocketsHttpHandlerBuilder SetProxy(Func<IWebProxy, IWebProxy> setterAction);

        public ISocketsHttpHandlerBuilder SetUseProxy(bool value);
        public ISocketsHttpHandlerBuilder SetUseProxy(Func<bool, bool> setterAction);

        public ISocketsHttpHandlerBuilder SetAutomaticDecompression(DecompressionMethods value);
        public ISocketsHttpHandlerBuilder SetAutomaticDecompression(Func<DecompressionMethods, DecompressionMethods> setterAction);

        public ISocketsHttpHandlerBuilder SetCookieContainer(CookieContainer value);
        public ISocketsHttpHandlerBuilder SetCookieContainer(Func<CookieContainer, CookieContainer> setterAction);

        public ISocketsHttpHandlerBuilder SetUseCookies(bool value);
        public ISocketsHttpHandlerBuilder SetUseCookies(Func<bool, bool> setterAction);

        public ISocketsHttpHandlerBuilder SetExpect100ContinueTimeout(TimeSpan value);
        public ISocketsHttpHandlerBuilder SetExpect100ContinueTimeout(Func<TimeSpan, TimeSpan> setterAction);

        public HttpMessageHandler Build();
    }
}
