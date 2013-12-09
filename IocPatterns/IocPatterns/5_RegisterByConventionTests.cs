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
    public class Entity1 { }

    public class Entity2 { }

    public interface IRepository<T>
    {
        void Add(T entity);
    }

    public class Repository<T> : IRepository<T> {
        public void Add(T entity) { }
    }

    public interface IEntity1Repository : IRepository<Entity1>
    {
        void Authenticate();
    }

    public class Entity1Repository : Repository<Entity1>, IEntity1Repository {
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

            var entity1Repository = container.Resolve<IEntity1Repository>();
            entity1Repository.GetType().Should().Be(typeof (Entity1Repository));

            var entity1Repository2 = container.Resolve<IRepository<Entity1>>();
            entity1Repository2.GetType().Should().Be(typeof(Entity1Repository));

            var entity2Repository = container.Resolve<IRepository<Entity2>>();
            entity2Repository.GetType().Should().Be(typeof(Repository<Entity2>));

            #region spoiler
            //container.Register(Classes.FromAssemblyInDirectory()
            //container.Register(Classes.FromAssemblyInThisApplication()
            //container.Register(Classes.FromAssemblyNamed()

            //container.Register(Classes.FromThisAssembly()
            //  .InSameNamespaceAs<Entity1Repository>()
            //  .WithService.Self()
            //  .WithService.DefaultInterfaces()
            //  .WithServiceFirstInterface()
            //  .WithServiceFromInterface()
            //  .LifestyleTransient());
            #endregion
        }
    }
}
