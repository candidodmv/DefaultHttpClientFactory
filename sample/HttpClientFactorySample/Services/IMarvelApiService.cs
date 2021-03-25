namespace HttpClientFactorySample.Services
{
    public interface IMarvelApiService
    {
        IMarvelApiClient ApiClient { get; }
    }
}
