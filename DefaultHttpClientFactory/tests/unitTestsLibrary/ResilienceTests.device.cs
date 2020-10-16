using System;
using NUnit.Framework;
using Plugin.DefaultHttpClientFactory;

namespace unitTestsLibrary
{
    [TestFixture]
    public class ResilienceTests
    {
        private IDefaultHttpClientFactory _defaultHttpClientFactory;

        [SetUp]
        public void Setup()
        {
            _defaultHttpClientFactory = CrossDefaultHttpClientFactory.Current;
        }


        [TearDown]
        public void Tear() { }
    }
}
