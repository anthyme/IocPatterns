using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IocPatterns
{
    public class User { }

    public class Country { }

    public interface IRepository<T>
    {
        void Add(T entity);
    }

    public class Repository<T> : IRepository<T> {
        public void Add(T entity) { }
    }

    public interface IUserRepository : IRepository<User>
    {
        void Authenticate();
    }

    public class UserRepository : Repository<User>, IUserRepository {
        public void Authenticate() { }
    }

    [TestClass]
    public class RegisterByConventionTests
    {
        [TestMethod]
        public void TestRegisterByConvention()
        {
            var container = new WindsorContainer();

            #region spoiler
            container.Register(Component.For(typeof(IRepository<>)).ImplementedBy(typeof(Repository<>)));
            container.Register(Classes.FromThisAssembly()
                    .BasedOn(typeof(IRepository<>))
                    .WithServiceAllInterfaces()
                    .LifestyleTransient());
            #endregion

            var userRepository1 = container.Resolve<IUserRepository>();
            userRepository1.GetType().Should().Be(typeof (UserRepository));

            var userRepository2 = container.Resolve<IRepository<User>>();
            userRepository2.GetType().Should().Be(typeof(UserRepository));

            var countryRepository = container.Resolve<IRepository<Country>>();
            countryRepository.GetType().Should().Be(typeof(Repository<Country>));

            #region spoiler
            //container.Register(Classes.FromAssemblyInDirectory()
            //container.Register(Classes.FromAssemblyInThisApplication()
            //container.Register(Classes.FromAssemblyNamed()

            //container.Register(Classes.FromThisAssembly()
            //  .InSameNamespaceAs<UserRepository>()
            //  .WithService.Self()
            //  .WithService.DefaultInterfaces()
            //  .WithServiceFirstInterface()
            //  .WithServiceFromInterface()
            //  .LifestyleTransient());
            #endregion
        }
    }
}
