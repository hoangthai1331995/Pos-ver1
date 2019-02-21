using BussinessModels;
using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rebate.Controllers
{
    public class RebateController : Controller
    {
        public string ServiceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
        protected List<string> NoAuthentication = new List<string>();
        protected List<string> LogAction = new List<string>();
        protected List<string> NoLogAction = new List<string>();

        public List<string> MapFilterParams(List<fitem> fitems)
        {
            try
            {
                List<string> parms = new List<string>();
                if (fitems != null && fitems.Count() > 0)
                {
                    foreach (fitem item in fitems)
                    {
                        parms.Add(item.key);
                        parms.Add(Request[item.key]);
                    }
                }
                return parms;
            }
            catch(Exception ex)
            {
                return new List<string>();
            }
        }

        public NI10_Employee SEmployee
        {
            get
            {
                if (Session != null)
                {
                    return (NI10_Employee)Session["EmployeeLogin"];
                }
                return null;
            }
            set
            {
                if (value == null)
                    Session.Remove("EmployeeLogin");
                else
                    Session["EmployeeLogin"] = value;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string currentUrl = Request.Url.PathAndQuery;
            string actionName = filterContext.ActionDescriptor.ActionName;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            if (!filterContext.IsChildAction)
            {
                if (Session["EmployeeLogin"] == null && !NoAuthentication.Contains(actionName))
                {
                    filterContext.Result = new RedirectResult("/User/Login?returnUrl=/");
                }
            }
        }
    }
}