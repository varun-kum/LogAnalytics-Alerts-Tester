using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlertTester.Interfaces
{
    /// <summary>
    /// Interface for Log trace Custom Dimension DTO
    /// </summary>
    public interface IApplicationDetails
    {
        string OutputPath { get; set; }
        string JSONFilePathType { get; set; }
        string JSONFilePath { get; set; }
        string JSONFileName { get; set; }

        int MaxDegreeOfParallelism { get; set; }
    }
}
        