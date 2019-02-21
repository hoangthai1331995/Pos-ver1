using BussinessObjects;
using Helpers.Paging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessModels
{
    public class ListInfo
    {
        public ListInfo()
        {
            Title = "";
            DataInfo = new DataTable();
            Resources = new List<ResourceListAuto>();
            PagingInfo = new Paging();
        }
        public string Title { get; set; }
        public string ActiveController { get; set; }
        public string ActiveArea { get; set; }
        public List<ResourceListAuto> Resources { get; set; }
        public DataTable DataInfo { get; set; }
        public Paging PagingInfo { get; set; }
    }
}
