using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlertTester.Telemetry.Interfaces
{
    /// <summary>
    /// Interface to implement AppInsight telemetry
    /// </summary>
    public interface IApplicationInsights
    {
        //
        // Summary:
        //     Send information about availability of an application. Create a separate Microsoft.ApplicationInsights.DataContracts.AvailabilityTelemetry
        //     instance for each call to Microsoft.ApplicationInsights.TelemetryClient.TrackAvailability(Microsoft.ApplicationInsights.DataContracts.AvailabilityTelemetry).
        //
        // Remarks:
        //     Learn more
        void TrackAvailability(AvailabilityTelemetry telemetry);
        //
        // Summary:
        //     Send information about availability of an application.
        //
        // Parameters:
        //   name:
        //     Availability test name.
        //
        //   timeStamp:
        //     The time when the availability was captured.
        //
        //   duration:
        //     The time taken for the availability test to run.
        //
        //   runLocation:
        //     Name of the location the availability test was run from.
        //
        //   success:
        //     True if the availability test ran successfully.
        //
        //   message:
        //     Error message on availability test run failure.
        //
        //   properties:
        //     Named string values you can use to classify and search for this availability
        //     telemetry.
        //
        //   metrics:
        //     Additional values associated with this availability telemetry.
        //
        // Remarks:
        //     Learn more
        void TrackAvailability(string name, DateTimeOffset timeStamp, TimeSpan duration, string runLocation, bool success, string message = null, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null);
        //
        // Summary:
        //     Send information about external dependency call in the application. Create a
        //     separate Microsoft.ApplicationInsights.DataContracts.DependencyTelemetry instance
        //     for each call to Microsoft.ApplicationInsights.TelemetryClient.TrackDependency(Microsoft.ApplicationInsights.DataContracts.DependencyTelemetry).
        //
        // Remarks:
        //     Learn more
        void TrackDependency(DependencyTelemetry telemetry);
        //
        // Summary:
        //     Send information about an external dependency (outgoing call) in the application.
        //
        // Parameters:
        //   dependencyTypeName:
        //     External dependency type. Very low cardinality value for logical grouping and
        //     interpretation of fields. Examples are SQL, Azure table, and HTTP.
        //
        //   target:
        //     External dependency target.
        //
        //   dependencyName:
        //     Name of the command initiated with this dependency call. Low cardinality value.
        //     Examples are stored procedure name and URL path template.
        //
        //   data:
        //     Command initiated by this dependency call. Examples are SQL statement and HTTP
        //     URL's with all query parameters.
        //
        //   startTime:
        //     The time when the dependency was called.
        //
        //   duration:
        //     The time taken by the external dependency to handle the call.
        //
        //   resultCode:
        //     Result code of dependency call execution.
        //
        //   success:
        //     True if the dependency call was handled successfully.
        //
        // Remarks:
        //     Learn more
        void TrackDependency(string dependencyTypeName, string target, string dependencyName, string data, DateTimeOffset startTime, TimeSpan duration, string resultCode, bool success);
        //
        // Summary:
        //     Send information about an external dependency (outgoing call) in the application.
        //
        // Parameters:
        //   dependencyTypeName:
        //     External dependency type. Very low cardinality value for logical grouping and
        //     interpretation of fields. Examples are SQL, Azure table, and HTTP.
        //
        //   dependencyName:
        //     Name of the command initiated with this dependency call. Low cardinality value.
        //     Examples are stored procedure name and URL path template.
        //
        //   data:
        //     Command initiated by this dependency call. Examples are SQL statement and HTTP
        //     URL's with all query parameters.
        //
        //   startTime:
        //     The time when the dependency was called.
        //
        //   duration:
        //     The time taken by the external dependency to handle the call.
        //
        //   success:
        //     True if the dependency call was handled successfully.
        //
        // Remarks:
        //     Learn more
        void TrackDependency(string dependencyTypeName, string dependencyName, string data, DateTimeOffset startTime, TimeSpan duration, bool success);
        //
        // Summary:
        //     Send information about an external dependency (outgoing call) in the application.
        //
        // Parameters:
        //   dependencyName:
        //     Name of the command initiated with this dependency call. Low cardinality value.
        //     Examples are stored procedure name and URL path template.
        //
        //   data:
        //     Command initiated by this dependency call. Examples are SQL statement and HTTP
        //     URL's with all query parameters.
        //
        //   startTime:
        //     The time when the dependency was called.
        //
        //   duration:
        //     The time taken by the external dependency to handle the call.
        //
        //   success:
        //     True if the dependency call was handled successfully.
        //
        // Remarks:
        //     Learn more
        [Obsolete("Please use a different overload of TrackDependency")]
        void TrackDependency(string dependencyName, string data, DateTimeOffset startTime, TimeSpan duration, bool success);
        //
        // Summary:
        //     Send an Microsoft.ApplicationInsights.DataContracts.EventTelemetry for display
        //     in Diagnostic Search and in the Analytics Portal.
        //
        // Parameters:
        //   eventName:
        //     A name for the event.
        //
        //   properties:
        //     Named string values you can use to search and classify events.
        //
        //   metrics:
        //     Measurements associated with this event.
        //
        // Remarks:
        //     Learn more
        void TrackEvent(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null);
        //
        // Summary:
        //     Send an Microsoft.ApplicationInsights.DataContracts.EventTelemetry for display
        //     in Diagnostic Search and in the Analytics Portal. Create a separate Microsoft.ApplicationInsights.DataContracts.EventTelemetry
        //     instance for each call to Microsoft.ApplicationInsights.TelemetryClient.TrackEvent(Microsoft.ApplicationInsights.DataContracts.EventTelemetry).
        //
        // Parameters:
        //   telemetry:
        //     An event log item.
        //
        // Remarks:
        //     Learn more
        void TrackEvent(EventTelemetry telemetry);
        //
        // Summary:
        //     Send an Microsoft.ApplicationInsights.DataContracts.ExceptionTelemetry for display
        //     in Diagnostic Search. Create a separate Microsoft.ApplicationInsights.DataContracts.ExceptionTelemetry
        //     instance for each call to Microsoft.ApplicationInsights.TelemetryClient.TrackException(Microsoft.ApplicationInsights.DataContracts.ExceptionTelemetry).
        //
        // Remarks:
        //     Learn more
        void TrackException(ExceptionTelemetry telemetry);
        //
        // Summary:
        //     Send an Microsoft.ApplicationInsights.DataContracts.ExceptionTelemetry for display
        //     in Diagnostic Search.
        //
        // Parameters:
        //   exception:
        //     The exception to log.
        //
        //   properties:
        //     Named string values you can use to classify and search for this exception.
        //
        //   metrics:
        //     Additional values associated with this exception.
        //
        // Remarks:
        //     Learn more
        void TrackException(Exception exception, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null);
        //
        // Summary:
        //     This method is not the preferred method for sending metrics. Metrics should always
        //     be pre-aggregated across a time period before being sent.
        //     Use one of the GetMetric(..) overloads to get a metric object for accessing SDK
        //     pre-aggregation capabilities.
        //     If you are implementing your own pre-aggregation logic, then you can use this
        //     method. If your application requires sending a separate telemetry item at every
        //     occasion without aggregation across time, you likely have a use case for event
        //     telemetry; see Microsoft.ApplicationInsights.TelemetryClient.TrackEvent(Microsoft.ApplicationInsights.DataContracts.EventTelemetry).
        //
        // Parameters:
        //   telemetry:
        //     The metric telemetry item.
        void TrackMetric(MetricTelemetry telemetry);
        //
        // Summary:
        //     This method is not the preferred method for sending metrics. Metrics should always
        //     be pre-aggregated across a time period before being sent.
        //     Use one of the GetMetric(..) overloads to get a metric object for accessing SDK
        //     pre-aggregation capabilities.
        //     If you are implementing your own pre-aggregation logic, then you can use this
        //     method. If your application requires sending a separate telemetry item at every
        //     occasion without aggregation across time, you likely have a use case for event
        //     telemetry; see Microsoft.ApplicationInsights.TelemetryClient.TrackEvent(Microsoft.ApplicationInsights.DataContracts.EventTelemetry).
        //
        // Parameters:
        //   name:
        //     Metric name.
        //
        //   value:
        //     Metric value.
        //
        //   properties:
        //     Named string values you can use to classify and filter metrics.
        void TrackMetric(string name, double value, IDictionary<string, string> properties = null);
        //
        // Summary:
        //     Send information about the page viewed in the application.
        //
        // Parameters:
        //   name:
        //     Name of the page.
        //
        // Remarks:
        //     Learn more
        void TrackPageView(string name);
        //
        // Summary:
        //     Send information about the page viewed in the application. Create a separate
        //     Microsoft.ApplicationInsights.DataContracts.PageViewTelemetry instance for each
        //     call to Microsoft.ApplicationInsights.TelemetryClient.TrackPageView(Microsoft.ApplicationInsights.DataContracts.PageViewTelemetry).
        //
        // Remarks:
        //     Learn more
        void TrackPageView(PageViewTelemetry telemetry);
        //
        // Summary:
        //     Send information about a request handled by the application. Create a separate
        //     Microsoft.ApplicationInsights.DataContracts.RequestTelemetry instance for each
        //     call to Microsoft.ApplicationInsights.TelemetryClient.TrackRequest(Microsoft.ApplicationInsights.DataContracts.RequestTelemetry).
        //
        // Remarks:
        //     Learn more
        void TrackRequest(RequestTelemetry request);
        //
        // Summary:
        //     Send information about a request handled by the application.
        //
        // Parameters:
        //   name:
        //     The request name.
        //
        //   startTime:
        //     The time when the page was requested.
        //
        //   duration:
        //     The time taken by the application to handle the request.
        //
        //   responseCode:
        //     The response status code.
        //
        //   success:
        //     True if the request was handled successfully by the application.
        //
        // Remarks:
        //     Learn more
        void TrackRequest(string name, DateTimeOffset startTime, TimeSpan duration, string responseCode, bool success);
        //
        // Summary:
        //     Send a trace message for display in Diagnostic Search. Create a separate Microsoft.ApplicationInsights.DataContracts.TraceTelemetry
        //     instance for each call to Microsoft.ApplicationInsights.TelemetryClient.TrackTrace(Microsoft.ApplicationInsights.DataContracts.TraceTelemetry).
        //
        // Parameters:
        //   telemetry:
        //     Message with optional properties.
        //
        // Remarks:
        //     Learn more
        void TrackTrace(TraceTelemetry telemetry);
        //
        // Summary:
        //     Send a trace message for display in Diagnostic Search.
        //
        // Parameters:
        //   message:
        //     Message to display.
        //
        //   severityLevel:
        //     Trace severity level.
        //
        //   properties:
        //     Named string values you can use to search and classify events.
        //
        // Remarks:
        //     Learn more
        void TrackTrace(string message, SeverityLevel severityLevel, IDictionary<string, string> properties);
        //
        // Summary:
        //     Send a trace message for display in Diagnostic Search.
        //
        // Parameters:
        //   message:
        //     Message to display.
        //
        //   properties:
        //     Named string values you can use to search and classify events.
        //
        // Remarks:
        //     Learn more
        void TrackTrace(string message, IDictionary<string, string> properties);
        //
        // Summary:
        //     Send a trace message for display in Diagnostic Search.
        //
        // Parameters:
        //   message:
        //     Message to display.
        //
        //   severityLevel:
        //     Trace severity level.
        //
        // Remarks:
        //     Learn more
        void TrackTrace(string message, SeverityLevel severityLevel);
        //
        // Summary:
        //     Send a trace message for display in Diagnostic Search.
        //
        // Parameters:
        //   message:
        //     Message to display.
        //
        // Remarks:
        //     Learn more
        void TrackTrace(string message);
    }
}

