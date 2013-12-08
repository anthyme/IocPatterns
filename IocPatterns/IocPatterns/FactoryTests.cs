using System;
using System.Dynamic;
using System.Runtime.CompilerServices;
using Castle.Windsor;
using FluentAssertions;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IocPatterns
{
    class ContextParameter
    {

    }

    class ContextualDependency
    {
        public ContextualDependency(ContextParameter parameter)
        {
            Parameter = parameter;
        }

        public ContextParameter Parameter { get; set; }
    }

    interface IContextualDependencyFactory
    {
        ContextualDependency Create(ContextParameter parameter);
    }

    [TestClass]
    public class FactoryTests
    {
        [TestMethod]
        public void TestManualFactory()
        {
            var container = new UnityContainer();
            container.RegisterType<IContextualDependencyFactory, ContextualDependencyFactory>();

            var factory = container.Resolve<IContextualDependencyFactory>();
            var parameter = new ContextParameter();

            var dependency = factory.Create(parameter);

            dependency.Parameter.Should().BeSameAs(parameter);
        }
    }

    class ContextualDependencyFactory : IContextualDependencyFactory
    {
        private readonly IUnityContainer _container;

        public ContextualDependencyFactory(IUnityContainer container)
        {
            _container = container;
        }

        public ContextualDependency Create(ContextParameter parameter)
        {
            return _container.Resolve<ContextualDependency>(
                new ParameterOverride("parameter", parameter));
        }
    }
}
