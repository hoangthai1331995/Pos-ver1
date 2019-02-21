using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using Helpers.BlankConnection;

namespace Configulation.Models
{
    public class TableInfo
    {
        public string Name { get; set; }

        public List<ColumnInfo> Colums { get; set; }

        public string PrimaryKey { get; set; }

        // store procedure 
        public string StoredProcedure { get; set; }

        public string SpSelect { get; set; }

        public string SpAddUpdate { get; set; }

        private ETDataConnection gConn;

        //constructor
        public TableInfo(string name, ETDataConnection gConn)
        {
            this.Name = name;
            this.gConn = gConn;

            Colums = new List<ColumnInfo>();

            // lấy danh sách cột
            string strQuery = "select * " +
                "from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='" + Name + "'";
            DataTable dt = gConn.GetDataTable(strQuery);

            // lấy danh sách các cột là khóa chính
            strQuery = "select COLUMN_NAME from INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE where TABLE_NAME = '" + Name + "' and CONSTRAINT_NAME like N'PK_%'";
            PrimaryKey = gConn.ExecuteScalar(strQuery).ToString();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ColumnInfo column = new ColumnInfo();
                column.Name = dt.Rows[i]["COLUMN_NAME"].ToString();
                column.DataType = dt.Rows[i]["DATA_TYPE"].ToString();
                if (dt.Rows[i]["CHARACTER_MAXIMUM_LENGTH"].ToString() != "")
                    column.MaxValue = int.Parse(dt.Rows[i]["CHARACTER_MAXIMUM_LENGTH"].ToString());
                else
                    column.MaxValue = 0;

                if (dt.Rows[i]["NUMERIC_PRECISION"].ToString() != "")
                    column.NumericPrecision = int.Parse(dt.Rows[i]["NUMERIC_PRECISION"].ToString());
                else
                    column.NumericPrecision = 0;

                if (dt.Rows[i]["NUMERIC_SCALE"].ToString() != "")
                    column.NumericScale = int.Parse(dt.Rows[i]["NUMERIC_SCALE"].ToString());
                else
                    column.NumericScale = 0;

                column.Default = dt.Rows[i]["COLUMN_DEFAULT"].ToString();

                if (column.Name != PrimaryKey)
                    Colums.Add(column);
            }
            this.StoredProcedure = "";
            this.SpSelect = GenerateStoredProcedureGetFilter(this);
            this.SpAddUpdate = GenerateStoredProcedureAddUpdate(this);
        }

        public void CreateStoreProcedure()
        {
            if (SpSelect != "")
                gConn.ExecuteNoneQuery(this.SpSelect);
            if (SpAddUpdate != "")
                gConn.ExecuteNoneQuery(this.SpAddUpdate);
        }

        private string GenerateStoredProcedureSelect(TableInfo table)
        {
            string sql = "";
            sql += "CREATE PROC [dbo].[sp" + ConvertName(table.Name) + "_Get" + ConvertName(table.Name) + "] \n";
            sql += "@" + table.PrimaryKey.ToLower() + " bigint \n";
            sql += "AS \n";
            sql += "BEGIN \n";
            sql += "    SELECT * FROM " + table.Name + " WHERE " + table.PrimaryKey + "=@" + table.PrimaryKey.ToLower() + " \n";
            sql += "END \n";
            return sql;
        }

