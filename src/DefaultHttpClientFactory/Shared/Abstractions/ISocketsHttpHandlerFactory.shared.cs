namespace Plugin.DefaultHttpClientFactory
{
    internal interface ISocketsHttpHandlerFactory
    {
        public ISocketsHttpHandlerBuilder Create();
    }
}
