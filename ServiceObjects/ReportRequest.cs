using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceObjects
{
    public class ReportRequest
    {
        public ReportRequest()
        {
            Params = new List<string>();
        }
        public string Key { get; set; }
        public List<string> Params { get; set; }
    }
}
