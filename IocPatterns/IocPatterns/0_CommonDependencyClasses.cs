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
        public BusinessService Service1 { get; set; }

        [Dependency]
        public BusinessService Service2 { get; set; }
    }

    public class BusinessService
    {
        [Dependency]
        public DataAccess DataAccess { get; set; }
    }

    public class DataAccess
    {

    }
}
