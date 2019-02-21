using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObjects.Xml
{
    public class FilterParam
    {
        public FilterParam()
        {
            name = "";
            display = "";
            type = "";
            databinding = "";
            value = "";
            html = "";
            tablebinding = "";
            fnamebinding = "";
            fvaluebinding = "";
            pRow = "";
            pCol = "";
        }
        public FilterParam(string _name, string _value)
        {
            name = _name;
            value = _value;
        }
        public FilterParam(string _name, string _display, string _type,string _value, string _databinding, string _html, string _tablebinding, string _fnamebinding, string _fvaluebinding)
        {
            name = _name;
            display = _display;
            type = _type;
            databinding = _databinding;
            value = _value;
            html = _html;
            tablebinding = _tablebinding;
            fnamebinding = _fnamebinding;
            fvaluebinding = _fvaluebinding;
        }
        public string name { get; set; }
        public string display { get; set; }
        public string type { get; set; }
        public string databinding { get; set; }
        public string tablebinding { get; set; }
        public string fnamebinding { get; set; }
        public string fvaluebinding { get; set; }
        public string value { get; set; }
        public string html { get; set; }
        public string pRow { get; set; }
        public string pCol { get; set; }
    }
}
