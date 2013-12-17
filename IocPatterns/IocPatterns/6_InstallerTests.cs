using System;
using System.Collections.Generic;
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
    [TestClass]
    public class InstallerTests
    {
        [TestMethod]
        public void TestRegisterByInstaller()
        {
            var container = new WindsorContainer();

            #region spoiler
            
            container.Install(FromAssembly.This());
            
            #endregion

            var userRepository1 = container.Resolve<IUserRepository>();
            userRepository1.GetType().Should().Be(typeof(UserRepository));

            var userRepository2 = container.Resolve<IRepository<User>>();
            userRepository2.GetType().Should().Be(typeof(UserRepository));

            var countryRepository = container.Resolve<IRepository<Country>>();
            countryRepository.GetType().Should().Be(typeof(Repository<Country>));

            #region spoiler

            //FromAssembly.Containing<UserRepository>();
            //FromAssembly.InDirectory();
            //FromAssembly.InThisApplication();
            //FromAssembly.Instance(Assembly.GetCallingAssembly());

            #endregion
        }
    }

    #region spoiler

    public class RepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For(typeof(IRepository<>)).ImplementedBy(typeof(Repository<>)));
            container.Register(Classes.FromThisAssembly()
                .BasedOn(typeof(IRepository<>))
                .WithServiceAllInterfaces()
                .LifestyleTransient());
        }
    }

    #endregion
}
