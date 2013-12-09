﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace IocPatterns
{
    public class BusinessDependency
    {
        [Dependency]
        public DataAccessDependency DataAccess { get; set; }
    }

    public class DataAccessDependency
    {

    }
}