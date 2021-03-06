using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eTraining.BussinessObjects;

namespace eTraining.DataObjects
{
    public class SqlUserTestResultDao : SqlDaoBase<UserTestResult>
    {
        public SqlUserTestResultDao()
        {
            TableName = "tblUserTestResult";
            EntityIDName = "UserTestResultId";
            StoreProcedurePrefix = "spUserTestResult_";
        }
        public SqlUserTestResultDao(string tableName, string entityIDName, string storeProcedurePrefix) : base(tableName, entityIDName, storeProcedurePrefix) { }
    }
}
