using AlertTester.Telemetry.Interfaces;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Metrics.Extensibility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Tester.DTO;

namespace AlertTester.Telemetry
{
    /// <summary>
    /// Class to implement AppInsight telemetry
    /// </summary>
    public class ApplicationInsights : IApplicationInsights
    {
        private string InstrumentationKey { get; set; }

        private static readonly TelemetryClient telemetryClient = new TelemetryClient(
          new Microsoft.ApplicationInsights.Extensibility.TelemetryConfiguration()
          {
              InstrumentationKey = ConfigurationManager.AppSettings["APPINSIGHTS_INSTRUMENTATIONKEY"],
              ConnectionString = "InstrumentationKey=07369c9f-e0a2-437f-9666-ca52b0413835"
          });


        public ApplicationInsights()
        {
            _ = TelemetryConfiguration.Active;
            telemetryClient.InstrumentationKey = ConfigurationManager.AppSettings["APPINSIGHTS_INSTRUMENTATIONKEY"];
        }

        /// <summary>
        /// Method to Implement ApplicationInsights Trace
        /// </summary>
        /// <param name="message">message</param>
        public void TrackTrace(string message)
        {
            WriteToConsole(message);
            Dictionary<string, string> properties = new Dictionary<string, string>();
            properties.Add("TransactionGuid", StaticApplicationSettings.TransactionGuid.ToString());
            telemetryClient.TrackTrace(message, properties);
            telemetryClient.Flush();
        }

        /// <summary>
        /// Method to Implement ApplicationInsights Trace
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="properties">properties</param>
        public void TrackTrace(string message, IDictionary<string, string> properties)
        {
            WriteToConsole(message, properties);
            if (!properties.ContainsKey("TransactionGuid"))
                properties.Add("TransactionGuid", StaticApplicationSettings.TransactionGuid.ToString());
            telemetryClient.TrackTrace(message, properties);
            telemetryClient.Flush();
        }

        /// <summary>
        /// Method to Implement ApplicationInsights Trace
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="severityLevel">severity Level</param>
        public void TrackTrace(string message, SeverityLevel severityLevel)
        {
            WriteToConsole(message);

            Dictionary<string, string> properties = new Dictionary<string, string>();
            properties.Add("TransactionGuid", StaticApplicationSettings.TransactionGuid.ToString());
            telemetryClient.TrackTrace(message, severityLevel, properties);
            telemetryClient.Flush();
        }

        /// <summary>
        /// Method to Implement ApplicationInsights Trace
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="severityLevel">severity Level</param>
        /// <param name="properties">properties</param>
        public void TrackTrace(string message, SeverityLevel severityLevel, IDictionary<string, string> properties)
        {
            WriteToConsole(message, properties);

            if (!properties.ContainsKey("TransactionGuid"))
                properties.Add("TransactionGuid", StaticApplicationSettings.TransactionGuid.ToString());
            telemetryClient.TrackTrace(message, severityLevel, properties);
            telemetryClient.Flush();
        }

        /// <summary>
        /// Method to Implement ApplicationInsights Exceptions
        /// </summary>
        /// <param name="exception">exception</param>
        /// <param name="properties">properties</param>
        /// <param name="metrics">metrics</param>
        public void TrackException(Exception exception, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            if (properties == null)
            {
                properties = new Dictionary<string, string>();
            }
            WriteToConsole(exception.StackTrace, properties);

            if (!properties.ContainsKey("TransactionGuid"))
                properties.Add("TransactionGuid", StaticApplicationSettings.TransactionGuid.ToString());
            telemetryClient.TrackException(exception, properties);
            telemetryClient.Flush();
        }

        private void WriteToConsole(string message, IDictionary<string, string> properties = null)
        {
            System.Console.WriteLine(message);

            if (properties != null)
            {
                System.Console.WriteLine("------------Properties Start--------------");
                foreach (var record in properties)
                {
                    System.Console.WriteLine(record.Key + ": " + record.Value);
                }
                System.Console.WriteLine("------------Properties End--------------");
            }

        }

        #region "Not Implemented"
        public void TrackAvailability(AvailabilityTelemetry telemetry)
        {
            throw new NotImplementedException();
        }

        public void TrackAvailability(string name, DateTimeOffset timeStamp, TimeSpan duration, string runLocation, bool success, string message = null, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            throw new NotImplementedException();
        }

        public void TrackDependency(DependencyTelemetry telemetry)
        {
            throw new NotImplementedException();
        }

        public void TrackDependency(string dependencyTypeName, string target, string dependencyName, string data, DateTimeOffset startTime, TimeSpan duration, string resultCode, bool success)
        {
            throw new NotImplementedException();
        }

        public void TrackDependency(string dependencyTypeName, string dependencyName, string data, DateTimeOffset startTime, TimeSpan duration, bool success)
        {
            throw new NotImplementedException();
        }

        public void TrackDependency(string dependencyName, string data, DateTimeOffset startTime, TimeSpan duration, bool success)
        {
            throw new NotImplementedException();
        }

        public void TrackEvent(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            throw new NotImplementedException();
        }

        public void TrackEvent(EventTelemetry telemetry)
        {
            throw new NotImplementedException();
        }

        public void TrackException(ExceptionTelemetry telemetry)
        {
            throw new NotImplementedException();
        }

        public void TrackMetric(MetricTelemetry telemetry)
        {
            throw new NotImplementedException();
        }

        public void TrackMetric(string name, double value, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void TrackPageView(string name)
        {
            throw new NotImplementedException();
        }

        public void TrackPageView(PageViewTelemetry telemetry)
        {
            throw new NotImplementedException();
        }

        public void TrackRequest(RequestTelemetry request)
        {
            throw new NotImplementedException();
        }

        public void TrackRequest(string name, DateTimeOffset startTime, TimeSpan duration, string responseCode, bool success)
        {
            throw new NotImplementedException();
        }

        public void TrackTrace(TraceTelemetry telemetry)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}



