using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eTraining.BussinessObjects;

namespace eTraining.DataObjects
{
    public class SqlCheckDetailDao : SqlDaoBase<CheckDetail>
    {
        public SqlCheckDetailDao()
        {
            TableName = "tblCheckDetail";
            EntityIDName = "CheckDetailId";
            StoreProcedurePrefix = "spCheckDetail_";
        }
        public SqlCheckDetailDao(string tableName, string entityIDName, string storeProcedurePrefix) : base(tableName, entityIDName, storeProcedurePrefix) { }
    }
}
