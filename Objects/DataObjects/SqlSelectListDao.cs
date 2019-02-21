using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class SqlSelectListDao : SqlDaoBase<SelectList>
    {
        public SqlSelectListDao()
        {
            TableName = "";
            EntityIDName = "";
            StoreProcedurePrefix = "spSelectList_";
        }
        public SqlSelectListDao(string tableName, string entityIDName, string storeProcedurePrefix) : base(tableName, entityIDName, storeProcedurePrefix) { }
        public List<SelectList> GetSelectLists(string storeProcedure = "")
        {
            object[] parms = new object[]
            {
                "@storeProcedure",
                storeProcedure
            };
            return DbAdapter1.ReadList<SelectList>(StoreProcedurePrefix + "GetList", Make, true, parms);
        }
    }
}
