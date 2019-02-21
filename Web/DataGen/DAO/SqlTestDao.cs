using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eTraining.BussinessObjects;

namespace eTraining.DataObjects
{
    public class SqlTestDao : SqlDaoBase<Test>
    {
        public SqlTestDao()
        {
            TableName = "tblTest";
            EntityIDName = "TestId";
            StoreProcedurePrefix = "spTest_";
        }
        public SqlTestDao(string tableName, string entityIDName, string storeProcedurePrefix) : base(tableName, entityIDName, storeProcedurePrefix) { }
    }
}
