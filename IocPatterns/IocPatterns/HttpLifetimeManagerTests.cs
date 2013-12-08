using System;
using System.Collections.Generic;
using System.Dynamic;
using FluentAssertions;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IocPatterns
{
    class HttpContext
    {
        public HttpContext()
        {
            Items = new Dictionary<string, object>();
        }

        public static HttpContext Current { get; set; }

        public static void CreateNewFakeRequest()
        {
            Current = new HttpContext();
        }

        public Dictionary<string, object> Items { get; set; }
    }

    [TestClass]
    public class HttpLifetimeManagerTests
    {
        [TestMethod]
        public void TestHttpLifetimeManager()
        {
            var container = new UnityContainer();
            container.RegisterType<DatatAccessDependency>(new HttpLifetimeManager());

            HttpContext.CreateNewFakeRequest();

            var client1 = container.Resolve<Client>();
            var client2 = container.Resolve<Client>();

            client1.Service1.DataAccess.Should().BeSameAs(client1.Service2.DataAccess);
            client2.Service1.DataAccess.Should().BeSameAs(client2.Service2.DataAccess);
            client2.Service1.DataAccess.Should().BeSameAs(client1.Service2.DataAccess);

            HttpContext.CreateNewFakeRequest();

            var client3 = container.Resolve<Client>();

            client3.Service1.DataAccess.Should().BeSameAs(client3.Service2.DataAccess);
            client3.Service1.DataAccess.Should().NotBeSameAs(client1.Service2.DataAccess);
        }
    }

    class HttpLifetimeManager : LifetimeManager
    {
        private readonly string _key;

        public HttpLifetimeManager()
        {
            _key = Guid.NewGuid().ToString();
        }

        public override object GetValue()
        {
            if (HttpContext.Current.Items.ContainsKey(_key))
                return HttpContext.Current.Items[_key];
            return null;
        }

        public override void SetValue(object newValue)
        {
            HttpContext.Current.Items[_key] = newValue;
        }

        public override void RemoveValue()
        {
            HttpContext.Current.Items.Remove(_key);
        }
    }
}
