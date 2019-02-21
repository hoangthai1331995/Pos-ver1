using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessModels
{
    public class PutDataResponse
    {
        public PutDataResponse()
        {
            StatusCode = "";
            StatusMess = "";
        }
        public string StatusCode { get; set; }
        public string StatusMess { get; set; }
    }
}
