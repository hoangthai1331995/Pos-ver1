using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eTraining.BussinessObjects;

namespace eTraining.DataObjects
{
    public class SqlProgramTranningDao : SqlDaoBase<ProgramTranning>
    {
        public SqlProgramTranningDao()
        {
            TableName = "tblProgramTranning";
            EntityIDName = "ProgramTranningId";
            StoreProcedurePrefix = "spProgramTranning_";
        }
        public SqlProgramTranningDao(string tableName, string entityIDName, string storeProcedurePrefix) : base(tableName, entityIDName, storeProcedurePrefix) { }
    }
}
