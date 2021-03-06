using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eTraining.BussinessObjects;

namespace eTraining.DataObjects
{
    public class SqlObjectTrainingDao : SqlDaoBase<ObjectTraining>
    {
        public SqlObjectTrainingDao()
        {
            TableName = "tblObjectTraining";
            EntityIDName = "ObjectTrainingId";
            StoreProcedurePrefix = "spObjectTraining_";
        }
        public SqlObjectTrainingDao(string tableName, string entityIDName, string storeProcedurePrefix) : base(tableName, entityIDName, storeProcedurePrefix) { }
    }
}
