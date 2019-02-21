using BussinessModels;
using BussinessObjects;
using DataObjects;
using Newtonsoft.Json;
using RestSharp;
using ServiceObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Functions
{
    public static class DataFunction
    {
        #region Call Direct

        public static List<T> GetCustomObjectList<T>(List<string> parms, string dataSource)
        {
            DataTable dataTable = new SqlDynamicDao<Dynamic>().GetDataTable(parms.ToArray(), dataSource);
            if(dataTable != null)
            {
                return ConvertDataTableToListCustomModel<T>(dataTable);
            }
            return new List<T>();
        }

        public static T GetCustomObjectSingle<T>(List<string> parms, string dataSource)
        {
            DataTable dataTable = new SqlDynamicDao<Dynamic>().GetDataTable(parms.ToArray(), dataSource);
            if (dataTable != null)
            {
                List<T> datas = ConvertDataTableToListCustomModel<T>(dataTable);
                if(datas != null && datas.Count() > 0)
                {
                    return datas.FirstOrDefault();
                }
            }
            return default(T);
        }

        public static int UpdateCustomField(List<string> parms, string dataSource)
        {
            return new SqlDynamicDao<Dynamic>().UpdateCustomField(parms.ToArray(), dataSource);
        }
        #endregion

        #region Call Service
        public static List<T> GetCustomObjectListFromService<T>(string serviceUrl, string requestMethod, string tokenKey, List<string> parms, string dataSource)
        {
            try
            {
                var _client = new RestClient(serviceUrl);
                var request = new RestRequest(requestMethod, Method.POST) { RequestFormat = DataFormat.Json };
                GetDataRequest dataRequest = new GetDataRequest();
                dataRequest.Params = parms;
                if (!String.IsNullOrEmpty(tokenKey))
                {
                    request.AddHeader("tokenKey", tokenKey);
                }
                dataRequest.DataSource = dataSource;
                request.AddJsonBody(dataRequest);
                var response = _client.Execute(request);
                GetDataResponse responseData = JsonConvert.DeserializeObject<GetDataResponse>(response.Content);
                List<T> dataObject = new List<T>();
                DataTable dataTable = new DataTable();
                if (responseData != null)
                {
                    dataTable = JsonConvert.DeserializeObject<DataTable>(responseData.Content.ToString());
                    return ConvertDataTableToListCustomModel<T>(dataTable);
                }
            }
            catch (Exception ex)
            { }
            return new List<T>();
        }

        public static T GetCustomObjectSingleFromService<T>(string serviceUrl, string requestMethod, string tokenKey, List<string> parms, string dataSource)
        {
            try
            {
                var _client = new RestClient(serviceUrl);
                var request = new RestRequest(requestMethod, Method.POST) { RequestFormat = DataFormat.Json };
                GetDataRequest dataRequest = new GetDataRequest();
                dataRequest.Params = parms;
                if (!String.IsNullOrEmpty(tokenKey))
                {
                    request.AddHeader("tokenKey", tokenKey);
                }
                dataRequest.DataSource = dataSource;
                request.AddJsonBody(dataRequest);
                var response = _client.Execute(request);
                GetDataResponse responseData = JsonConvert.DeserializeObject<GetDataResponse>(response.Content);
                List<T> dataObject = new List<T>();
                DataTable dataTable = new DataTable();
                if (responseData != null)
                {
                    dataTable = JsonConvert.DeserializeObject<DataTable>(responseData.Content.ToString());
                    return ConvertDataTableToListCustomModel<T>(dataTable).FirstOrDefault();
                }
            }
            catch (Exception ex)
            { }
            return default(T);
        }

        public static PutDataResponse PutDataToService(string serviceUrl, string requestMethod, string tokenKey, List<string> parms, string dataSource)
        {
            try
            {
                var _client = new RestClient(serviceUrl);
                var request = new RestRequest(requestMethod, Method.POST) { RequestFormat = DataFormat.Json };
                GetDataRequest dataRequest = new GetDataRequest();
                dataRequest.Params = parms;
                if (!String.IsNullOrEmpty(tokenKey))
                {
                    request.AddHeader("tokenKey", tokenKey);
                }
                dataRequest.DataSource = dataSource;
                request.AddJsonBody(dataRequest);
                var response = _client.Execute(request);
                GetDataResponse responseData = JsonConvert.DeserializeObject<GetDataResponse>(response.Content);
                
                DataTable dataTable = new DataTable();
                if (responseData != null)
                {
                    dataTable = JsonConvert.DeserializeObject<DataTable>(responseData.Content.ToString());
                    return ConvertDataTableToListCustomModel<PutDataResponse>(dataTable).FirstOrDefault();
                }
            }
            catch (Exception ex)
            { }
            return new PutDataResponse();
        }

        public static object ExecuteNoneQuery(string serviceUrl, string requestMethod, string tokenKey, List<string> parms, string dataSource)
        {
            try
            {
                var _client = new RestClient(serviceUrl);
                var request = new RestRequest(requestMethod, Method.POST) { RequestFormat = DataFormat.Json };
                NoneQueryRequest dataRequest = new NoneQueryRequest();
                dataRequest.Params = parms;
                if (!String.IsNullOrEmpty(tokenKey))
                {
                    request.AddHeader("tokenKey", tokenKey);
                }
                dataRequest.DataSource = dataSource;
                request.AddJsonBody(dataRequest);
                var response = _client.Execute(request);
                GetDataResponse responseData = JsonConvert.DeserializeObject<GetDataResponse>(response.Content);
                if(responseData.Status == 1)
                {
                    return responseData.Content;
                }
            }
            catch (Exception ex)
            { }
            return null;
        }

        #region Report
        public static DataTable GetDataReportFromService(string serviceUrl,string tokenKey, List<string> parms, string reportKey)
        {
            try
            {
                var _client = new RestClient(serviceUrl);
                var request = new RestRequest("Request/GetReportData", Method.POST) { RequestFormat = DataFormat.Json };
                ReportRequest dataRequest = new ReportRequest();
                dataRequest.Params = parms;
                if (!String.IsNullOrEmpty(tokenKey))
                {
                    request.AddHeader("tokenKey", tokenKey);
                }
                dataRequest.Key = reportKey;
                request.AddJsonBody(dataRequest);
                var response = _client.Execute(request);
                GetDataResponse responseData = JsonConvert.DeserializeObject<GetDataResponse>(response.Content);
                DataTable dataTable = new DataTable();
                if (responseData != null)
                    return JsonConvert.DeserializeObject<DataTable>(responseData.Content.ToString());
            }
            catch (Exception ex)
            { }
            return null;
        }
        #endregion

        #endregion
        public static List<T> ConvertDataTableToListCustomModel<T>(DataTable dataTable)
        {
            List<T> responseObj = new List<T>();
            if (dataTable != null)
            {
                try
                {
                    for (int r = 0; r < dataTable.Rows.Count; r++) // each row
                    {
                        Type type = typeof(T);
                        var entity = (T)Activator.CreateInstance(type);
                        for (int c = 0; c < dataTable.Columns.Count; c++) // each row
                        {
                            foreach (PropertyInfo info in type.GetProperties())
                            {
                                try
                                {
                                    string fieldName = info.Name;
                                    if (dataTable.Columns[c].ColumnName == fieldName)
                                    {
                                        info.SetValue(entity, TypeDescriptor.GetConverter(info.PropertyType).ConvertFrom(dataTable.Rows[r][c].ToString()), null);
                                    }
                                }
                                catch { }
                            }
                        }
                        responseObj.Add(entity);
                    }
                    return responseObj;
                }
                catch (Exception ex)
                { }
            }
            return new List<T>();
        }
    }
}
