using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eTraining.BussinessObjects;

namespace eTraining.DataObjects
{
    public class SqlProgramRankingDao : SqlDaoBase<ProgramRanking>
    {
        public SqlProgramRankingDao()
        {
            TableName = "tblProgramRanking";
            EntityIDName = "ProgramRankingId";
            StoreProcedurePrefix = "spProgramRanking_";
        }
        public SqlProgramRankingDao(string tableName, string entityIDName, string storeProcedurePrefix) : base(tableName, entityIDName, storeProcedurePrefix) { }
    }
}
