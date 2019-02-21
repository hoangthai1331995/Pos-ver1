using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModelBussinessObjects.Xml
{
    public class TabAction
    {
        /*
            <dataBind>modal_add_contract</dataBind>
            <displayBind>Bổ sung hợp đồng</displayBind>
            <style>au-target btn btn-tag btn-tag-light btn-tag-rounded</style>
            <type>modal</type>
            <link>~/Areas/IMTSurplus/Views/Partner/_ViewDetail.cshtml</link>
            <icon>fa fa-edit</icon>
            <jsfunction>partner_get_single</jsfunction>
         */
        public string dataBind { get; set; }
        public string displayBind { get; set; }
        public string style { get; set; }
        public string type { get; set; }
        public string link { get; set; }
        public string icon { get; set; }
        public string jsfunction { get; set; }
        public string message { get; set; }
    }
}
