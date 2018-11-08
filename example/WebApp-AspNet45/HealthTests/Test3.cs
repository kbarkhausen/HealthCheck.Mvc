using System;
using HealthCheck.Mvc.Models;

namespace HealthCheck.Mvc.HealthTests
{
    public class Test3 : HealthCheck.Mvc.HealthTest
    {
        public override string TestName => "Parsing config file";

        public override TestResult Run()
        {
            throw new NotImplementedException();
        }
    }
}