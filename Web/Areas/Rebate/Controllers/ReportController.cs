using BussinessModels;
using Helpers.Functions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rebate.Controllers
{
    public class ReportController : RebateController
    {
        // GET: Report
        public ActionResult Index(string key = "", int currentPage = 1)
        {
            int cacheTime = (24 - DateTime.Now.Hour) * 60;

            ReportItemModel model = XmlReader.DeserializeXMLFileToObject<ReportItemModel>(Server.MapPath("/Files/Xml/" + key + ".xml"));
            model.data = CachingHelper.GetObjectFromCache<DataTable>(key.Replace("-", "_") + "_" + SEmployee.EmployeeCode, cacheTime);
            if (model.data == null)
            {
                model.data = CachingHelper.SetObjectFromCache<DataTable>(key.Replace("-", "_") + "_" + SEmployee.EmployeeCode, cacheTime, DataFunction.GetDataReportFromService(ServiceUrl, "", MapFilterParams(model.filter), "RPT1902-00001"));
            }
            return View(model);
        }
    }
}