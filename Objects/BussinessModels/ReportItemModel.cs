using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessModels
{
    public class ReportItemModel
    {
        public string pkey { get; set; }
        public List<fitem> filter { get; set; }
        public List<litem> list { get; set; }
        public xmlPaging paging { get; set; }
        public DataTable data { get; set; }
    }

    public class fitem
    {
        public int index { get; set; }
        public string key { get; set; }
        public string value { get; set; }
        public string type { get; set; }
        public string holder { get; set; }
        public string display { get; set; }
        public string defaultvalue { get; set; }
        public string datasource { get; set; }
    }

    public class litem
    {
        public string key { get; set; }
        public string display { get; set; }
    }

    public class xmlPaging
    {
        public int pagesize { get; set; }
        public int pagenum { get; set; }
    }
}
