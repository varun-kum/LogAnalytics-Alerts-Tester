using AlertTester.Telemetry.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlertTester.Console
{
    /// <summary>
    /// Processing Class
    /// </summary>
    public class Process
    {
        IApplicationInsights _applicationInsights { get; set; }
        public Process(IApplicationInsights applicationInsights)
        {
            _applicationInsights = applicationInsights;
        }
        /// <summary>
        /// Method to read configs and Start Processing.
        /// </summary>
        public void DoWork()
        {
            try
            {
                //Log start
                _applicationInsights.TrackTrace("AlertTester: Starting Processing");

                AlertTester.Process.LocalConfiguration configuration = new AlertTester.Process.LocalConfiguration();

                //Read Application Connfig.
                configuration.ReadConfig();

                //Read JSON Config file to get queries information.
                configuration.ReadJSONFile();

                //Call Processing method for the Json Config read above.
                AlertTester.Process.QueryProcessor processor = new AlertTester.Process.QueryProcessor();
                processor.ExecuteQueries(configuration.LogAnalyticsProviderConfig,
                    configuration.ApplicationConfig,
                    configuration.QueryConfigs,
                    _applicationInsights);
            }
            catch(Exception ex)
            {
                //Log Exception
                _applicationInsights.TrackException(ex);
            }
        }
    }
}
