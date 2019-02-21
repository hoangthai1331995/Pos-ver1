using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using System.Reflection;
using System.ComponentModel;
using BussinessObjects;

namespace DataObjects
{
    public abstract class SqlDaoBase<T> where T : BusinessObject
    {
        private string mTableName;
        private string mEntityIDName;
        private string mStoreProcedurePrefix;

        protected string TableName
        {
            get { return mTableName; }
            set { mTableName = value; }
        }

        protected string EntityIDName
        {
            get { return mEntityIDName; }
            set { mEntityIDName = value; }
        }

        protected string StoreProcedurePrefix
        {
            get { return mStoreProcedurePrefix; }
            set { mStoreProcedurePrefix = value; }
        }

        protected SqlDaoBase() { }

        protected SqlDaoBase(string tableName, string entityIDName, string storeProcedurePrefix)
        {
            mTableName = tableName;
            mEntityIDName = entityIDName;
            mStoreProcedurePrefix = storeProcedurePrefix;
        }

        public virtual bool Insert(T businessObject)
        {
            string sql = mStoreProcedurePrefix + "AddUpdate";
            businessObject.ID = Extensions.AsLong(DbAdapter1.ExcecuteScalar(sql, true, Take(businessObject)));

            if (businessObject.ID > 0)
                return true;
            return false;
        }

        public virtual bool Update(T businessObject)
        {
            string sql = mStoreProcedurePrefix + "AddUpdate";
            businessObject.ID = Extensions.AsLong(DbAdapter1.ExcecuteScalar(sql, true, Take(businessObject)));

            if (businessObject.ID > 0)
                return true;
            return false;
        }

        public virtual bool Delete(T businessObject)
        {
            string sql = string.Format(@"DELETE FROM {0} WHERE {1} = @ID", mTableName, mEntityIDName);
            object[] parms = { "@ID", businessObject.ID };

            if (DbAdapter1.ExecuteNonQuery(sql, false, parms) > 0)
                return true;
            return false;
        }

        public virtual bool Delete(long entityId)
        {
            string sql = string.Format(@"DELETE FROM {0} WHERE {1} = @ID", mTableName, mEntityIDName);
            object[] parms = { "@ID", entityId };

            if (DbAdapter1.ExecuteNonQuery(sql, false, parms) > 0)
                return true;
            return false;
        }

        public virtual IList<T> GetListByCustomDataSource(object[] parms, string dataSource)
        {
            return DbAdapter1.ReadList(dataSource, Make, true, parms);
        }

        public virtual T GetSingleByCustomDataSource(object[] parms, string dataSource)
        {
            return DbAdapter1.Read(dataSource, Make, true, parms);
        }

        public virtual int UpdateByCustomDataSource(object[] parms, string dataSource)
        {
            return Convert.ToInt32(DbAdapter1.ExcecuteScalar(dataSource, true, parms));
        }

        public virtual DataTable GetDataTable(object[] parms, string sql)
        {
            
            return DbAdapter1.ReadDataTable(sql, true, parms);
        }

        public virtual T GetSingle(long entityID)
        {
            string sql = string.Format(@"SELECT * FROM {0} WHERE {1} = @EntityID", mTableName, mEntityIDName);
            object[] parms = { "@EntityID", entityID };

            return DbAdapter1.Read(sql, Make, false, parms);
        }

        public virtual T GetByID(Int64 id) //Vu.L
        {
            string sql = StoreProcedurePrefix + "Get" + mTableName.Replace("tbl", "");
            object[] parms = { "@" + mEntityIDName.ToLower(), id };
            return DbAdapter1.Read(sql, Make, true, parms);
        }

        public virtual IList<T> GetByColumnName(string columnName, object value) 
        {
            string sql = string.Format(@"SELECT * FROM {0} p where p.{1} = @" + columnName.ToLower(), TableName, columnName);
            object[] parms = { "@" + columnName.ToLower(), value };
            return DbAdapter1.ReadList(sql, Make, false, parms);
        }

        public virtual IList<T> GetByColumnNameLike(string columnName, object value) 
        {
            string sql = string.Format(@"SELECT * FROM {0} p WHERE LOWER(p.{1}) like '%' + LOWER(@" + columnName.ToLower() + ") + '%'", TableName, columnName);
            object[] parms = { "@" + columnName.ToLower(), value };
            return DbAdapter1.ReadList(sql, Make, false, parms);
        }

        public virtual IList<T> GetAll()
        {
            string sql = Extensions.OrderBy(string.Format(@"SELECT * FROM {0}", TableName), mEntityIDName + " desc");
            return DbAdapter1.ReadList(sql, Make, false);
        }

        public virtual int Count()
        {
            string sql = string.Format(@"SELECT count({0}) FROM {1}",mEntityIDName, TableName);
            return DbAdapter1.ExcecuteScalar(sql, false,null).AsInt();
        }

       
        protected virtual object[] Take(T businessObject)
        {
            Type type = typeof(T);
            var listProperties = new List<object>();
            foreach (PropertyInfo info in type.GetProperties())
            {
                if (info.PropertyType.FullName != null && (info.MemberType == MemberTypes.Property && info.PropertyType != typeof(IDictionary<string, object>)
                                                           && info.PropertyType.FullName.IndexOf("Objects", StringComparison.Ordinal) == -1 && info.Name != "EcryptedID"))
                {
                    string fieldName = info.Name;
                    var obj = "@" + fieldName;
                    listProperties.Add(obj);
                    object propValue;
                    if (info.PropertyType.BaseType != null && info.PropertyType.BaseType.ToString().IndexOf("Enum", StringComparison.Ordinal) >= 0)
                    {
                        propValue = (short)Enum.Parse(info.GetValue(businessObject, null).GetType(), info.GetValue(businessObject, null).ToString());
                    }
                    else
                    {
                        propValue = info.GetValue(businessObject, null);
                    }
                    listProperties.Add(propValue);
                }
            }
            return listProperties.ToArray();
        }

        protected virtual T Make(IDataReader reader)
        {
            Type type = typeof(T);
            var entity = (T)Activator.CreateInstance(type);
            for (int i = 0; i < reader.FieldCount; i++)
            {
                string column = reader.GetName(i);
                bool check = true;
                if (!string.IsNullOrEmpty(column))
                {
                    foreach (PropertyInfo info in type.GetProperties())
                    {
                        try
                        {
                            string fieldName = info.Name;
                            if (fieldName == "ID")
                                fieldName = mEntityIDName;
                            if (column == fieldName)
                            {
                                info.SetValue(entity, TypeDescriptor.GetConverter(info.PropertyType).ConvertFrom(reader[fieldName].ToString()), null);
                                check = false;
                            }
                        }
                        catch { }
                    }
                    if (check)
                    {
                        entity.ExtentionProperty.Add(column, Extensions.AsString(reader[i]));
                    }
                }
            }
            return entity;
        }
    }
}