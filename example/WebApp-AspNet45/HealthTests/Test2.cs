using HealthCheck.Mvc.Models;

namespace WebApp_AspNet45.HealthTests
{
    public class Test2 : HealthCheck.Mvc.HealthTest
    {
        public override string TestName => "Testing connectivity to DB";

        public override TestResult Run()
        {
            return new TestResult { Successful = true };
        }
    }
}