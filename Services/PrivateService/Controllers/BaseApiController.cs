using Helpers.Functions;
using Extension;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Web.Http.Results;
using static Extension.ServiceHelper;
using ServiceObjects;
using DataObjects;
using BussinessObjects;

namespace PrivateService.Controllers
{

    public class BaseApiController : ApiController
    {
        // GET: BaseApi
        protected StatusCodeResult NoContent()
        {
            return StatusCode(HttpStatusCode.NoContent);
        }

        protected IHttpActionResult BadRequest()
        {
            var x = new System.Web.Http.Results.ResponseMessageResult(
                Request.CreateErrorResponse(
                    (HttpStatusCode)400,
                    new HttpError(GlobalStatus.InvalidData.ToString())
                )
            );

            x.Response.Headers.Add(EwHeaders.X_Status, GlobalStatus.InvalidData.ToString());
            return x;
        }

        protected IHttpActionResult Pagination<T>(T data, int totalItems = 0, int limit = 20, int page = 1)
        {
            var x = new System.Web.Http.Results.ResponseMessageResult(
                Request.CreateResponse(HttpStatusCode.OK, data)
            );

            int totalPage = totalItems / limit;
            totalPage = totalItems % limit == 0 ? totalPage : (totalPage + 1);

            x.Response.Headers.Add(EwHeaders.X_Paging_Total_Count, totalItems.ToString());
            x.Response.Headers.Add(EwHeaders.X_Paging_Limit, limit.ToString());
            x.Response.Headers.Add(EwHeaders.X_Paging_Total_Pages, totalPage.ToString());
            x.Response.Headers.Add(EwHeaders.X_Paging_Current_Page, page.ToString());
            x.Response.Headers.Add(EwHeaders.X_Status, HttpStatusCode.OK.ToString());
            return x;
        }

        protected IHttpActionResult NoOK()
        {
            return NoOK("Something goes wrong");
        }

        protected IHttpActionResult NoOK(GlobalStatus statusCode)
        {
            return NoOK(statusCode.ToString()) as ResponseMessageResult;
        }

        protected IHttpActionResult NoOK(string statusCode)
        {
            var x = new System.Web.Http.Results.ResponseMessageResult(
                Request.CreateErrorResponse(
                    (HttpStatusCode)422,
                    new HttpError(statusCode)
                )

            );

            x.Response.Headers.Add(EwHeaders.X_Status, statusCode);
            return x;
        } 
    }
}