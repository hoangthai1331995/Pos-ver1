using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTraining.BussinessObjects
{
    public class DepartmentRegion : BusinessObject
    {
		             public Int64 RegionID { get; set; } 

             public Int64 DepartmentID { get; set; } 

             public int Status { get; set; } 

             public DateTime CreatedDate { get; set; } 

             public string CreatedUser { get; set; } 


    }
}
