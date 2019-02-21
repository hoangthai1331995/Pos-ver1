using BussinessObjects;
using Helpers.Functions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
        private string serviceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
        protected List<string> NoAuthentication = new List<string>();
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

        //protected override void OnResultExecuting(ResultExecutingContext filterContext)
        //{
        //    string permissionKey = filterContext.Controller.TempData.ContainsKey("Permission") ? filterContext.Controller.TempData["Permission"].ToString() : "";
        //    if (permissionKey != "")
        //    {
        //        object dataResponse = DataFunction.ExecuteNoneQuery(serviceUrl, "Request/NoneQuery", "",
        //                                                               new List<string>() {
        //                                                                                            "@employeeCode", SEmployee.EmployeeCode,
        //                                                                                            "@functionCode", permissionKey
        //                                                                                  },
        //                                                               "[dbo].[NI10_Function_CheckPermission]");
        //        if (dataResponse != null)
        //        {
        //            if (Convert.ToInt32(dataResponse) == 0)
        //            {
        //                filterContext.Result = new RedirectResult("/User/RequiredPermission?k=" + StaticFunc.Base64Encode(permissionKey));
        //            }
        //        }
        //        else
        //        {
        //            filterContext.Result = new RedirectResult("/User/RequiredPermission?k=" + StaticFunc.Base64Encode(permissionKey));
        //        }
        //    }
        //}

        #region 'WriteLog'
        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            //Log the error!!
            WriteLogError((SEmployee != null) ? "_" + filterContext.Exception.ToString() : filterContext.Exception.ToString());
            //NotificationMessage = new LayoutNotificationModel("error", "Có lỗi trong quá trình xử lý dữ liệu", "Thông báo!");
            //Redirect or return a view, but not both.
            filterContext.Result = Content(filterContext.Exception.ToString());
        }
        public void WriteLogError(string message)
        {
            bool exists = false;
            bool exists1 = false;
            string aaa = AppDomain.CurrentDomain.BaseDirectory;
            //string actionName = Request.Url.AbsolutePath.Replace("/", "_");
            string controllerName = Request.RequestContext.RouteData.Values["controller"].ToString();
            string actionName = Request.RequestContext.RouteData.Values["action"].ToString();
            StreamWriter sw = null;
            string subPath = DateTime.Now.ToString("yyyyMMdd");
            exists = System.IO.Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\" + subPath);
            if (!exists)
                System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\" + subPath);
            try
            {
                exists1 = System.IO.Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\" + subPath + "\\" + controllerName);
                if (!exists1)
                    System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\" + subPath + "\\" + controllerName);

                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\" + subPath + "\\" + controllerName + "\\" + actionName + ".txt", true);
                sw.WriteLine(DateTime.Now.ToString("g") + ": " + message);
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString("g") + ": " + ex.ToString());
                sw.Flush();
                sw.Close();
            }
        }

        #endregion
    }
}