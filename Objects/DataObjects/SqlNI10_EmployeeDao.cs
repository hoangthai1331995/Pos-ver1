using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class SqlNI10_EmployeeDao : SqlDaoBase<NI10_Employee>
    {
        public SqlNI10_EmployeeDao()
        {
            TableName = "NI10_Employee";
            EntityIDName = "EmployeeId";
            StoreProcedurePrefix = "spNI10_Employee_";
        }
        public SqlNI10_EmployeeDao(string tableName, string entityIDName, string storeProcedurePrefix) : base(tableName, entityIDName, storeProcedurePrefix) { }
    }
}
