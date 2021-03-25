using HttpClientFactorySample.Infrastructure;
using Plugin.DefaultHttpClientFactory;
using System;

namespace HttpClientFactorySample.Services
{
    public class MarvelApiService : IMarvelApiService
    {
        private readonly IDefaultHttpClientFactory _defaultHttpClientFactory;
        private readonly Lazy<IMarvelApiClient> _marvelClientLazy;
        private string computeNewTs => DateTime.Now.ToTimestamp().ToString("#");

        public IMarvelApiClient ApiClient => _marvelClientLazy.Value;

        public MarvelApiService(IDefaultHttpClientFactory defaultHttpClientFactory)
        {
            _defaultHttpClientFactory = defaultHttpClientFactory;
            _marvelClientLazy = new Lazy<IMarvelApiClient>(() =>
                new MarvelApiClient(_defaultHttpClientFactory.Create()));
        }


    }
}
