using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObjects
{
    public class NI10_Menu : BusinessObject
    {
        public long MenuId { get; set; }
        public long FunctionId { get; set; }
        public string MenuName { get; set; }
        public long ParentID { get; set; }
        public string Link { get; set; }
        public int Sort { get; set; }
        public int MenuLevel { get; set; }
        public string Icon { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedById { get; set; }
    }
}
