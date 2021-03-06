using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BussinessObjects;

namespace DataObjects
{
    public class SqlTableRenderAutoDao : SqlDaoBase<TableRenderAuto>
    {
        public SqlTableRenderAutoDao()
        {
            TableName = "cofTableRenderAuto";
            EntityIDName = "TableRenderAutoId";
            StoreProcedurePrefix = "spTableRenderAuto_";
        }
        public SqlTableRenderAutoDao(string tableName, string entityIDName, string storeProcedurePrefix) : base(tableName, entityIDName, storeProcedurePrefix) { }
        public int Clear(string tableName = "")
        {
            object[] parms = new object[] { "@tableName", tableName };
            return Convert.ToInt32(DbAdapter1.ExcecuteScalar("spTableRenderAuto_Clear", true, parms));
        }
        public TableRenderAuto GetTableRenderAuto(string tableName = "")
        {
            object[] parms = new object[]
            {
                "@tableName",
                tableName
            };
            return DbAdapter1.Read<TableRenderAuto>(StoreProcedurePrefix + "GetByTableName", Make, true, parms);
        }
    }
}
