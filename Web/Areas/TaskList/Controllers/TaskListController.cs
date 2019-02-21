using BussinessObjects;
using Helpers.Functions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskList.Controllers
{
    public class TaskListController : Controller
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

        protected bool CheckPermission(string functionCode = "")
        {
            object dataResponse = DataFunction.ExecuteNoneQuery(serviceUrl, "Request/NoneQuery", "",
                                                                        new List<string>() {
                                                                                                    "@employeeCode", SEmployee.EmployeeCode,
                                                                                                    "@functionCode", functionCode
                                                                                           },
                                                                        "[dbo].[NI10_Function_CheckPermission]");
            if (dataResponse != null)
            {
                if (Convert.ToInt32(dataResponse) > 0)
                    return true;
            }
            return false;
        }

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

        #region Upload File
        protected string UploadFile(HttpPostedFileBase file, string uploadPath = null, string[] allowExtention = null, long maxSize = 200000000)
        {
            string strUploads = (uploadPath == null || uploadPath == "") ? "~/Userfile" : "";
            string strName = "";
            if (file != null)
            {
                if (allowExtention == null)
                {
                    allowExtention = new[] { "xls", "xlsx", "pdf", "docx", "jpg", "png", "jepg", "zip", "rar" };
                }
                allowExtention = allowExtention.Where(item => item != null).Select(item => item.ToLower()).ToArray();
                var fileExt = System.IO.Path.GetExtension(file.FileName.ToLower()).Substring(1);
                if (!allowExtention.Contains(fileExt))
                {
                    return "";
                }
                if (file.ContentLength > maxSize)
                {
                    return "";
                }
                if (uploadPath == null)
                {
                    if (allowExtention.Contains(fileExt))
                    {
                        strUploads += "/Image";
                    }
                    else
                    {
                        strUploads += "/File";
                    }
                }
                else
                {
                    strUploads += "/" + uploadPath.Trim().Trim('/');
                }

                if (file.ContentLength >= 0)
                {
                    string[] fileName = Path.GetFileName(file.FileName).ToString().Split('.').ToArray();
                    string name = "";
                    for (int i = 0; i < fileName.Count() - 1; i++)
                    {
                        name += fileName[i] + ".";
                    }
                    name = name.Trim('.');
                    int k = 0;
                    string newName = DateTime.Now.ToString("yyyyMMdd") + "-" + name;
                    var checkFIleExist = Path.Combine(Server.MapPath(strUploads), newName + "." + fileExt.ToLower());
                    do
                    {
                        if (System.IO.File.Exists(checkFIleExist))
                        {
                            k++;
                            newName = DateTime.Now.ToString("yyyyMMdd") + "_" + name + "(" + k + ")";
                            checkFIleExist = Path.Combine(Server.MapPath(strUploads), newName + "." + fileExt.ToLower());
                        }
                    } while (System.IO.File.Exists(checkFIleExist));
                    var path = Path.Combine(Server.MapPath(strUploads), newName + "." + fileExt.ToLower());
                    file.SaveAs(path);
                    strName = newName + "." + fileExt.ToLower();
                }
            }
            return strName;
        }
        #endregion
    }
}