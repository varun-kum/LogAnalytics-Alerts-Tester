using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlertTester.Interfaces;
using AlertTester.Process.Providers;
using AlertTester.Telemetry.Interfaces;
using Tester.DTO;

namespace AlertTester.Process
{
    /// <summary>
    /// Class to process Log Analytics queries
    /// </summary>
    public class QueryProcessor
    {
        /// <summary>
        /// Method to process queries.
        /// </summary>
        /// 
        public void ExecuteQueries(
            ILogAnalyticsProviderDetails logAnalyticsProviderDetails,
            IApplicationDetails applicationDetails,
            IEnumerable<IQueryDetails> queries, IApplicationInsights _applicationInsights)
        {
            //Run each query in parallel with MaxDegreeOfParallelism.
            Parallel.ForEach(
                queries,
                new ParallelOptions { MaxDegreeOfParallelism = applicationDetails.MaxDegreeOfParallelism },
                queryDetails => ExecuteQuery(logAnalyticsProviderDetails, applicationDetails, queryDetails, _applicationInsights));
        }

        /// <summary>
        /// Method to process each query. 
        /// </summary>
        /// 
        public void ExecuteQuery(
            ILogAnalyticsProviderDetails logAnalyticsProviderDetails,
            IApplicationDetails applicationDetails,
            IQueryDetails queryDetails, IApplicationInsights _applicationInsights)
        {
            Guid QueryPrcessingGuid = Guid.NewGuid();
            Dictionary<string, string> properties = new Dictionary<string, string>();
            properties.Add("QueryPrcessingGuid", QueryPrcessingGuid.ToString());
            _applicationInsights.TrackTrace("AlertTester: Starting Processing Query", properties);

            try
            {
                //Index to give uniqueid for each dictionary record for trace custom dimension.
                int index = 1;
                DateTime dtIntervalEnd = queryDetails.StartDate.AddMinutes(queryDetails.Interval); ;
                DateTime dtWindow = dtIntervalEnd.AddMinutes(-queryDetails.Window); ;
                LogAnalyticsProviderV2 loganalyticsprovider = new LogAnalyticsProviderV2
                    (logAnalyticsProviderDetails.loganalyClientId,
                    logAnalyticsProviderDetails.loganalyClientKey);

                List<string> lstresult = new List<string>();
                while (dtIntervalEnd <= queryDetails.EndDate)
                {
                    string dtstartString = dtWindow.ToString("yyyy-MM-dd HH:mm:ss");
                    string dtendString = dtIntervalEnd.ToString("yyyy-MM-dd HH:mm:ss");
                    string logAnalyticsQuery = string.Format(queryDetails.Query, dtstartString, dtendString);
                    Tuple<double, Int64, Int64, bool> retQOS = loganalyticsprovider.ProcessQOSQuery(
                            logAnalyticsProviderDetails.workSpaceID,
                            logAnalyticsProviderDetails.loganalyClientId,
                            logAnalyticsProviderDetails.loganalyClientKey,
                            logAnalyticsQuery);

                    dtIntervalEnd = dtIntervalEnd.AddMinutes(queryDetails.Interval);
                    dtWindow = dtIntervalEnd.AddMinutes(-queryDetails.Window);

                    //check if the query executed successfully.
                    if (retQOS.Item1 == 1)
                    {
                        //Populate log object for trace CustomDimension.
                        LogResultDTO resultDTO = new LogResultDTO();
                        resultDTO.ProcessingDateTime = dtendString;
                        resultDTO.Count = retQOS.Item3;
                        resultDTO.Result = retQOS.Item4;
                        resultDTO.Query = logAnalyticsQuery;
                        string message = Newtonsoft.Json.JsonConvert.SerializeObject(resultDTO);

                        //Adding the result into log message properties
                        properties.Add(index.ToString(), message);
                        index++;
                        //lstresult.Add(dtendString + "," + retQOS.Item3 + "," + retQOS.Item4);
                    }
                }

                //Send trace log to appinsights.
                _applicationInsights.TrackTrace("AlertTester: Query Results", properties);

                if (lstresult.Count > 1)
                    File.WriteAllLines(Path.Combine(applicationDetails.OutputPath, Guid.NewGuid().ToString() + ".csv"), lstresult);
            }
            catch (Exception ex)
            {
                //Log Exception
                _applicationInsights.TrackException(ex, properties);
            }
        }
    }
}
