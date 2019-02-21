using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceObjects
{
    public class ServiceParam
    {
        public ServiceParam()
        {
            Name = "";
            Value = "";
        }
        public ServiceParam(string _name, string _value)
        {
            Name = _name;
            Value = _value;
        }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
