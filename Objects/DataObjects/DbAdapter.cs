using ServiceObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;


namespace DataObjects
{
    //
    // Here is some skeleton code demonstrating how you could manange multiple database connections.
    // Note: this code is not actually used in Patterns in Action.
    //

    /// <summary>
    ///  Connects with Database 1
    /// </summary>
    public static class DbAdapter1
    {
        private static readonly string connectionStringName = ConfigurationManager.AppSettings.Get("ConnectionStringName");
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;

        #region Fast data readers

        public static T Read<T>(string sql, Func<IDataReader, T> make, bool isStoreProcedure = true, object[] parms = null)
        {
            return DbCore.Read(sql, make, connectionString, isStoreProcedure, parms);
        }

        public static List<T> ReadList<T>(string sql, Func<IDataReader, T> make, bool isStoreProcedure = true, object[] parms = null)
        {
            return DbCore.ReadList(sql, make, connectionString, isStoreProcedure, parms);
        }

        public static DataTable ReadDataTable(string sql, bool isStoreProcedure = true, object[] parms = null)
        {
            return DbCore.ReadDataTable(sql, connectionString, isStoreProcedure, parms);
        }

        public static int GetCount(string sql, bool isStoreProcedure = true, object[] parms = null)
        {
            return DbCore.GetCount(sql, connectionString, isStoreProcedure, parms);
        }

        public static object GetScalar(string sql, bool isStoreProcedure = true, object[] parms = null)
        {
            return DbCore.GetScalar(sql, connectionString, isStoreProcedure, parms);
        }

        public static int ExecuteNonQuery(string sql, bool isStoreProcedure = true, object[] parms = null)
        {
            return DbCore.ExecuteNonQuery(sql, connectionString, isStoreProcedure, parms);
        }

        public static object ExcecuteScalar(string sql, bool isStoreProcedure = true, object[] parms = null)
        {
            return DbCore.ExcecuteScalar(sql, connectionString, isStoreProcedure, parms);
        }

        #endregion Fast data readers

        #region Data update section

        #endregion Data update section
    }

    /// <summary>
    /// Connects with Database 2
    /// </summary>
    public static class DbAdapter2
    {
        private static readonly string connectionStringName = ConfigurationManager.AppSettings.Get("InsideConnectionStringName");
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;

        #region Fast data readers

        public static T Read<T>(string sql, Func<IDataReader, T> make, bool isStoreProcedure = true, object[] parms = null)
        {
            return DbCore.Read(sql, make, connectionString, isStoreProcedure, parms);
        }

        public static List<T> ReadList<T>(string sql, Func<IDataReader, T> make, bool isStoreProcedure = true, object[] parms = null)
        {
            return DbCore.ReadList(sql, make, connectionString, isStoreProcedure, parms);
        }

        public static DataTable ReadDataTable(string sql, bool isStoreProcedure = true, object[] parms = null)
        {
            return DbCore.ReadDataTable(sql, connectionString, isStoreProcedure, parms);
        }

        public static int GetCount(string sql, bool isStoreProcedure = true, object[] parms = null)
        {
            return DbCore.GetCount(sql, connectionString, isStoreProcedure, parms);
        }

        public static object GetScalar(string sql, bool isStoreProcedure = true, object[] parms = null)
        {
            return DbCore.GetScalar(sql, connectionString, isStoreProcedure, parms);
        }

        public static int ExecuteNonQuery(string sql, bool isStoreProcedure = true, object[] parms = null)
        {
            return DbCore.ExecuteNonQuery(sql, connectionString, isStoreProcedure, parms);
        }

        public static object ExcecuteScalar(string sql, bool isStoreProcedure = true, object[] parms = null)
        {
            return DbCore.ExcecuteScalar(sql, connectionString, isStoreProcedure, parms);
        }

        #endregion Fast data readers
    }

    public static class DbCore
    {
        private static readonly string dataProvider = ConfigurationManager.AppSettings.Get("DataProvider");
        private static readonly DbProviderFactory factory = DbProviderFactories.GetFactory(dataProvider);

        #region Fast data readers

        /// <summary>
        /// Fast read of individual item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="make"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static T Read<T>(string sql, Func<IDataReader, T> make, string connectionString, bool isStoreProcedure = true,
                                object[] parms = null)
        {
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;

