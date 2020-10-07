using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlertTester.Interfaces
{
    /// <summary>
    /// Interface for Log Analytics Provider Details
    /// </summary>
    public interface ILogAnalyticsProviderDetails
    {
        string loganalyClientId { get; set; }
        string loganalyClientKey { get; set; }
        string workSpaceID { get; set; }
    }
}
