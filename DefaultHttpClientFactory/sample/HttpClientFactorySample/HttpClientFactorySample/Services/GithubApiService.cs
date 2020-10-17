using Plugin.DefaultHttpClientFactory;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpClientFactorySample.Services
{
    public class GithubApiService : IGithubApiService
    {
        private readonly IDefaultHttpClientFactory _defaultHttpClientFactory;

        public GithubApiService(IDefaultHttpClientFactory defaultHttpClientFactory)
        {
            _defaultHttpClientFactory = defaultHttpClientFactory;
        }
    }
}
