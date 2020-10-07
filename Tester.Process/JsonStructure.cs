using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester.Process
{
    /// <summary>
    /// Class to deserialize Query Json
    /// </summary>
    public class JsonStructure
    {
        public string contentVersion { get; set; }
        public List<JsonAlertStructure> Alerts { get; set; }
    }
}
