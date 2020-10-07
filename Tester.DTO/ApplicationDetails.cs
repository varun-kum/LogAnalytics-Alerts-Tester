using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlertTester.Interfaces;

namespace AlertTester.DTO
{
    /// <summary>
    /// Class for Log trace Custom Dimension DTO
    /// </summary>
    public class ApplicationDetails : IApplicationDetails
    {
        public string OutputPath { get; set; }
        public string JSONFilePath { get; set; }
        public string JSONFilePathType { get; set; }
        public string JSONFileName { get; set; }
        int IApplicationDetails.MaxDegreeOfParallelism { get; set; }
    }
}
