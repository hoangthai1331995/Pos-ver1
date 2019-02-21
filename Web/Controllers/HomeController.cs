using BussinessModels;
using Helpers.Functions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models.Attributes;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
      
        public ActionResult Index()
        {
            TempData["Permission"] = "aaaaaa";
            string dadsfds = "12344";
            return View();
            // [dbo].[NI09_TaskList_TestConnect] 'PUT'
            //string serviceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
            //PutDataResponse dataResponse = DataFunction.PutDataToService(serviceUrl, "Request/GetData", "", new List<string>() { "@typeConnect", "PUT" }, "[dbo].[NI09_TaskList_TestConnect]");
            //return Content(JsonConvert.SerializeObject(dataResponse));
        }
    }
}