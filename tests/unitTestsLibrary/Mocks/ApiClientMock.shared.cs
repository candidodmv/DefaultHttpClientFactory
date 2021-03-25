using System;
using System.Net.Http;
using Flurl.Http.Testing;

namespace unitTestsLibrary.Mocks
{
    public class ApiClientMock
    {
        private readonly HttpClient _httpClient;

        public ApiClientMock(HttpClient httpClient)
        {
            _httpClient = httpClient;
            var test = new HttpTest();

        }


    }
}
