using AlertTester.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester.Process
{
    public interface IReadJSON
    {
        IEnumerable<IQueryDetails> ReadJSONFile(string JSONFilePathType, string JSONFilePath, string JSONFileName);
    }
}
