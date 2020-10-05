using System;
using System.Net;
using System.Net.Http;

namespace Plugin.DefaultHttpClientFactory
{
    internal class SocketsHttpHandlerFactory : ISocketsHttpHandlerFactory, ISocketsHttpHandlerBuilder
    {
        private SocketsHttpHandler _socketsHttpHandler;
        private static readonly SocketsHttpHandlerFactory _singleton = new SocketsHttpHandlerFactory();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static SocketsHttpHandlerFactory()
        {
        }

        private SocketsHttpHandlerFactory()
        {
        }

        public static ISocketsHttpHandlerFactory Instance
            => _singleton;


        private SocketsHttpHandlerFactory(SocketsHttpHandler socketsHttpHandler)
        {
            _socketsHttpHandler = socketsHttpHandler;
        }

        public ISocketsHttpHandlerBuilder Create()
            => new SocketsHttpHandlerFactory(new SocketsHttpHandler());

        public HttpMessageHandler Build()
            => _socketsHttpHandler;

        public ISocketsHttpHandlerBuilder SetAllowAutoRedirect(bool value)
        {
            _socketsHttpHandler.AllowAutoRedirect = value;
            return this;
        }

        public ISocketsHttpHandlerBuilder SetAllowAutoRedirect(Func<bool, bool> setterAction)
        {
            _socketsHttpHandler.AllowAutoRedirect = setterAction.Invoke(_socketsHttpHandler.AllowAutoRedirect);
            return this;
        }

        public ISocketsHttpHandlerBuilder SetAutomaticDecompression(DecompressionMethods value)
        {
            _socketsHttpHandler.AutomaticDecompression = value;
            return this;
        }

        public ISocketsHttpHandlerBuilder SetAutomaticDecompression(Func<DecompressionMethods, DecompressionMethods> setterAction)
        {
            _socketsHttpHandler.AutomaticDecompression = setterAction.Invoke(_socketsHttpHandler.AutomaticDecompression);
            return this;
        }

        public ISocketsHttpHandlerBuilder SetConnectTimeout(TimeSpan value)
        {
            _socketsHttpHandler.ConnectTimeout = value;
            return this;
        }

        public ISocketsHttpHandlerBuilder SetConnectTimeout(Func<TimeSpan, TimeSpan> setterAction)
        {
            _socketsHttpHandler.ConnectTimeout = setterAction.Invoke(_socketsHttpHandler.ConnectTimeout);
            return this;
        }

        public ISocketsHttpHandlerBuilder SetCookieContainer(CookieContainer value)
        {
            _socketsHttpHandler.CookieContainer = value;
            return this;
        }

        public ISocketsHttpHandlerBuilder SetCookieContainer(Func<CookieContainer, CookieContainer> setterAction)
        {
            _socketsHttpHandler.CookieContainer = setterAction.Invoke(_socketsHttpHandler.CookieContainer);
            return this;
        }

        public ISocketsHttpHandlerBuilder SetCredentials(ICredentials value)
        {
            _socketsHttpHandler.Credentials = value;
            return this;
        }

        public ISocketsHttpHandlerBuilder SetCredentials(Func<ICredentials, ICredentials> setterAction)
        {
            _socketsHttpHandler.Credentials = setterAction.Invoke(_socketsHttpHandler.Credentials);
            return this;
        }

        public ISocketsHttpHandlerBuilder SetDefaultProxyCredentials(ICredentials value)
        {
            _socketsHttpHandler.DefaultProxyCredentials = value;
            return this;
        }

        public ISocketsHttpHandlerBuilder SetDefaultProxyCredentials(Func<ICredentials, ICredentials> setterAction)
        {
            _socketsHttpHandler.DefaultProxyCredentials = setterAction.Invoke(_socketsHttpHandler.DefaultProxyCredentials);
            return this;
        }

        public ISocketsHttpHandlerBuilder SetExpect100ContinueTimeout(TimeSpan value)
        {
            _socketsHttpHandler.Expect100ContinueTimeout = value;
            return this;
        }

        public ISocketsHttpHandlerBuilder SetExpect100ContinueTimeout(Func<TimeSpan, TimeSpan> setterAction)
        {
            _socketsHttpHandler.Expect100ContinueTimeout = setterAction.Invoke(_socketsHttpHandler.Expect100ContinueTimeout);
            return this;
        }

        public ISocketsHttpHandlerBuilder SetMaxAutomaticRedirections(int value)
        {
            _socketsHttpHandler.MaxAutomaticRedirections = value;
            return this;
        }

        public ISocketsHttpHandlerBuilder SetMaxAutomaticRedirections(Func<int, int> setterAction)
        {
            _socketsHttpHandler.MaxAutomaticRedirections = setterAction.Invoke(_socketsHttpHandler.MaxAutomaticRedirections);
            return this;
        }

