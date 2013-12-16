using System;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IocPatterns
{
    public interface IServiceAutoFactory
    {
        Service Create(Parameter parameter);
        void Release(Service businessDependency);
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
                Component.For<IServiceAutoFactory>().AsFactory(),
                Component.For<DataAccess>(),
                Component.For<Service>()
                );
            #endregion

            var factory = container.Resolve<IServiceAutoFactory>();

            var context = new Parameter();
            var dep = factory.Create(context);

            dep.Parameter.Should().BeSameAs(context);
            dep.DataAccess.Should().NotBeNull();
        }
    }
}