                using (var command = factory.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = sql;
                    if (isStoreProcedure)
                        command.CommandType = CommandType.StoredProcedure;
                    command.SetParameters(parms);  // Extension method
                    connection.Open();
                    T t = default(T);
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                        t = make(reader);
                    return t;
                }
            }
        }

        /// <summary>
        /// Fast read of list of items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="make"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static List<T> ReadList<T>(string sql, Func<IDataReader, T> make, string connectionString, bool isStoreProcedure = true,
                                          object[] parms = null)
        {
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;

                using (var command = factory.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = sql;
                    if (isStoreProcedure)
                        command.CommandType = CommandType.StoredProcedure;
                    command.SetParameters(parms);

                    command.CommandTimeout = 60;
                    connection.Open();

                    var list = new List<T>();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                        list.Add(make(reader));
                    return list;
                }
            }
        }

        public static DataTable ReadDataTable(string sql, string connectionString, bool isStoreProcedure = true,
                                object[] parms = null)
        {
            DataTable dt = new DataTable();
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;

                using (var command = factory.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = sql;
                    if (isStoreProcedure)
                        command.CommandType = CommandType.StoredProcedure;
                    command.SetParameters(parms);  // Extension method
                    connection.Open();
                    var reader = command.ExecuteReader();
                    dt.Load(reader);
                }
            }
            return dt;
        }

        /// <summary>
        /// Gets a record count.
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static int GetCount(string sql, string connectionString, bool isStoreProcedure = true, object[] parms = null)
        {
            return Convert.ToInt32(GetScalar(sql, connectionString, isStoreProcedure, parms));
        }

        /// <summary>
        /// Gets any scalar value from the database.
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static object GetScalar(string sql, string connectionString, bool isStoreProcedure = true, object[] parms = null)
        {
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;

                using (var command = factory.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = sql;
                    if (isStoreProcedure)
                        command.CommandType = CommandType.StoredProcedure;
                    command.SetParameters(parms);

                    connection.Open();
                    return command.ExecuteScalar();
                }
            }
        }

        public static int ExecuteNonQuery(string sql, string connectionString, bool isStoreProcedure = true, object[] parms = null)
        {
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;

                using (var command = connection.CreateCommand())
                {
                    command.Connection = connection;
                    if (isStoreProcedure)
                        command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = sql;
                    command.SetParameters(parms);

                    connection.Open();

                    return command.ExecuteNonQuery();
                }
            }
        }

        public static object ExcecuteScalar(string sql, string connectionString, bool isStoreProcedure = true, object[] parms = null)
        {
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                using (var command = connection.CreateCommand())
                {
                    command.Connection = connection;
                    if (isStoreProcedure)
                        command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = sql;
                    command.SetParameters(parms);
                    connection.Open();
                    return command.ExecuteScalar();
                }
            }
        }

        public static NoneQueryResponse ReadNoneQuery(string sql, string connectionString, bool isStoreProcedure = true,
                               object[] parms = null)
        {
            NoneQueryResponse response = new NoneQueryResponse();
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;

                using (var command = factory.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = sql;
                    if (isStoreProcedure)
                        command.CommandType = CommandType.StoredProcedure;
                    command.SetParameters(parms);  // Extension method
                    connection.Open();

                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        response.SSID = reader.GetString(reader.GetOrdinal("SSID"));
                        response.RowID = reader.GetString(reader.GetOrdinal("RowID"));
                        response.StatusCode = reader.GetString(reader.GetOrdinal("StatusCode"));
                        response.StatusMess = reader.GetString(reader.GetOrdinal("StatusMess"));
                    }
                    return response;
                }
            }
        }

        public static string GetPageSQL2005(string tableOrViewName, int pageSize, int selectedPage, string whereExpression, string sortExpression)
        {
            string sql = string.Format(@"SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY {0}) AS rownum, * FROM {1}) AS tmp 
                                         WHERE rownum >=  {2}  AND rownum <= {3} {4}",
                                         sortExpression, tableOrViewName, ((selectedPage - 1) * pageSize + 1),
                                         (selectedPage * pageSize), (whereExpression.Trim() != "" ? " AND " + whereExpression : ""));
            return sql;
        }

        #endregion

        #region Data update section

        /// <summary>
        /// Inserts an item into the database
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static long Insert(string sql, string connectionString, object[] parms = null)
        {
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;

                using (var command = factory.CreateCommand())
                {
                    command.Connection = connection;
                    command.SetParameters(parms);                     // Extension method  
                    command.CommandText = sql.AppendIdentitySelect(); // Extension method

                    connection.Open();

                    // MS Access does not support multistatement batch commands. Issue a separate query.
                    if (dataProvider == "System.Data.OleDb")
                    {
                        command.ExecuteNonQuery();
                        command.CommandText = "SELECT @@IDENTITY";
                    }

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        /// <summary>
        /// Updates an item in the database
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        public static void Update(string sql, string connectionString, object[] parms = null)
        {
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;

                using (var command = factory.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = sql;
                    command.SetParameters(parms);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Deletes an item from the database.
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        public static void Delete(string sql, string connectionString, object[] parms = null)
        {
            Update(sql, connectionString, parms);
        }

        #endregion

        #region Extension methods

        /// <summary>
        /// Extension method: Appends the db specific syntax to sql string 
        /// for retrieving newly generated identity (autonumber) value.
        /// </summary>
        /// <param name="sql">The sql string to which to append syntax.</param>
        /// <returns>Sql string with identity select.</returns>
        private static string AppendIdentitySelect(this string sql)
        {
            switch (dataProvider)
            {
                // Microsoft Access does not support multistatement batch commands
                case "System.Data.OleDb": return sql;
                case "System.Data.SqlClient": return sql + ";SELECT SCOPE_IDENTITY()";
                case "System.Data.OracleClient": return sql + ";SELECT MySequence.NEXTVAL FROM DUAL";
                default: return sql + ";SELECT @@IDENTITY";
            }
        }

        /// <summary>
        /// Extention method. Adds query parameters to command object.
        /// </summary>
        /// <param name="command">Command object.</param>
        /// <param name="parms">Array of name-value query parameters.</param>
        private static void SetParameters(this DbCommand command, object[] parms)
        {
            if (parms != null && parms.Length > 0)
            {
                // NOTE: Processes a name/value pair at each iteration
                for (int i = 0; i < parms.Length; i += 2)
                {
                    string name = parms[i].ToString();

                    // No empty strings to the database
                    //if (parms[i + 1] is string && (string)parms[i + 1] == "")
                    //    parms[i + 1] = null;

                    // If null, set to DbNull
                    object value = parms[i + 1] ?? DBNull.Value;

                    var dbParameter = command.CreateParameter();
                    dbParameter.ParameterName = name;
                    dbParameter.Value = value;

                    command.Parameters.Add(dbParameter);
                }
            }
        }

        #endregion
    }
}
