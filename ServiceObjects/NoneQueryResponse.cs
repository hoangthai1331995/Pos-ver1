using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceObjects
{
    public class NoneQueryResponse : BusinessObject
    {
        public string SSID { get; set; }
        public string RowID { get; set; }
        public string StatusCode { get; set; }
        public string StatusMess { get; set; }
    }
}
