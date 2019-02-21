using BussinessObjects;
using BussinessObjects.Xml;
using DataObjects;
using Extension;
using Helpers.Functions;
using ServiceObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PrivateService.Controllers
{
    public class RequestController : BaseApiController
    {
        // GET: Request
        [HttpPost]
        [Route("Request/GetData")]
        public IHttpActionResult GetData(GetDataRequest request)
        {
            GetDataResponse response = new GetDataResponse();
            try
            {
                response.Content = new SqlDynamicDao<Dynamic>().GetDataTable(request.Params.ToArray(), request.DataSource);
                response.Message = Extension.ResponseMessage.Success;
                response.Status = (int)ResponseStatus.Success;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = Mapper.SetResponseResult((int)ResponseStatus.UnknowError, Extension.ResponseMessage.UnknowError, ex.Message);
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("Request/GetReportData")]
        public IHttpActionResult GetReportData(ReportRequest request)
        {
            GetDataResponse response = new GetDataResponse();
            try
            {
                ServiceMethods sMethods = XmlReader.DeserializeXMLFileToObject<ServiceMethods>(System.Web.Hosting.HostingEnvironment.MapPath("/Files/Xml/RPT.xml"));
                ServiceMethod method = sMethods.Methods.Where(m => m.key == request.Key).FirstOrDefault();
                if (method != null)
                {
                    response.Content = new SqlDynamicDao<Dynamic>().GetDataTable(request.Params.ToArray(), method.datasource);
                    response.Message = Extension.ResponseMessage.Success;
                    response.Status = (int)ResponseStatus.Success;
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = Mapper.SetResponseResult((int)ResponseStatus.UnknowError, Extension.ResponseMessage.UnknowError, ex.Message);
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("Request/NoneQuery")]
        public IHttpActionResult NoneQuery(NoneQueryRequest request)
        {
            GetDataResponse response = new GetDataResponse();
            try
            {
                response.Content = new SqlDynamicDao<Dynamic>().GetSingleData(request.Params, request.DataSource);
                response.Message = Extension.ResponseMessage.Success;
                response.Status = (int)ResponseStatus.Success;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = Mapper.SetResponseResult((int)ResponseStatus.UnknowError, Extension.ResponseMessage.UnknowError, ex.Message);
            }
            return Ok(response);
        }
    }
}