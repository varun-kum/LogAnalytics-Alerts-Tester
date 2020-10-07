using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester.DTO
{
    /// <summary>
    /// Class for Query Result DTO
    /// </summary>
    public class LogResultDTO
    {
        public string ProcessingDateTime { get; set; }
        public long Count { get; set; }
        public bool Result { get; set; }
        public string Query { get; set; }
    }
}
