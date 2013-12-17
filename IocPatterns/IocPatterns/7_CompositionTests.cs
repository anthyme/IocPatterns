using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IocPatterns
{
    public interface IService
    {

    }

    [Export(typeof(IService))]
    public class ServiceMef : IService
    {

    }

    [TestClass]
    public class CompositionTests
    {
        [TestMethod]
        public void TestComposition()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            //new DirectoryCatalog("path")
            var container = new CompositionContainer(catalog);

            var service = container.GetExportedValue<IService>();
            service.GetType().Should().Be(typeof(ServiceMef));
        }
    }
}
