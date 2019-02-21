using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceObjects
{
    public class GetDataResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public object Content { get; set; }
    }
}
