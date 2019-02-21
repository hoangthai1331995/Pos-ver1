using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceObjects
{
    public class GetDataRequest
    {
        public int RequestType { get; set; }
        public string RequestIp { get; set; }
        public string RequestUrl { get; set; }
        public string Connection { get; set; }
        public GetDataRequest()
        {
            Params = new List<string>();
        }
        public List<string> Params { get; set; }
        public string Module { get; set; }
        public string Controller { get; set; }
        public string MethodName { get; set; }
        public string DataSource { get; set; }
    }
}
