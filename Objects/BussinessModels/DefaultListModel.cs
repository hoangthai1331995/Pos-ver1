using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessModels
{
    public class DefaultListModel
    {
        public string TableName { get; set; }
        public string ObjectName { get { return !String.IsNullOrEmpty(TableName) ? TableName.Replace("tbl", "") : ""; } set { } }
        public string TargetArea { get; set; }
        public string ControllerName { get; set; }
        public bool IsAllowAddUpdate { get; set; }
    }
}
