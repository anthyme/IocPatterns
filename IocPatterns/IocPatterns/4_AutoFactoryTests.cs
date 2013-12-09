using System;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IocPatterns
{
    public interface IContextualDependencyAutoFactory
    {
        ContextualDependency Create(ContextParameter parameter);
        void Release(ContextualDependency businessDependency);
    }

    [TestClass]
    public class AutoFactoryTests
    {
        [TestMethod]
        public void TestAutoFactory()
        {
            var container = new WindsorContainer();

            #region spoiler
            container.AddFacility<TypedFactoryFacility>();
            container.Register(
                Component.For<IContextualDependencyAutoFactory>().AsFactory(),
                Component.For<DataAccessDependency>(),
                Component.For<ContextualDependency>()
                );
            #endregion

            var factory = container.Resolve<IContextualDependencyAutoFactory>();

            var context = new ContextParameter();
            var dep = factory.Create(context);

            dep.Parameter.Should().BeSameAs(context);
            dep.DataAccess.Should().NotBeNull();
        }
    }
}
