using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObjects.Xml
{
    public class SubMenu
    {
        /*
            <index>1</index>
            <display>DANH SÁCH ĐỐI TÁC</display>
            <link>/ImtSurplus/Partner</link>
            <active>1</active>
         */
        public int index { get; set; }
        public string display { get; set; }
        public string link { get; set; }
        public int active { get; set; }
    }
}
