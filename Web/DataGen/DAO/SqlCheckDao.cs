using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eTraining.BussinessObjects;

namespace eTraining.DataObjects
{
    public class SqlCheckDao : SqlDaoBase<Check>
    {
        public SqlCheckDao()
        {
            TableName = "tblCheck";
            EntityIDName = "CheckId";
            StoreProcedurePrefix = "spCheck_";
        }
        public SqlCheckDao(string tableName, string entityIDName, string storeProcedurePrefix) : base(tableName, entityIDName, storeProcedurePrefix) { }
    }
}
