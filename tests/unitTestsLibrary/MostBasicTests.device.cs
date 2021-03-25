using System;
using System.Net.Http;
using NUnit.Framework;
using Plugin.DefaultHttpClientFactory;

namespace unitTestsLibrary
{
    [TestFixture]
    public class MostBasicTests
    {
        private IDefaultHttpClientFactory _defaultHttpClientFactory;

        [SetUp]
        public void Setup() {
            _defaultHttpClientFactory = CrossDefaultHttpClientFactory.Current;
        }


        [TearDown]
        public void Tear() { }

        [Test]
        public void instance_is_not_null()
        {
            
            var instance = _defaultHttpClientFactory.Create();
            Console.WriteLine("test1");
            Assert.IsNotNull(instance);
        }

        [Test]
        public void two_instances_are_different()
        {
            var instance1 = _defaultHttpClientFactory.Create();
            var instance2 = _defaultHttpClientFactory.Create();
            
            Assert.AreNotSame(instance1, instance2);
        }

        [Test]
        public void two_instances_are_different_even_same_name()
        {
            var instanceName = "intanceName";
            var instance1 = _defaultHttpClientFactory.Create(instanceName);
            var instance2 = _defaultHttpClientFactory.Create(instanceName);

            Assert.AreNotSame(instance1, instance2);
        }

        [Test]
        public void base_message_handler_is_same()
        {
            var instance1 = _defaultHttpClientFactory.GetOrInstantiateHttpMessageHanlder();
            var instance2 = _defaultHttpClientFactory.GetOrInstantiateHttpMessageHanlder();

            Assert.AreSame(instance1, instance2);
        }

        [Test]
        public void base_message_handler_is_sockets()
        {
            var instance = _defaultHttpClientFactory.GetOrInstantiateHttpMessageHanlder();

            Assert.IsTrue(instance is SocketsHttpHandler);
        }

        //[Test]
        //public void Fail()
        //{
        //    Assert.False(true);
        //}

        //[Test]
        //[Ignore("another time")]
        //public void Ignore()
        //{
        //    Assert.True(false);
        //}

        //[Test]
        //public void Inconclusive()
        //{
        //    Assert.Inconclusive("Inconclusive");
        //}
    }
}
