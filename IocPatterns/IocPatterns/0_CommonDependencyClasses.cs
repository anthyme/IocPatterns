using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace IocPatterns
{
    class Client
    {
        [Dependency]
        public BusinessDependency Service1 { get; set; }

        [Dependency]
        public BusinessDependency Service2 { get; set; }
    }

    public class BusinessDependency
    {
        [Dependency]
        public DataAccessDependency DataAccess { get; set; }
    }

    public class DataAccessDependency
    {

    }
}
