using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTraining.BussinessObjects
{
    public class ProgramTranning : BusinessObject
    {
		             public string ProgramName { get; set; } 

             public DateTime FromDate { get; set; } 

             public DateTime ToDate { get; set; } 

             public Int64 RanktingID { get; set; } 

             public Int64 TeacherID { get; set; } 

             public Int64 TrainingTypeID { get; set; } 

             public Int64 ObjectTrainingID { get; set; } 

             public Int64 UserTitleID { get; set; } 

             public Int64 DocumentID { get; set; } 

             public Int64 DepartmentID { get; set; } 

             public Int64 RegionID { get; set; } 

             public int Status { get; set; } 

             public DateTime CreatedDate { get; set; } 

             public Int64 CreatedUser { get; set; } 


    }
}
