using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlertTester.DTO;
using AlertTester.Interfaces;
using Newtonsoft.Json;
using Tester.Process;

namespace AlertTester.Process
{
    /// <summary>
    /// Class to read application cnfiguration and Queries JSON.
    /// </summary>
    public class LocalConfiguration : IConfiguration
    {
        public ILogAnalyticsProviderDetails LogAnalyticsProviderConfig { get; set; }
        public IApplicationDetails ApplicationConfig { get; set; }
        public IEnumerable<IQueryDetails> QueryConfigs { get; set; }

        /// <summary>
        /// Read Application Configuration settings
        /// </summary>
        public void ReadConfig()
        {
            LogAnalyticsProviderConfig = new LogAnalyticsProviderDetails();
            ApplicationConfig = new ApplicationDetails();
            if (ConfigurationManager.AppSettings["AAClientID"] != null)
                LogAnalyticsProviderConfig.loganalyClientId = ConfigurationManager.AppSettings["AAClientID"];
            if (ConfigurationManager.AppSettings["AAClientKey"] != null)
                LogAnalyticsProviderConfig.loganalyClientKey = ConfigurationManager.AppSettings["AAClientKey"];
            if (ConfigurationManager.AppSettings["WorkSpaceID"] != null)
                LogAnalyticsProviderConfig.workSpaceID = ConfigurationManager.AppSettings["WorkSpaceID"];

            if (ConfigurationManager.AppSettings["OutputPath"] != null)
                ApplicationConfig.OutputPath = ConfigurationManager.AppSettings["OutputPath"];
            if (ConfigurationManager.AppSettings["JSONFilePathType"] != null)
                ApplicationConfig.JSONFilePathType = ConfigurationManager.AppSettings["JSONFilePathType"];
            if (ConfigurationManager.AppSettings["JSONFilePath"] != null)
                ApplicationConfig.JSONFilePath = ConfigurationManager.AppSettings["JSONFilePath"];
            if (ConfigurationManager.AppSettings["JSONFileName"] != null)
                ApplicationConfig.JSONFileName = ConfigurationManager.AppSettings["JSONFileName"];
            if (ConfigurationManager.AppSettings["MaxDegreeOfParallelism"] != null)
                ApplicationConfig.MaxDegreeOfParallelism = Convert.ToInt32(ConfigurationManager.AppSettings["MaxDegreeOfParallelism"]);
        }

        public void ReadJSONFile()
        {
            IReadJSON readJson = new ReadJSON();
            QueryConfigs = readJson.ReadJSONFile(ApplicationConfig.JSONFilePathType, ApplicationConfig.JSONFilePath, ApplicationConfig.JSONFileName);
        }
    }
}
