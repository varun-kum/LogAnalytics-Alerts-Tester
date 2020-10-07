using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlertTester.Interfaces
{
    public interface IConfiguration
    {
        ILogAnalyticsProviderDetails LogAnalyticsProviderConfig { get; set; }
        IApplicationDetails ApplicationConfig { get; set; }
        IEnumerable<IQueryDetails> QueryConfigs { get; set; }
        void ReadConfig();
        void ReadJSONFile();

    }
}
