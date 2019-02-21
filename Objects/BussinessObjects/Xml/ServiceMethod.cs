using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObjects.Xml
{
    public class ServiceMethods
    {
       public List<ServiceMethod> Methods { get; set; }
    }

    public class ServiceMethod
    {
        public int index { get; set; }
        public string key { get; set; }
        public string datasource { get; set; }
    }
}
