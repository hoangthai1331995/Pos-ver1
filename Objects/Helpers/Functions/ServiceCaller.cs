using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceObjects;
using BussinessObjects;
using System.Data;
using System.Reflection;
using System.ComponentModel;

namespace ServiceRoute
{
    public class ServiceCallers
    {
        public static List<T> GetList<T>(string serviceUrl, string serviceMethod, string tokenKey, List<string> parms, Method method = Method.POST) where T : BusinessObject // Return List Object
        {
            try
            {
                var _client = new RestClient(serviceUrl);
                var request = new RestRequest(serviceMethod, method) { RequestFormat = DataFormat.Json };
                GetDataRequest dataRequest = new GetDataRequest();
                dataRequest.Params = parms;
                if(!String.IsNullOrEmpty(tokenKey))
                {
                    request.AddHeader("tokenKey", tokenKey);
                }
                request.AddBody(dataRequest);
                var response = _client.Execute(request);
                GetDataResponse responseData = JsonConvert.DeserializeObject<GetDataResponse>(response.Content);
                List<T> dataObject = new List<T>();
                DataTable dataTable = new DataTable();
                if (responseData != null)
                {
                    dataTable = JsonConvert.DeserializeObject<DataTable>(responseData.Content.ToString());
                    return ConvertDataTableToListObject<T>(dataTable);
                }
            }
            catch(Exception ex)
            {}
            return new List<T>();
        }
        public static List<T> GetFilterWithPaging<T>(string serviceUrl, string serviceMethod, string tokenKey, List<string> filterParam, Method method = Method.POST) where T : BusinessObject // Return List Object
        {
            var content = "";
            try
            {
                var _client = new RestClient(serviceUrl);
                var request = new RestRequest(serviceMethod, method) { RequestFormat = DataFormat.Json };
                GetDataRequest dataRequest = new GetDataRequest();
                
                if (!String.IsNullOrEmpty(tokenKey))
                {
                    request.AddHeader("tokenKey", tokenKey);
                }
                request.AddBody(dataRequest);
                var response = _client.Execute(request);
                GetDataResponse responseData = JsonConvert.DeserializeObject<GetDataResponse>(response.Content);
                content = responseData.Content.ToString();
                List<T> dataObject = new List<T>();
                if (responseData != null)
                {
                    DataTable dataTable = new DataTable();
                    if (responseData != null)
                    {
                        dataTable = JsonConvert.DeserializeObject<DataTable>(responseData.Content.ToString());
                        return ConvertDataTableToListObject<T>(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {}
            return new List<T>();
        }
        public static T GetSingle<T>(string serviceUrl, string serviceMethod, string tokenKey, List<string> filterParam, Method method,string objectId="") where T : BusinessObject // Return Single object
        {
            try
            {
                var _client = new RestClient(serviceUrl);
                var request = new RestRequest(serviceMethod, method) { RequestFormat = DataFormat.Json };
                GetDataRequest dataRequest = new GetDataRequest();
                dataRequest.Params = filterParam;
                //dataRequest.Params.Add(new ServiceParam(filterParam.name, filterParam.value));
                if(!String.IsNullOrEmpty(tokenKey))
                {
                    request.AddHeader("tokenKey", tokenKey);
                }
                if (!String.IsNullOrEmpty(objectId))
                {
                    request.AddHeader("objectId", objectId);
                }
                request.AddBody(dataRequest);
                var response = _client.Execute(request);
                GetDataResponse responseData = JsonConvert.DeserializeObject<GetDataResponse>(response.Content);
                T dataObject = default(T);
                if (responseData != null)
                {
                    DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(responseData.Content.ToString());
                    if(dataTable != null)
                    {
                        List<T> dataObjects = ConvertDataTableToListObject<T>(dataTable);
                        if(dataObjects != null && dataObjects.Count() > 0)
                        {
                            return dataObjects.FirstOrDefault();
                        }
                    }
                }
            }
            catch(Exception ex)
            {}
            return default(T);
        }
        public static NoneQueryResponse AddUpdate<T>(string serviceUrl, string serviceMethod, string tokenKey, T dataPost, string denyFields = "", string username = "", string permissionCode = "") // Return Single Object
        {
            try
            {
                var _client = new RestClient(serviceUrl);
                var request = new RestRequest(serviceMethod, Method.POST) { RequestFormat = DataFormat.Json };
                PutDataRequest dataRequest = new PutDataRequest();
                dataRequest.RequestObject = dataPost;
                dataRequest.DenyFields = denyFields;
                /*
                    For case add more param over object
                */
                request.AddBody(dataRequest);
                if(!String.IsNullOrEmpty(tokenKey))
                {
                    request.AddHeader("tokenKey", tokenKey);
                }
                var response = _client.Execute(request);
                GetDataResponse responseData = JsonConvert.DeserializeObject<GetDataResponse>(response.Content);
                NoneQueryResponse dataObject = new NoneQueryResponse();
                if (responseData != null)
                {
                    dataObject = JsonConvert.DeserializeObject<NoneQueryResponse>(responseData.Content.ToString());
                    return dataObject;
                }
                return dataObject;
            }
            catch(Exception ex)
            {
                return new NoneQueryResponse();
            }
        }
        public static Dictionary<string, object> SelectCustomData(string serviceUrl, string serviceMethod, List<string> filterParam) // Return Rowcount
        {
            try
            {
                var _client = new RestClient(serviceUrl);
                var request = new RestRequest(serviceMethod, Method.PATCH) { RequestFormat = DataFormat.Json };

                GetDataRequest dataRequest = new GetDataRequest();
                request.AddBody(dataRequest);
                var response = _client.Execute(request);
                GetDataResponse responseData = JsonConvert.DeserializeObject<GetDataResponse>(response.Content);
                Dictionary<string, object> dataObject = new Dictionary<string, object>();
                if (responseData != null)
                {
                    dataObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseData.Content.ToString());
                }
                return dataObject;
            }
            catch(Exception ex)
            {
                return new Dictionary<string, object>();
            }
        }
        public static bool UpdateCustomData(string serviceUrl, string serviceMethod, List<string> filterParam) // Return Rowcount
        {
            try
            {
                var _client = new RestClient(serviceUrl);
                var request = new RestRequest(serviceMethod, Method.PATCH) { RequestFormat = DataFormat.Json };
                GetDataRequest dataRequest = new GetDataRequest();
                request.AddBody(dataRequest);
                var response = _client.Execute(request);
                GetDataResponse responseData = JsonConvert.DeserializeObject<GetDataResponse>(response.Content);
                NoneQueryResponse dataObject = new NoneQueryResponse();
                if (responseData != null)
                {
                    dataObject = JsonConvert.DeserializeObject<NoneQueryResponse>(responseData.Content.ToString());
                    if (dataObject.StatusCode == "Done")
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static List<SelectList> GetSelectList(string serviceUrl, string tokenKey, string  tableName, string fieldDisplay, string fieldValue) // Return List Object
        {
            try
            {
                var _client = new RestClient(serviceUrl);
                var request = new RestRequest("Common/GetSelectList", Method.POST) { RequestFormat = DataFormat.Json };
                GetSelectListRequest dataRequest = new GetSelectListRequest(tableName, fieldDisplay, fieldValue);
                if (!String.IsNullOrEmpty(tokenKey))
                {
                    request.AddHeader("tokenKey", tokenKey);
                }
                request.AddBody(dataRequest);
                var response = _client.Execute(request);
                GetDataResponse responseData = JsonConvert.DeserializeObject<GetDataResponse>(response.Content);
                List<SelectList> dataObject = new List<SelectList>();
                if (responseData != null)
                {
                    dataObject = JsonConvert.DeserializeObject<List<SelectList>>(responseData.Content.ToString());
                    SelectList defaultSelected = new SelectList();
                    defaultSelected.Name = " - Chọn - ";
                    defaultSelected.Code = "";
                    dataObject.Insert(0, defaultSelected);
                }
                return dataObject;
            }
            catch (Exception ex)
            {
                return new List<SelectList>();
            }
        }
        public static List<SelectList> GetSelectListWithSchema(string serviceUrl, string tokenKey, string tableName, string fieldDisplay, string fieldValue) // Return List Object
        {
            try
            {
                var _client = new RestClient(serviceUrl);
                var request = new RestRequest("Common/GetSelectListWithSchema", Method.POST) { RequestFormat = DataFormat.Json };
                GetSelectListRequest dataRequest = new GetSelectListRequest(tableName, fieldDisplay, fieldValue);
                if (!String.IsNullOrEmpty(tokenKey))
                {
                    request.AddHeader("tokenKey", tokenKey);
                }
                request.AddBody(dataRequest);
                var response = _client.Execute(request);
                GetDataResponse responseData = JsonConvert.DeserializeObject<GetDataResponse>(response.Content);
                List<SelectList> dataObject = new List<SelectList>();
                if (responseData != null)
                {
                    dataObject = JsonConvert.DeserializeObject<List<SelectList>>(responseData.Content.ToString());
                    SelectList defaultSelected = new SelectList();
                    defaultSelected.Name = " - Chọn - ";
                    defaultSelected.Code = "";
                    dataObject.Insert(0, defaultSelected);
                }
                return dataObject;
            }
            catch (Exception ex)
            {
                return new List<SelectList>();
            }
        }
        public static List<SelectList> GetSelectListNoneSelect(string serviceUrl, string tokenKey, string tableName, string fieldDisplay, string fieldValue) // Return List Object
        {
            try
            {
                var _client = new RestClient(serviceUrl);
                var request = new RestRequest("Common/GetSelectList", Method.POST) { RequestFormat = DataFormat.Json };
                GetSelectListRequest dataRequest = new GetSelectListRequest(tableName, fieldDisplay, fieldValue);
                if (!String.IsNullOrEmpty(tokenKey))
                {
                    request.AddHeader("tokenKey", tokenKey);
                }
                request.AddBody(dataRequest);
                var response = _client.Execute(request);
                GetDataResponse responseData = JsonConvert.DeserializeObject<GetDataResponse>(response.Content);
                List<SelectList> dataObject = new List<SelectList>();
                if (responseData != null)
                {
                    dataObject = JsonConvert.DeserializeObject<List<SelectList>>(responseData.Content.ToString());
                }
                return dataObject;
            }
            catch (Exception ex)
            {
                return new List<SelectList>();
            }
        }

        public static List<T> ConvertDataTableToListObject<T>(DataTable dataTable) where T : BusinessObject
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
                            bool check = true;

                            foreach (PropertyInfo info in type.GetProperties())
                            {
                                try
                                {
                                    string fieldName = info.Name;
                                    if (dataTable.Columns[c].ColumnName == fieldName)
                                    {
                                        info.SetValue(entity, TypeDescriptor.GetConverter(info.PropertyType).ConvertFrom(dataTable.Rows[r][c].ToString()), null);
                                        check = false;
                                    }
                                }
                                catch { }
                            }
                            if (check)
                            {
                                entity.ExtentionProperty.Add(dataTable.Columns[c].ColumnName, dataTable.Rows[r][c].ToString());
                            }
                        }
                        responseObj.Add(entity);
                    }
                    return responseObj;
                }
                catch (Exception ex)
                {}
            }
            return new List<T>();
        }
    }
}
