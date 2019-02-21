using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceObjects
{
    public class GetSelectListRequest
    {
        public GetSelectListRequest(string _tableName, string _fieldDisplay, string _fieldValue)
        {
            TableName = _tableName;
            FieldDisplay = _fieldDisplay;
            FieldValue = _fieldValue;
        }
        public string TableName { get; set; }
        public string FieldDisplay { get; set; }
        public string FieldValue { get; set; }
    }
}
