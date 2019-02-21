using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObjects.Xml
{
    public class ListField
    {
        /*
           <name>ParamId</name>
           <display>Tên thuộc tính</display>
           <type>text</type>
       */
        public ListField()
        {

        }
        public ListField(int _index, string _name, string _display)
        {
            index = _index;
            name = _name;
            display = _display;
        }
        public int index { get; set; }
        public string name { get; set; }
        public string display { get; set; }
        public string type { get; set; }
        public string group { get; set; }
        public List<FieldDetail> Details { get; set; }
    }

    public class FieldDetail
    {
        /*
         <detail>
            <fieldId>statusId</fieldId>
            <dataBind>INAU</dataBind>
            <displayBind>Chưa duyệt</displayBind>
            <background>label label-default</background>
          </detail>
         */
        public string dataBind { get; set; }
        public string displayBind { get; set; }
        public string style { get; set; }
        public string type { get; set; }
        public string link { get; set; }
        public string icon { get; set; }
        public string jsfunction { get; set; }
        public string message { get; set; }
        public string conditionTarget { get; set; }
        public string conditionValue { get; set; }
    }
}
