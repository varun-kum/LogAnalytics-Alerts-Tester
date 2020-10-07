using AlertTester.DTO;
using AlertTester.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester.Process
{
    public class ReadJSON : IReadJSON
    {

        /// <summary>
        /// Read Query JSON File
        /// </summary>
        public IEnumerable<IQueryDetails> ReadJSONFile(string JSONFilePathType, string JSONFilePath, string JSONFileName)
        {
            List<QueryDetails> QueryConfigs = new List<QueryDetails>();

            string jsonFile = GetJsonFilePath(JSONFilePathType, JSONFilePath, JSONFileName);

            string jsonContent = File.ReadAllText(jsonFile);
            var jsonObject = JsonConvert.DeserializeObject<JsonStructure>(jsonContent);

            foreach (JsonAlertStructure alert in jsonObject.Alerts)
            {
                QueryDetails QueryConfig = new QueryDetails();
                QueryConfig = new QueryDetails();
                QueryConfig.StartDate = alert.StartDate;
                QueryConfig.EndDate = alert.EndDate;
                QueryConfig.Interval = alert.Interval;
                QueryConfig.Window = alert.Window;
                QueryConfig.Query = alert.query;
                QueryConfigs.Add(QueryConfig);
            }
            return QueryConfigs;
        }

        public string GetJsonFilePath(string JSONFilePathType, string JSONFilePath, string JSONFileName)
        {
            string jsonFile = string.Empty;
            if (JSONFilePathType == "Physical")
            {
                jsonFile = Path.Combine(JSONFilePath, JSONFileName);
            }
            else if (JSONFilePathType == "InApplication")
            {
                //To get the location the assembly normally resides on disk or the install directory
                string path = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;

                //once you have the path you get the directory with:
                var directory = System.IO.Path.GetDirectoryName(path).Replace(@"file:\", "");

                jsonFile = Path.Combine(directory, JSONFilePath, JSONFileName);
            }
            else
            {
                throw new Exception("JSONFilePathType is not valid in app.config.");
            }

            return jsonFile;
        }
    }
}
