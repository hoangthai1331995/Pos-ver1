using BussinessModels;
using BussinessObjects;
using BussinessObjects.Xml;
using Helpers.Functions;
using Helpers.Paging;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class AdminController : Controller
    {
        protected List<string> NoAuthentication = new List<string>();
        protected List<string> LogAction = new List<string>();
        protected List<string> NoLogAction = new List<string>();
        protected bool IsAllowAddUpdate = false;
        protected string TableName = "";

        public User SUser
        {
            get
            {
                if (Session != null)
                {
                    return (User)Session["UserLogin"];
                }
                return null;
            }
            set
            {
                if (value == null)
                    Session.Remove("UserLogin");
                else
                    Session["UserLogin"] = value;
            }
        }

        #region 'Base Action'
        public ActionResult Index()
        {
            return View();
            //DefaultListModel model = new DefaultListModel()
            //{
            //    TableName = TableName,
            //    TargetArea = Request.RequestContext.RouteData.DataTokens["area"].ToString(),
            //    ControllerName = Request.RequestContext.RouteData.Values["controller"].ToString(),
            //    IsAllowAddUpdate = IsAllowAddUpdate
            //};
            //return View(model);
        }
       
        #endregion
        #region 'WriteLog'

        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            //Log the error!!
            WriteLogError((SUser != null) ?  "_" + filterContext.Exception.ToString() : filterContext.Exception.ToString());
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

        #region 'Private Function'
        protected string ParseNull(string value)
        {
            return (!String.IsNullOrEmpty(value) ? value : "");
        }
        #endregion

        #region 'Virtual Method'
        public virtual ActionResult CUSRender()
        {
            return Content("");
        }

        public virtual ActionResult ACTRender(long id = 0)
        {
            return Content("");
        }
        #endregion

        #region Base Function
        public List<FilterParam> GenerateFilterParams()
        {
            string currentArea = Request.RequestContext.RouteData.DataTokens["area"].ToString();
            string currentController = this.ControllerContext.RouteData.Values["controller"].ToString();
            string currentAction = this.ControllerContext.RouteData.Values["action"].ToString();
            string xmlPath = "~/Files/Xml/Filter/" + currentArea.ToLower() + "-" + currentController.ToLower() + "-" + currentAction.ToLower() + ".xml";

            List<FilterParam> Parms = XmlReader.GetListFilterParam(xmlPath);
            #region Generate Default Param
            if (Parms.FirstOrDefault(c => c.name == "Action") == null)
            { Parms.Add(new FilterParam("Action", "get")); }

            Parms.Add(new FilterParam("UserID", ""));
            #endregion
            return Parms;
        }

        public List<FilterParam> GenerateFilterParams(string action = "")
        {

            string currentArea = Request.RequestContext.RouteData.DataTokens["area"].ToString();
            string currentController = this.ControllerContext.RouteData.Values["controller"].ToString();
            string currentAction = this.ControllerContext.RouteData.Values["action"].ToString();
            string xmlPath = "~/Files/Xml/Filter/" + currentArea.ToLower() + "-" + currentController.ToLower() + "-" + action + ".xml";

            List<FilterParam> Parms = XmlReader.GetListFilterParam(xmlPath);
            #region Generate Default Param
            if (Parms.FirstOrDefault(c => c.name == "Action") == null)
            { Parms.Add(new FilterParam("Action", "get")); }

            Parms.Add(new FilterParam("UserID", ""));
            #endregion
            return Parms;
        }

        public List<FilterParam> GenerateFilterParamsByFilePath(string filePath = "")
        {

            string currentArea = Request.RequestContext.RouteData.DataTokens["area"].ToString();
            string currentController = this.ControllerContext.RouteData.Values["controller"].ToString();
            string currentAction = this.ControllerContext.RouteData.Values["action"].ToString();
            string xmlPath = "~/Files/Xml/Filter/" + currentArea.ToLower() + "-" + currentController.ToLower() + "-" + filePath + ".xml";

            List<FilterParam> Parms = XmlReader.GetListFilterParam(xmlPath);
            #region Generate Default Param
            if (Parms.FirstOrDefault(c => c.name == "Action") == null)
            { Parms.Add(new FilterParam("Action", "get")); }

            Parms.Add(new FilterParam("UserID", ""));
            #endregion
            return Parms;
        }


        public List<ListField> GenerateFieldList()
        {
            string currentArea = Request.RequestContext.RouteData.DataTokens["area"].ToString();
            string currentController = this.ControllerContext.RouteData.Values["controller"].ToString();
            string currentAction = this.ControllerContext.RouteData.Values["action"].ToString();
            string xmlPath = "~/Files/Xml/List/" + currentArea.ToLower() + "-" + currentController.ToLower() + "-" + currentAction.ToLower() + ".xml";
            //List<ListField> fields = XmlReader.GetDataFieldForList(xmlPath);
            List<ListField> fields = XmlReader.GetDataFieldForList2(xmlPath);
            return fields;
        }

        public List<ListField> GenerateFieldList2(string fileName)
        {
            string currentArea = Request.RequestContext.RouteData.DataTokens["area"].ToString();
            string currentController = this.ControllerContext.RouteData.Values["controller"].ToString();
            string currentAction = this.ControllerContext.RouteData.Values["action"].ToString();
            string xmlPath = "~/Files/Xml/List/" + currentArea.ToLower() + "-" + currentController.ToLower() + "-" + fileName.ToLower() + ".xml";
            //List<ListField> fields = XmlReader.GetDataFieldForList(xmlPath);
            List<ListField> fields = XmlReader.GetDataFieldForList2(xmlPath);
            return fields;
        }
        public List<ListTab> GenerateTabList()
        {
            string currentArea = Request.RequestContext.RouteData.DataTokens["area"].ToString();
            string currentController = this.ControllerContext.RouteData.Values["controller"].ToString();
            string currentAction = this.ControllerContext.RouteData.Values["action"].ToString();
            string xmlPath = "~/Files/Xml/StatusTab/" + currentArea.ToLower() + "-" + currentController.ToLower() + "-" + currentAction.ToLower() + ".xml";
            List<ListTab> tabs = XmlReader.GetDataTabForList2(xmlPath);
            return tabs;
        }
        public List<SubMenu> GenerateSubMenu()
        {
            string currentArea = Request.RequestContext.RouteData.DataTokens["area"].ToString();
            string currentController = this.ControllerContext.RouteData.Values["controller"].ToString();
            string currentAction = this.ControllerContext.RouteData.Values["action"].ToString();
            string xmlPath = "~/Files/Xml/SubMenu/" + currentArea.ToLower() + "-" + currentController.ToLower() + "-" + currentAction.ToLower() + ".xml";
            List<SubMenu> subMenus = XmlReader.GetSubMenu(xmlPath);
            return subMenus;
        }
        public List<ActionLinkModel> GenerateActionLinkForList()
        {
            string currentArea = Request.RequestContext.RouteData.DataTokens["area"].ToString();
            string currentController = this.ControllerContext.RouteData.Values["controller"].ToString();
            string currentAction = this.ControllerContext.RouteData.Values["action"].ToString();
            string xmlPath = "~/Files/Xml/Action/" + currentArea.ToLower() + "-" + currentController.ToLower() + "-" + currentAction.ToLower() + ".xml";
            List<ActionLinkModel> actions = XmlReader.GetActionLinkForList(xmlPath);
            return actions;
        }

        public string GenerateFileNameForReport(string fileHeader = "", string fileType = ".xlsx")
        {
            string reportFilePath;
            if (string.IsNullOrEmpty(fileHeader) == true)
            {
                string currentArea = Request.RequestContext.RouteData.DataTokens["area"].ToString();
                string currentController = this.ControllerContext.RouteData.Values["controller"].ToString();
                string currentAction = this.ControllerContext.RouteData.Values["action"].ToString();
                reportFilePath = "/Files/FileExport/-" + currentArea.ToLower() + "-" + currentController.ToLower() + "-" + currentAction.ToLower() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + fileType;
            }
            else
            {
                reportFilePath = "/Files/FileExport/" + fileHeader + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + fileType;
            }
            return reportFilePath;
        }


        public void InitFilterParamFirst(List<FilterParam> FilterParams)
        {
            if (FilterParams != null && FilterParams.Count() > 0)
            {
                foreach (FilterParam p in FilterParams)
                {
                    if (p.type != "status")
                    {
                        p.html = this.RenderRazorParamTemplateSingle(this, p);
                    }
                }
            }
        }
        public void InitFilterParamFirst(List<FilterParam> FilterParams, bool isAddUpdate)
        {
            if (FilterParams != null && FilterParams.Count() > 0)
            {
                foreach (FilterParam p in FilterParams)
                {
                    if (p.type != "status")
                    {
                        p.html = this.RenderRazorFieldHeader(this, p);
                    }
                }
            }
        }

        public List<FilterParam> InitFilterParamWithData(List<FilterParam> FilterParams)
        {
            List<FilterParam> newFilterParams = new List<FilterParam>();
            if (FilterParams != null && FilterParams.Count() > 0)
            {
                //newFilterParams = FilterParams.Where(m => m.type != "daterand").ToList();
                foreach (FilterParam p in FilterParams)
                {
                    if (p.type == "daterand")
                    {
                        string[] nameL = (p.name.Contains("-")) ? p.name.Split('-') : new string[0];
                        if (nameL.Count() == 2)
                        {
                            FilterParam p1 = new FilterParam();
                            p1.name = nameL[0];
                            string[] arrP1 = Request[nameL[0]].Split('/');

                            p1.value = (arrP1.Count() == 3) ? arrP1[2] + "-" + arrP1[1] + "-" + arrP1[0] : "";
                            newFilterParams.Add(p1);

                            FilterParam p2 = new FilterParam();
                            p2.name = nameL[1];
                            string[] arrP2 = Request[nameL[1]].Split('/');
                            p2.value = (arrP2.Count() == 3) ? arrP2[2] + "-" + arrP2[1] + "-" + arrP2[0] : "";
                            newFilterParams.Add(p2);
                        }
                    }
                    else if (p.type == "dateone")
                    {

                        FilterParam p1 = new FilterParam();
                        p1.name = p.name;
                        string[] arrP1 = Request[p.name].Split('/');

                        p1.value = (arrP1.Count() == 3) ? arrP1[2] + "-" + arrP1[1] + "-" + arrP1[0] : "";
                        newFilterParams.Add(p1);



                    }
                    else if (p.type == "status")
                    {
                        p.value = Request["status"];
                        newFilterParams.Add(p);
                    }
                    else if (p.type == "key")
                    {
                        p.value = "-1";
                        newFilterParams.Add(p);
                    }
                    else
                    {
                        p.value = Request[p.name] != null ? Request[p.name] : "";
                        newFilterParams.Add(p);
                    }
                }
                FilterParams = newFilterParams;
            }
            //#region Generate Default Param
            //newFilterParams.Add(new BaseModel.FilterParam("Action", "get"));
            //newFilterParams.Add(new BaseModel.FilterParam("UserID", SUserName));
            //#endregion
            return newFilterParams;
        }

        //public void InitList<T>(ListModel<T> model)
        //{
        //    model.FieldLists = this.GenerateFieldList();
        //    model.FilterParams = this.GenerateFilterParams();
        //    model.ActionLinks = this.GenerateActionLinkForList();
        //    model.TabLists = this.GenerateTabList();
        //    model.FilterParams = this.InitFilterParamWithData(model.FilterParams);
        //}

        //public void InitList<T>(ListModel<T> model, string action)
        //{
        //    model.FieldLists = this.GenerateFieldList2(action);
        //    model.FilterParams = this.GenerateFilterParams(action);
        //    model.ActionLinks = this.GenerateActionLinkForList();
        //    model.TabLists = this.GenerateTabList();
        //    model.FilterParams = this.InitFilterParamWithData(model.FilterParams);
        //}
        //public void InitListForExcel<T>(ListModel<T> model, string action)
        //{
        //    model.FieldLists = this.GenerateFieldList2(action);
        //    model.FilterParams = this.GenerateFilterParams(action);
        //    model.ActionLinks = this.GenerateActionLinkForList();
        //    model.TabLists = this.GenerateTabList();
        //    model.FilterParams = this.InitFilterParamWithData(model.FilterParams);
           
        //}

        public string RenderRazorViewToString(Controller controller, string viewName, DataTable data, List<ListField> Fields, List<ActionLinkModel> ActionLinks, string status, Paging paging)
        {
            if (paging.TotalRecords == 0)
            {
                paging.TotalRecords = data != null && data.Rows.Count > 0 ? Convert.ToInt32(data.Rows[0]["TotalRec"]) : 0;
            }
            controller.ViewData["DataBind"] = data;
            controller.ViewData["Fields"] = Fields;
            controller.ViewData["ActionLinks"] = ActionLinks;
            controller.ViewData["Paging"] = paging;
            controller.ViewData["status"] = status;
            controller.ViewData["Trans_type"] = status;
            //controller.ViewData["SerivceUrl"] = ServiceUrl;
            //controller.ViewData["CurrentLogin"] = SUserName;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        public string RenderRazorParamTemplateSingle(Controller controller, FilterParam param)
        {
            string viewName = "";
            controller.ViewData["Param"] = param;
            switch (param.type)
            {

                case "textbox":
                    viewName = "~/Views/Shared/ParamTemplates/_textBox.cshtml";
                    break;
                case "number":
                    viewName = "~/Views/Shared/ParamTemplates/_number.cshtml";
                    break;
                case "datetime":
                    viewName = "~/Views/Shared/ParamTemplates/_dateTime.cshtml";
                    break;
                case "daterand":
                    viewName = "~/Views/Shared/ParamTemplates/_dateRand.cshtml";
                    break;
                case "dateone":
                    viewName = "~/Views/Shared/ParamTemplates/_dateOneField.cshtml";
                    break;
                case "combobox":
                    //controller.ViewData["SelectListData"] = Func_Base.ToSelectList(ServiceUrl, param.databinding);
                    //viewName = "~/Views/Shared/ParamTemplates/_comboBox.cshtml";
                    break;
                //case "multiselect":
                //    controller.ViewData["SelectListData"] = Func_Base.ToSelectList(ServiceUrl, param.databinding, param.tablebinding, param.fnamebinding, param.fvaluebinding);
                //    viewName = "~/Views/Shared/ParamTemplates/_multiselect.cshtml";
                //    break;
                //case "dynamic_combobox":
                //    controller.ViewData["SelectListData"] = Func_Base.ToSelectList(ServiceUrl, param.databinding, param.tablebinding, param.fnamebinding, param.fvaluebinding);
                //    viewName = "~/Views/Shared/ParamTemplates/_dynamic_combobox.cshtml";
                //    break;
                case "file":
                    viewName = "~/Views/Shared/ParamTemplates/_fileUpload.cshtml";
                    break;
                default:
                    viewName = "~/Views/Shared/ParamTemplates/_blank.cshtml";
                    break;
            }
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
        public string RenderRazorFieldHeader(Controller controller, FilterParam param)
        {
            string viewName = "";
            controller.ViewData["Param"] = param;
            switch (param.type)
            {

                case "textbox":
                    viewName = "~/Views/Shared/BodyHeader/_textBox.cshtml";
                    break;
                case "number":
                    viewName = "~/Views/Shared/BodyHeader/_number.cshtml";
                    break;
                case "datetime":
                    viewName = "~/Views/Shared/BodyHeader/_dateTime.cshtml";
                    break;
                case "daterand":
                    viewName = "~/Views/Shared/BodyHeader/_dateRand.cshtml";
                    break;
                case "dateone":
                    viewName = "~/Views/Shared/BodyHeader/_dateOneField.cshtml";
                    break;
                //case "combobox":
                    //controller.ViewData["SelectListData"] = Func_Base.ToSelectList(ServiceUrl, param.databinding);
                    //viewName = "~/Views/Shared/ParamTemplates/_comboBox.cshtml";
                    break;
                //case "multiselect":
                //    controller.ViewData["SelectListData"] = Func_Base.ToSelectList(ServiceUrl, param.databinding, param.tablebinding, param.fnamebinding, param.fvaluebinding);
                //    viewName = "~/Views/Shared/BodyHeader/_multiselect.cshtml";
                //    break;
                //case "dynamic_combobox":
                //    controller.ViewData["SelectListData"] = Func_Base.ToSelectList(ServiceUrl, param.databinding, param.tablebinding, param.fnamebinding, param.fvaluebinding);
                //    viewName = "~/Views/Shared/BodyHeader/_dynamic_combobox.cshtml";
                //    break;
                case "file":
                    viewName = "~/Views/Shared/BodyHeader/_fileUpload.cshtml";
                    break;
                default:
                    viewName = "~/Views/Shared/BodyHeader/_blank.cshtml";
                    break;
            }
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

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