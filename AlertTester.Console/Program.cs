using AlertTester.Telemetry;
using AlertTester.Telemetry.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tester.DTO;

namespace AlertTester.Console
{
    class Program
    {
        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            StaticApplicationSettings.TransactionGuid = Guid.NewGuid();
            IApplicationInsights applicationInsights = new ApplicationInsights();
            
            //Start Processing
            Process process = new Process(applicationInsights);

            //Method to start processing the work.
            process.DoWork();
        }
    }
}
