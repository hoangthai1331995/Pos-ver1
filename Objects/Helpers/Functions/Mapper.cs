using BussinessModels;
using BussinessObjects;
using Newtonsoft.Json;
using ServiceObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Functions
{
    public static class Mapper
    {
        public static List<string> SplitString(string s = "", char character = ',')
        {
            try
            {
                if (!String.IsNullOrEmpty(s))
                {
                    string[] arrStr = s.Split(character);
                    return arrStr.ToList();
                }
            }
            catch (Exception ex)
            { }
            return new List<string>();
        }

        public static IEnumerable<Dictionary<string, object>> Serialize(SqlDataReader reader)
        {
            var results = new List<Dictionary<string, object>>();
            var cols = new List<string>();
            for (var i = 0; i < reader.FieldCount; i++)
                cols.Add(reader.GetName(i));

            while (reader.Read())
                results.Add(SerializeRow(cols, reader));

            return results;
        }
        private static Dictionary<string, object> SerializeRow(IEnumerable<string> cols,
                                                        SqlDataReader reader)
        {
            var result = new Dictionary<string, object>();
            foreach (var col in cols)
                result.Add(col, reader[col]);
            return result;
        }

        public static GetDataResponse SetResponseResult(int _status, string _message, object _content)
        {
            GetDataResponse response = new GetDataResponse();
            response.Status = _status;
            response.Message = _message;
            response.Content = _content;
            return response;
        }

        public static List<EditTableField> MapObjectToEditModel<T>(T data, List<FieldAddUpdateAuto> controlLists)
        {
            List<EditTableField> editTableFields = new List<EditTableField>();
            Type type = data.GetType();
            foreach (FieldAddUpdateAuto control in controlLists) // each row
            {
                foreach (PropertyInfo info in type.GetProperties())
                {
                    try
                    {
                        if (info.Name == control.FieldName)
                        {
                            EditTableField editTableField = new EditTableField()
                            {
                                FieldName = control.FieldName,
                                FieldValue = info.GetValue(data).ToString(),
                                FieldType = Convert.ToInt32(control.FieldType)
                            };
                            editTableFields.Add(editTableField);
                        }
                    }
                    catch { }
                }
            }
            return editTableFields;
        }

        public static List<T> MapJsonStringToListObject<T>(string jsonString = "")
        {
            try
            {
                if(!String.IsNullOrEmpty(jsonString))
                {
                    return JsonConvert.DeserializeObject<List<T>>(jsonString);
                }
            }
            catch(Exception ex)
            {
            }
            return new List<T>();
        }
    }
}