        public ISocketsHttpHandlerBuilder SetMaxConnectionsPerServer(int value)
        {
            _socketsHttpHandler.MaxConnectionsPerServer = value;
            return this;
        }

        public ISocketsHttpHandlerBuilder SetMaxConnectionsPerServer(Func<int, int> setterAction)
        {
            _socketsHttpHandler.MaxConnectionsPerServer = setterAction.Invoke(_socketsHttpHandler.MaxConnectionsPerServer);
            return this;
        }

        public ISocketsHttpHandlerBuilder SetMaxResponseDrainSize(int value)
        {
            _socketsHttpHandler.MaxResponseDrainSize = value;
            return this;
        }

        public ISocketsHttpHandlerBuilder SetMaxResponseDrainSize(Func<int, int> setterAction)
        {
            _socketsHttpHandler.MaxResponseDrainSize = setterAction.Invoke(_socketsHttpHandler.MaxResponseDrainSize);
            return this;
        }

        public ISocketsHttpHandlerBuilder SetMaxResponseHeadersLength(int value)
        {
            _socketsHttpHandler.MaxResponseHeadersLength = value;
            return this;
        }

        public ISocketsHttpHandlerBuilder SetMaxResponseHeadersLength(Func<int, int> setterAction)
        {
            _socketsHttpHandler.MaxResponseHeadersLength = setterAction.Invoke(_socketsHttpHandler.MaxResponseHeadersLength);
            return this;
        }

        public ISocketsHttpHandlerBuilder SetPooledConnectionLifetime(TimeSpan pooledLifeTime)
        {
            _socketsHttpHandler.PooledConnectionLifetime = pooledLifeTime;
            return this;
        }

        public ISocketsHttpHandlerBuilder SetPooledConnectionLifetime(Func<TimeSpan, TimeSpan> setterAction)
        {
            _socketsHttpHandler.PooledConnectionLifetime = setterAction.Invoke(_socketsHttpHandler.PooledConnectionLifetime);
            return this;
        }

        public ISocketsHttpHandlerBuilder SetPreAuthenticate(bool value)
        {
            _socketsHttpHandler.PreAuthenticate = value;
            return this;
        }

        public ISocketsHttpHandlerBuilder SetPreAuthenticate(Func<bool, bool> setterAction)
        {
            _socketsHttpHandler.PreAuthenticate = setterAction.Invoke(_socketsHttpHandler.PreAuthenticate);
            return this;
        }

        public ISocketsHttpHandlerBuilder SetProxy(IWebProxy value)
        {
            _socketsHttpHandler.Proxy = value;
            return this;
        }

        public ISocketsHttpHandlerBuilder SetProxy(Func<IWebProxy, IWebProxy> setterAction)
        {
            _socketsHttpHandler.Proxy = setterAction.Invoke(_socketsHttpHandler.Proxy);
            return this;
        }

        public ISocketsHttpHandlerBuilder SetResponseDrainTimeout(TimeSpan value)
        {
            _socketsHttpHandler.ResponseDrainTimeout = value;
            return this;
        }

        public ISocketsHttpHandlerBuilder SetResponseDrainTimeout(Func<TimeSpan, TimeSpan> setterAction)
        {
            _socketsHttpHandler.ResponseDrainTimeout = setterAction.Invoke(_socketsHttpHandler.ResponseDrainTimeout);
            return this;
        }

        //public ISocketsHttpHandlerBuilder SetSslOptions(SslClientAuthenticationOptions value)
        //{
        //    _socketsHttpHandler.SslOptions = value;
        //    return this;
        //}

        //public ISocketsHttpHandlerBuilder SetSslOptions(Func<SslClientAuthenticationOptions, SslClientAuthenticationOptions> setterAction)
        //{
        //    _socketsHttpHandler.SslOptions = setterAction.Invoke(_socketsHttpHandler.SslOptions);
        //    return this;
        //}

        public ISocketsHttpHandlerBuilder SetUseCookies(bool value)
        {
            _socketsHttpHandler.UseCookies = value;
            return this;
        }

        public ISocketsHttpHandlerBuilder SetUseCookies(Func<bool, bool> setterAction)
        {
            _socketsHttpHandler.UseCookies = setterAction.Invoke(_socketsHttpHandler.UseCookies);
            return this;
        }

        public ISocketsHttpHandlerBuilder SetUseProxy(bool value)
        {
            _socketsHttpHandler.UseProxy = value;
            return this;
        }

        public ISocketsHttpHandlerBuilder SetUseProxy(Func<bool, bool> setterAction)
        {
            _socketsHttpHandler.UseProxy = setterAction.Invoke(_socketsHttpHandler.UseProxy);
            return this;
        }
    }
}
