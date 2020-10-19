using Plugin.DefaultHttpClientFactory;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpClientFactorySample.Services
{
    public class MarvelApiService : IMarvelApiService
    {
        private readonly IDefaultHttpClientFactory _defaultHttpClientFactory;

        public MarvelApiService(IDefaultHttpClientFactory defaultHttpClientFactory)
        {
            _defaultHttpClientFactory = defaultHttpClientFactory;
        }
    }
}
