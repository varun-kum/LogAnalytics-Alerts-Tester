using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester.Interfaces
{
    public interface IConfigurationSetting<T>
    {
        T value { get; set; }

        T ReadConfigSetting(string key);
    }
}
