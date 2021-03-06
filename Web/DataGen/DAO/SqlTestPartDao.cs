using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eTraining.BussinessObjects;

namespace eTraining.DataObjects
{
    public class SqlTestPartDao : SqlDaoBase<TestPart>
    {
        public SqlTestPartDao()
        {
            TableName = "tblTestPart";
            EntityIDName = "TestPartId";
            StoreProcedurePrefix = "spTestPart_";
        }
        public SqlTestPartDao(string tableName, string entityIDName, string storeProcedurePrefix) : base(tableName, entityIDName, storeProcedurePrefix) { }
    }
}
