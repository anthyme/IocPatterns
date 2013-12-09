using System;
using System.Dynamic;
using System.Runtime.CompilerServices;
using Castle.Windsor;
using FluentAssertions;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IocPatterns
{
    public class ContextParameter
    {

    }

    public class ContextualDependency
    {
        public ContextualDependency(ContextParameter parameter, DataAccessDependency dataAccess)
        {
            Parameter = parameter;
            DataAccess = dataAccess;
        }

        public ContextParameter Parameter { get; private set; }
        public DataAccessDependency DataAccess { get; private set; }
    }

    public interface IContextualDependencyFactory
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
            #region spoiler
            container.RegisterType<IContextualDependencyFactory, ContextualDependencyFactory>();
            #endregion

            var factory = container.Resolve<IContextualDependencyFactory>();
            var parameter = new ContextParameter();

            var dependency = factory.Create(parameter);

            dependency.Parameter.Should().BeSameAs(parameter);
            dependency.DataAccess.Should().NotBeNull();
        }
    }

    #region spoiler
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
    #endregion
}
