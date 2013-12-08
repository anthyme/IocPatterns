using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace IocPatterns
{
    class BusinessDependency
    {
        [Dependency]
        public DatatAccessDependency DataAccess { get; set; }
    }

    class DatatAccessDependency
    {

    }
}
