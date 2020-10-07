using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlertTester.Interfaces;

namespace AlertTester.DTO
{
    /// <summary>
    /// Class for Query Details
    /// </summary>
    public class QueryDetails : IQueryDetails
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Interval { get; set; }
        public int Window { get; set; }
        public string Query { get; set; }
    }
}
