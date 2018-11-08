using System.Collections.Generic;

namespace HealthCheck.Mvc.Models
{
    public class IndexPageModel
    {        
        public string PageTitle { get; set; }
        public string Footer { get; set; }

        public List<DisplayTestResult> TestResults { get; set; }

        public IndexPageModel()
        {
            TestResults = new List<DisplayTestResult>();
        }
    }
}
