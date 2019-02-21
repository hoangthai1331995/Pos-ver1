using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceObjects
{
    public class PutDataRequest
    {
        public PutDataRequest()
        {
            ExtensionParam = new List<string>();
            DenyFields = "";
        }
        public object RequestObject { get; set; }
        public List<string> ExtensionParam { get; set; }
        public string DenyFields { get; set; } // "CreatedDate,UpdatedDate"
        public string DataSource { get; set; }
    }
}
