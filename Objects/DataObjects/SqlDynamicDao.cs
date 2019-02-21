using BussinessObjects;
using ServiceObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class SqlDynamicDao<T> where T : BusinessObject
    {

        private string mTableName;
        private string mEntityIDName;
        private string mStoreProcedurePrefix;
        private string mConnectionName;
        //private string validationCode = ConfigurationManager.AppSettings.Get("ClientValidationCode");

        public string TableName
        {
            get { return mTableName; }
            set { mTableName = value; }
        }

        public string EntityIDName
        {
            get { return mEntityIDName; }
            set { mEntityIDName = value; }
        }

        public string StoreProcedurePrefix
        {
            get { return mStoreProcedurePrefix; }
            set { mStoreProcedurePrefix = value; }
        }

        public string ConnectionName
        {
            get { return (String.IsNullOrEmpty(mConnectionName) ? "Default" : mConnectionName); }
            set { mConnectionName = value; }
        }

        public SqlDynamicDao() { }

        public SqlDynamicDao(string tableName, string entityIDName, string storeProcedurePrefix)
        {
            mTableName = tableName;
            mEntityIDName = entityIDName;
            mStoreProcedurePrefix = storeProcedurePrefix;
        }

        public SqlDynamicDao(string connectionName)
        {
            mConnectionName = connectionName;
        }

        public virtual IList<T> GetList(object[] parms)
        {
            string sql = StoreProcedurePrefix + "GetList";
            switch(ConnectionName)
            {
                case "Default":
                    return DbAdapter1.ReadList<T>(sql, Make, true, parms);
                case "Inside":
                    return DbAdapter2.ReadList<T>(sql, Make, true, parms);
                default:
                    return DbAdapter1.ReadList<T>(sql, Make, true, parms);
            }
        }

        public virtual IList<T> GetFilter(List<string> parms, string storeName = "")
        {
            switch (ConnectionName)
            {
                case "Default":
                    return DbAdapter1.ReadList(storeName, Make, true, parms.ToArray());
                case "Inside":
                    return DbAdapter2.ReadList(storeName, Make, true, parms.ToArray());
                default:
                    return DbAdapter1.ReadList(storeName, Make, true, parms.ToArray());
            }
            
        }
      
        public virtual T GetSingle(long entityID)
        {
            string sql = string.Format(@"SELECT * FROM {0} WHERE {1} = @EntityID", mTableName, mEntityIDName);
            object[] parms = { "@EntityID", entityID };
            switch (ConnectionName)
            {
                case "Default":
                    return DbAdapter1.Read(sql, Make, false, parms);
                case "Inside":
                    return DbAdapter2.Read(sql, Make, false, parms);
                default:
                    return DbAdapter1.Read(sql, Make, false, parms);
            }
        }

        public virtual object GetSingleData(List<string> parms, string storeProcedure)
        {
            switch (ConnectionName)
            {
                case "Default":
                    return DbAdapter1.ExcecuteScalar(storeProcedure, true, parms.ToArray());
                case "Inside":
                    return DbAdapter2.ExcecuteScalar(storeProcedure, true, parms.ToArray());
                default:
                    return DbAdapter1.ExcecuteScalar(storeProcedure, true, parms.ToArray());
            }
        }

        public virtual T GetSingleObject(List<string> parms, string storeProcedure)
        {
            
            switch (ConnectionName)
            {
                case "Default":
                    return DbAdapter1.Read(storeProcedure, Make, true, parms.ToArray());
                case "Inside":
                    return DbAdapter2.Read(storeProcedure, Make, true, parms.ToArray());
                default:
                    return DbAdapter1.Read(storeProcedure, Make, true, parms.ToArray());
            }
        }

        public virtual DataTable GetDataTable(object[] parms, string sql)
        {
            switch (ConnectionName)
            {
                case "Default":
                    return DbAdapter1.ReadDataTable(sql, true, parms);
                case "Inside":
                    return DbAdapter2.ReadDataTable(sql, true, parms);
                default:
                    return DbAdapter1.ReadDataTable(sql, true, parms);
            }
        }

        public virtual int UpdateCustomField(object[] parms, string sql)
        {
            switch (ConnectionName)
            {
                case "Default":
                    return Convert.ToInt32(DbAdapter1.ExcecuteScalar(sql, false, parms));
                case "Inside":
                    return Convert.ToInt32(DbAdapter2.ExcecuteScalar(sql, false, parms));
                default:
                    return Convert.ToInt32(DbAdapter1.ExcecuteScalar(sql, false, parms));
            }
            
        }

        public virtual object[] Take(T businessObject, string denyFields = "")
        {
            //if(Validate.CheckValidate(validationCode))
            //{
            Type type = typeof(T);
            var listProperties = new List<object>();
            foreach (PropertyInfo info in type.GetProperties())
            {
                if (!String.IsNullOrEmpty(denyFields))
                {
                    List<string> denyFieldArr = denyFields.Split(',').ToList();
                    if (!denyFieldArr.Exists(m => m == info.PropertyType.FullName) && info.PropertyType.FullName != null && (info.MemberType == MemberTypes.Property && info.PropertyType != typeof(IDictionary<string, object>)
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
                else
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
            }
            return listProperties.ToArray();
            //}
            //return new List<object>().ToArray();
        }

        public virtual T Make(IDataReader reader)
        {
            //if (Validate.CheckValidate(validationCode))
            //{
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
            //}
            //return null;
        }

        public static NoneQueryResponse MakeNoneQuery(IDataReader reader)
        {
            Type type = typeof(NoneQueryResponse);
            NoneQueryResponse entity = (NoneQueryResponse)Activator.CreateInstance(type);
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
