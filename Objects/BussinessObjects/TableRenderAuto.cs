using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObjects
{
    public class TableRenderAuto : BusinessObject
    {
		     public string TableName { get; set; } 

             public Int16 Status { get; set; } 

             public int PageSize { get; set; } 

             public DateTime CreatedDate { get; set; } 

             public DateTime UpdatedDate { get; set; } 


    }
}
