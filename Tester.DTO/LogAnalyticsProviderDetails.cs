using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlertTester.Interfaces;

namespace AlertTester.DTO
{
    /// <summary>
    /// Class for Log Analytics Provider Details
    /// </summary>
    public class LogAnalyticsProviderDetails : ILogAnalyticsProviderDetails
    {
        public string loganalyClientId { get; set; }
        public string loganalyClientKey { get; set; }
        public string workSpaceID { get; set; }
    }
}
