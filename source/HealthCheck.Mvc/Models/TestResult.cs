namespace HealthCheck.Mvc.Models
{
    /// <summary>
    /// The result of a test execution.
    /// All tests are considered successful unless they throw an exception or change the value of Successful to false
    /// </summary>
    public class TestResult
    {
        public bool Successful { get; set; }
        public string ErrorMessage { get; set; }

        public TestResult()
        {
            Successful = true;
        }
    }
}
