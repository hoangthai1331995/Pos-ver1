using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eTraining.BussinessObjects;

namespace eTraining.DataObjects
{
    public class SqlTrainingTypeDao : SqlDaoBase<TrainingType>
    {
        public SqlTrainingTypeDao()
        {
            TableName = "tblTrainingType";
            EntityIDName = "TrainingTypeId";
            StoreProcedurePrefix = "spTrainingType_";
        }
        public SqlTrainingTypeDao(string tableName, string entityIDName, string storeProcedurePrefix) : base(tableName, entityIDName, storeProcedurePrefix) { }
    }
}
