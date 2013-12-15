using System;
using System.ComponentModel;
using FluentAssertions;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IocPatterns
{
    [TestClass]
    public class UnitOfWorkTests
    {
        [TestMethod]
        public void TestUnitOfWork()
        {
            var container = new UnityContainer();

            var client = container.Resolve<Client>();

            client.Service1.Should().NotBeSameAs(client.Service2);
            client.Service1.DataAccess.Should().NotBeSameAs(client.Service2.DataAccess);

            var unitOfWork = container.Resolve<UnitOfWork>();

            var client2 = unitOfWork.Create<Client>();
            var client3 = unitOfWork.Create<Client>();

            client2.Service1.Should().NotBeSameAs(client2.Service2);
            client2.Service1.DataAccess.Should().BeSameAs(client2.Service2.DataAccess);

            client3.Service1.Should().NotBeSameAs(client3.Service2);
            client3.Service1.DataAccess.Should().BeSameAs(client3.Service2.DataAccess);

            client2.Service1.DataAccess.Should().BeSameAs(client3.Service2.DataAccess);
        }
    }

    #region spoiler
    class UnitOfWork
    {
        private readonly IUnityContainer _container;

        public UnitOfWork(IUnityContainer container)
        {
            _container = container.CreateChildContainer();
            DataAccess = _container.Resolve<DataAccessDependency>();
            _container.RegisterInstance(DataAccess);
        }

        public DataAccessDependency DataAccess { get; private set; }

        public T Create<T>()
        {
            return _container.Resolve<T>();
        }
    }
    #endregion
}
