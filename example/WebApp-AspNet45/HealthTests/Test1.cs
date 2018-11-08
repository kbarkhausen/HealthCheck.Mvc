using HealthCheck.Mvc.Models;

namespace HealthCheck.Mvc.HealthTests
{
    public class Test1 : HealthCheck.Mvc.HealthTest
    {

        public override string TestName => "Testing connectivity to API";

        public override TestResult Run()
        {
            var result = new TestResult();

            result.Successful = true;

            var configvalue1 = GetTestSetting("connectionString");            

            return result;
        }
    }
}