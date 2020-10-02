namespace Plugin.DefaultHttpClientFactory
{
    public interface ISocketsHttpHandlerFactory
    {
        public ISocketsHttpHandlerBuilder Create();
    }
}
