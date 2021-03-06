using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eTraining.BussinessObjects;

namespace eTraining.DataObjects
{
    public class SqlUserTitleDao : SqlDaoBase<UserTitle>
    {
        public SqlUserTitleDao()
        {
            TableName = "tblUserTitle";
            EntityIDName = "UserTitleId";
            StoreProcedurePrefix = "spUserTitle_";
        }
        public SqlUserTitleDao(string tableName, string entityIDName, string storeProcedurePrefix) : base(tableName, entityIDName, storeProcedurePrefix) { }
    }
}