        private string GenerateStoredProcedureAddUpdate(TableInfo table)
        {
            string sql = "";
            sql += "CREATE PROCEDURE [dbo].[sp" + ConvertName(table.Name) + "_AddUpdate]  \n";
            sql += "    -- Add the parameters for the stored procedure here \n";
            sql += "    @id bigint = 0, \n";
            for (int i = 0; i < table.Colums.Count; i++)
            {
                ColumnInfo col = table.Colums[i];
                sql += "    @" + col.Name.ToLower() + " " + col.DataType;
                if (col.MaxValue == -1)
                    sql += "(MAX)";
                if (col.MaxValue > 0)
                    sql += "(" + col.MaxValue + ")";

                if (col.MaxValue == 0)
                {
                    if (col.NumericPrecision != 0 && col.NumericScale != 0)
                    {
                        sql += "(" + col.NumericPrecision + "," + col.NumericScale + ")";
                    }
                }
                if (i < table.Colums.Count - 1)
                    sql += ", \n";
                else
                    sql += " \n";

            }
            sql += "AS \n";
            sql += "BEGIN \n";
            sql += "    -- SET NOCOUNT ON added to prevent extra result sets from \n";
            sql += "    -- interfering with SELECT statements. \n";
            sql += "SET NOCOUNT ON; \n";
            sql += "    if (@id <= 0) \n";
            sql += "    begin \n";
            sql += "    insert into " + table.Name + "( \n";
            for (int j = 0; j < table.Colums.Count; j++)
            {
                sql += "            " + table.Colums[j].Name;
                if (j < table.Colums.Count - 1)
                    sql += ", \n";
                else
                    sql += ") \n";
            }
            sql += "    values(" + "\n";

            for (int k = 0; k < table.Colums.Count; k++)
            {
                sql += "            @" + table.Colums[k].Name.ToLower();
                if (k < table.Colums.Count - 1)
                    sql += ", \n";
                else
                    sql += "\n";
            }
            sql += "                ) \n";
            sql += "        set @id = @@identity \n";
            sql += "    end \n";
            sql += "    else \n";
            sql += "    begin \n";
            sql += "        update " + table.Name + " set \n";
            for (int l = 0; l < table.Colums.Count; l++)
            {
                sql += "        " + table.Colums[l].Name + " = " + "@" + table.Colums[l].Name.ToLower();
                if (l < table.Colums.Count - 1)
                    sql += ", \n";
                else
                    sql += "\n";
            }
            sql += "        where " + table.PrimaryKey + " = @id \n";
            sql += "    end \n";
            sql += "    -- Insert statements for procedure here \n";
            sql += "    SELECT @id as " + table.PrimaryKey + " \n";
            sql += "END";
            return sql;
        }

        private string GenerateStoredProcedureGetFilter(TableInfo table)
        {
            string sql = "";
            sql += "CREATE PROCEDURE [dbo].[sp" + ConvertName(table.Name) + "_GetFilter]  \n";
            sql += "@pagesize int, \n";
            sql += "@pagenum int \n";
            sql += "AS \n";
            sql += "BEGIN \n";
            sql += "   declare @min as bigint \n";
            sql += "   declare @max as bigint \n";
            sql += "   declare @totalRec as bigint \n";
            sql += "   if (@pageSize = 0) --Get all list, now paging \n";
            sql += "   begin \n";
            sql += "        set @min = 0 \n";
            sql += "        set @max = 100000 \n";
            sql += "   end \n";
            sql += "   else \n";
            sql += "   begin \n";
            sql += "        set @min = (@pagenum - 1) * @pageSize + 1 \n";
            sql += "        set @max = @pagenum * @pageSize \n";
            sql += "   end \n";
            sql += "   select @totalRec = COUNT(*) \n";
            sql += "   from " + table.Name + " \n";
            sql += "   where 1 = 1 \n";
            sql += "   --------------------------------------------- \n";
            sql += "   ---- Conditional here ----------------------- \n";
            sql += "   --------------------------------------------- \n";
            sql += "   select @totalRec AS TotalRec,* \n";
            sql += "   from ( \n";
            sql += "            select ROW_NUMBER() OVER(ORDER BY a.CreatedDate desc) AS RowNum, a.* \n";
            sql += "            from [" + table.Name + "] a \n";
            sql += "            where 1 = 1 \n";
            sql += "            --------------------------------------------- \n";
            sql += "            ---- Conditional here ----------------------- \n";
            sql += "            --------------------------------------------- \n";
            sql += "          ) " + table.Name + "Temp \n";
            sql += "   where   RowNum BETWEEN @min AND @max \n";
            sql += "   order by CreatedDate desc \n";
            sql += "END \n";
            return sql;
        }

        private string ConvertName(string tableName)
        {
            return tableName.Replace("tbl", "").Replace("cof", "");
        }
    }
}
