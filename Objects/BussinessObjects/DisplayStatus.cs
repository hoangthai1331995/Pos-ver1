using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObjects
{
    public class DisplayStatus : BusinessObject
    {
        public string TableRender { get; set; }
        public int StatusValue { get; set; }
        public string StatusDisplay { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
