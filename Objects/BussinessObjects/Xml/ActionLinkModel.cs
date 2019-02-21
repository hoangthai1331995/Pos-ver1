using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObjects.Xml
{
    public class ActionLinkModel
    {
        /*
            <name>Detail</name>
            <display>Chi tiết</display>
            <type>popup</type>
            <link>~/Areas/IMTAccount/Views/Home/_Detail.cshtml</link>
            <style>info</style>
            <icon></icon>
            <message></message>
            <target></target>
         */
        public string name { get; set; }
        public string display { get; set; }
        public string type { get; set; }
        public string link { get; set; }
        public string style { get; set; }
        public string icon { get; set; }
        public string message { get; set; }
        public string target { get; set; }
    }
}
