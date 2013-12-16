using System;
using System.Dynamic;
using System.Runtime.CompilerServices;
using Castle.Windsor;
using FluentAssertions;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IocPatterns
{
    public class Parameter { }

    public class Service
    {
        public Service(Parameter parameter, DataAccess dataAccess)
        {
            Parameter = parameter;
            DataAccess = dataAccess;
        }

        public Parameter Parameter { get; private set; }
        public DataAccess DataAccess { get; private set; }
    }

    public interface IServiceFactory
    {
        Service Create(Parameter parameter);
    }

    [TestClass]
    public class FactoryTests
    {
        [TestMethod]
        public void TestManualFactory()
        {
            var container = new UnityContainer();
            #region spoiler
            container.RegisterType<IContextualDependencyFactory, ServiceFactory>();
            #endregion

            var factory = container.Resolve<IServiceFactory>();
            var parameter = new Parameter();

            var dependency = factory.Create(parameter);

            dependency.Parameter.Should().BeSameAs(parameter);
            dependency.DataAccess.Should().NotBeNull();
        }
    }

    #region spoiler
    class ServiceFactory : IServiceFactory
    {
        private readonly IUnityContainer _container;

        public ServiceFactory(IUnityContainer container)
        {
            _container = container;
        }

        public Service Create(Parameter parameter)
        {
            return _container.Resolve<Service>(
                new ParameterOverride("parameter", parameter));
        }
    }
    #endregion
}
