using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parsing_kibana_logs
{
    public class LogItem
    {
        [Name("@timestamp")]
        public string DateStamp { get; set; }

        [Name("message")]
        public string Message { get; set; }
    }
}
