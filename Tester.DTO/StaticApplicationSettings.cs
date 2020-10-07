using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester.DTO
{
    /// <summary>
    /// Unique Guid for each Run
    /// </summary>
    public class StaticApplicationSettings
    {
        public static Guid TransactionGuid { get; set; }
    }
}
