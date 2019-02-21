using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class SqlDisplayStatusDao : SqlDaoBase<DisplayStatus>
    {
        public SqlDisplayStatusDao()
        {
            TableName = "cofDisplayStatus";
            EntityIDName = "DisplayStatusId";
            StoreProcedurePrefix = "cofDisplayStatus_";
        }
        public SqlDisplayStatusDao(string tableName, string entityIDName, string storeProcedurePrefix) : base(tableName, entityIDName, storeProcedurePrefix) { }
    }
}
