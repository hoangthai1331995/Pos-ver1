using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceObjects
{
    public class NoneQueryRequest
    {
        public NoneQueryRequest()
        {
            Params = new List<string>();
            DataSource = "";
        }
        public List<string> Params { get; set; }
        public string DataSource { get; set; }
    }
}
