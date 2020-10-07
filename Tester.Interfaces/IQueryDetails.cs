using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlertTester.Interfaces
{
    /// <summary>
    /// Interface for Query Details
    /// </summary>
    public interface IQueryDetails
    {
        DateTime StartDate { get; set; }    
        DateTime EndDate { get; set; }
        int Interval { get; set; }
        int Window { get; set; }
        string Query { get; set; }
    }
}
