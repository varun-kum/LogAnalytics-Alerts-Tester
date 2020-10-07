using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester.Process
{
    /// <summary>
    /// Class to deserialize Query Json - JsonAlertStructure
    /// </summary>
    public class JsonAlertStructure
    {
        public string alertName { get; set; }
        public string description { get; set; }
        public string severity { get; set; }
        public string alertSeverity { get; set; }
        public string query { get; set; }
        public string source { get; set; }
        public string sourceInstance { get; set; }
        public string suppressionBehavior { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Interval { get; set; }
        public int Window { get; set; }
    }
}
