using BussinessModels;
using DataObjects;
using Helpers.Functions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TaskList.Models;

namespace TaskList.Controllers
{
    public class HomeController : TaskListController
    {
        // GET: Workflow
        public ActionResult Index(string status = "")
        {
            string serviceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
            List<TasklistModel> dataResponse = DataFunction.GetCustomObjectListFromService<TasklistModel>
            (
               serviceUrl, "Request/GetData", "", new List<string>() {
                   "@SSID", "", "@ACTION", "", "@OBJECTID", "", "@USERID", SEmployee.EmployeeCode, "@StatusType", "", "@TaskType", "", "@TaskID", ""
               }, "[dbo].[NI09_TaskList_Items_GetlList]"
            );
            ViewBag.ListTasklist = dataResponse;

            List<ListTasklistStatusModel> dataResponse2 = DataFunction.GetCustomObjectListFromService<ListTasklistStatusModel>
            (
               serviceUrl, "Request/GetData", "", new List<string>() { }, "[dbo].[NI09_TaskList_GetListStatus]"
            );
            ViewBag.ListStatus = dataResponse2;
            ViewBag.ListTaskType = new SqlSelectListDao().GetSelectLists("spSelectList_NI09_TaskList_TaskTypeList");
            return View();
        }

        // GET: Detail
        public ActionResult Detail(string id)
        {
            string serviceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
            List<TasklistModel> dataResponse = DataFunction.GetCustomObjectListFromService<TasklistModel>
            (
               serviceUrl, "Request/GetData", "", new List<string>() {
                   "@SSID", "", "@ACTION", "", "@OBJECTID", "", "@USERID", SEmployee.EmployeeCode, "@StatusType", "", "@TaskType", "", "@TaskID", id
               }, "[dbo].[NI09_TaskList_Items_GetlList]"
            );

            List<ListTasklistStatusModel> dataResponse2 = DataFunction.GetCustomObjectListFromService<ListTasklistStatusModel>
            (
               serviceUrl, "Request/GetData", "", new List<string>() { }, "[dbo].[NI09_TaskList_GetListStatus]"
            );
            ViewBag.ListStatus = dataResponse2;
            TasklistModel model = new TasklistModel();
            TasklistModel taskParentDetail = new TasklistModel();
            ViewBag.taskParentDetail = taskParentDetail;
            if (dataResponse.Count > 0)
            {
                model = dataResponse.FirstOrDefault();
                model.OptionListDB = new SqlSelectListDao().GetSelectLists("NI09_TaskList_OptionList_GetList");


                model.OptionListView = (from a in model.OptionStringItemList
                                        join b in model.OptionListDB on a.KeyName equals b.Code
                                        select new OptionListModel
                                        {
                                            Code = a.KeyName,
                                            Name = b.Name,
                                            Val = a.KeyVal
                                        }).ToList();
                if (model.ParentID > 0)
                {
                    List<TasklistModel> dataResponse3 = DataFunction.GetCustomObjectListFromService<TasklistModel>
                    (
                       serviceUrl, "Request/GetData", "", new List<string>() {
                           "@SSID", "", "@ACTION", "", "@OBJECTID", "", "@USERID", "", "@StatusType", "", "@TaskType", "", "@TaskID", model.ParentID.ToString()
                       }, "[dbo].[NI09_TaskList_Items_GetlList]"
                    );
                    if (dataResponse3.Count > 0)
                    {
                        taskParentDetail = dataResponse3.FirstOrDefault();
                        ViewBag.taskParentDetail = taskParentDetail;
                    }
                }
            }
            // model.TaskTypeList = new SqlSelectListDao().GetSelectLists("spSelectList_NI09_TaskList_TaskTypeList");
            return View(model);
        }
        // GET: AddUpdate
        public ActionResult AddUpdate(string id)
        {
            TasklistModel model = new TasklistModel();
            string serviceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
            if (!String.IsNullOrEmpty(id))
            {
                List<TasklistModel> content = DataFunction.GetCustomObjectListFromService<TasklistModel>
                (
                   serviceUrl, "Request/GetData", "", new List<string>() {
                       "@SSID", "", "@ACTION", "", "@OBJECTID", "", "@USERID", SEmployee.EmployeeCode, "@StatusType", "", "@TaskType", "", "@TaskID", id
                   }, "[dbo].[NI09_TaskList_Items_GetlList]"
                );
                model = content.FirstOrDefault();
                model.OptionListDB = new SqlSelectListDao().GetSelectLists("NI09_TaskList_OptionList_GetList");


                model.OptionListView = (from a in model.OptionStringItemList
                                        join b in model.OptionListDB on a.KeyName equals b.Code
                                        select new OptionListModel
                                        {
                                            Code = a.KeyName,
                                            Name = b.Name,
                                            Val = a.KeyVal
                                        }).ToList();
            }
            else
            {
                model.OptionListDB = new SqlSelectListDao().GetSelectLists("NI09_TaskList_OptionList_GetList");
                model.OptionListView = (from a in model.OptionListDB
                                        select new OptionListModel
                                        {
                                            Code = a.Code,
                                            Name = a.Name,
                                            Val = false
                                        }).ToList();
            }
            model.TaskTypeList = new SqlSelectListDao().GetSelectLists("spSelectList_NI09_TaskList_TaskTypeList");
            model.ObjectInChargeTypeList = new SqlSelectListDao().GetSelectLists("spSelectList_NI09_TaskList_ObjectInChargeTypeList");
            model.PriorityList = new SqlSelectListDao().GetSelectLists("spSelectList_NI09_TaskList_PriorityLevel");
            List<ListTasklistStatusModel> dataResponse2 = DataFunction.GetCustomObjectListFromService<ListTasklistStatusModel>
            (
               serviceUrl, "Request/GetData", "", new List<string>() { }, "[dbo].[NI09_TaskList_GetListStatus]"
            );
            ViewBag.ListStatus = dataResponse2;

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddUpdate(TasklistModel model, HttpPostedFileBase[] AttachFile)
        {
            WriteLogError(JsonConvert.SerializeObject(model));

            #region Upload File
            List<FileStringItem> fileStringItems = new List<FileStringItem>();
            if (model.FileString != null)
            {
                fileStringItems = JsonConvert.DeserializeObject<List<FileStringItem>>(model.FileString);
            }
            if (AttachFile != null && AttachFile.Count() > 0 && AttachFile[0] != null)
            {
                foreach(HttpPostedFileBase file in AttachFile)
                {
                    string fileName = UploadFile(file, "/Files/TaskList");
                    FileStringItem fileItem = new FileStringItem()
                    {
                        FileID = 0,
                        FileName = fileName,
                        FilePath = "/Files/TaskList/" + fileName
                    };
                    fileStringItems.Add(fileItem);
                }
            }
            #endregion

            List<CheckListItem> checkListItems = JsonConvert.DeserializeObject<List<CheckListItem>>(model.CheckList);
            List<OptionStringItem> optionItems = JsonConvert.DeserializeObject<List<OptionStringItem>>(model.OptionString);

            List <ObjectStringModel> ObjectString = new List<ObjectStringModel>();
            string fileStr = JsonConvert.SerializeObject(fileStringItems).ToString();
            ObjectStringModel abc = new ObjectStringModel
            {
                TaskID = model.TaskID,
                ParentID = model.ParentID,
                TaskTitle = model.TaskTitle,
                ItemsDesc = model.ItemsDesc,
                TaskType = model.TaskType,
                StatusType = model.StatusType,
                FileString = fileStringItems,
                PriorityLevel = model.PriorityLevel,
                CheckList = checkListItems,
                OptionString = optionItems,
                FromDateTime = StaticFunc.ConvertFormDateTime(Request["FromDateTime"]).ToString("MM/dd/yyyy HH:mm"),
                ToDateTime = StaticFunc.ConvertFormDateTime(Request["ToDateTime"]).ToString("MM/dd/yyyy HH:mm"),
                CompletedDatetime = StaticFunc.ConvertFormDateTime(Request["CompletedDatetime"]).ToString("MM/dd/yyyy HH:mm"),
                FromDateRepeat = StaticFunc.ConvertFormDate(Request["FromDateRepeat"]).ToString("MM/dd/yyyy HH:mm"),
                ToDateRepeat = StaticFunc.ConvertFormDate(Request["ToDateRepeat"]).ToString("MM/dd/yyyy HH:mm"),
                IsRepeated = model.IsRepeated,
                RepeatType = model.RepeatType,
                InTimeString = model.InTimeString,
                InWeekdayString = model.InWeekdayString,
                InMonthdayString = model.InMonthdayString,
                ObjectInChargeType = model.ObjectInChargeType,
                ObjectInCharge = model.ObjectInCharge,
                IsActive = "1",
                IsFollow = Request["IsFollow"] == "1" ? "True" : "False",
                PersonsControl = model.PersonsControl,
            };
            ObjectString.Add(abc);
            string test = JsonConvert.SerializeObject(ObjectString);
            WriteLogError(Request["FromDateTime"].ToString());
            WriteLogError(Request["ToDateTime"].ToString());
            WriteLogError(test);
            //return View();
            string serviceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
            PutDataResponse AddUpdateTask = DataFunction.PutDataToService(serviceUrl, "Request/GetData", "", new List<string>() {
                   "@SSID", "", "@ACTION", "SAVE", "@OBJECTID", "", "@USERID", SEmployee.EmployeeCode, "@TaskID", model.TaskID.ToString(), "@ObjectString", test
               }, "[dbo].[NI09_TaskList_Items_save]");
            if (AddUpdateTask.StatusCode == "DONE")
                TempData["success"] = "Cập nhật dữ liệu thành công";
            else
                TempData["error"] = AddUpdateTask.StatusMess;
            return Redirect("/TaskList/Home/Detail?id=" + model.TaskID);
        }

        #region 'Ajax'
        public JsonResult LoadObjectInChargeType(string itemCode = "", string ItemCode_KeySeach = "")
        {
            string serviceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
            List<BussinessObjects.SelectList> dataList = DataFunction.GetCustomObjectListFromService<BussinessObjects.SelectList>
                (
                   serviceUrl, "Request/GetData", "", new List<string>() {
                       "@SSID", "", "@ACTION", "", "@OBJECTID", "", "@USERID", SEmployee.EmployeeCode, "@ItemCode", itemCode, "@ItemCode_KeySeach", ItemCode_KeySeach
                   }, "[dbo].[NI09_TaskList_ObjectInCharge_GetListByType]"
                );
            return Json(dataList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadObjectInChargeTypeByUser(string lstItemCode = "", string ItemType = "")
        {
            string serviceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
            List<BussinessObjects.SelectList> dataList = DataFunction.GetCustomObjectListFromService<BussinessObjects.SelectList>
                (
                   serviceUrl, "Request/GetData", "", new List<string>() {
                       "@lstItemCode", lstItemCode, "ItemType", ItemType
                   }, "[dbo].[NI09_TaskList_ObjectInCharge_List_GetByListItemCode]"
                );
            return Json(dataList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SearchTaskParent(string TaskTitle = "")
        {
            string serviceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
            List<BussinessObjects.SelectList> dataList = DataFunction.GetCustomObjectListFromService<BussinessObjects.SelectList>
                (
                   serviceUrl, "Request/GetData", "", new List<string>() {
                       "@TaskTitle", TaskTitle
                   }, "[dbo].[NI09_TaskList_Items_Search]"
                );
            return Json(dataList, JsonRequestBehavior.AllowGet);
        }
        #endregion


        public ActionResult ViewList(string typeAction = "", string status = "", string taskType = "")
        {
            string serviceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
            List<TasklistModel> dataResponse = DataFunction.GetCustomObjectListFromService<TasklistModel>
            (
               serviceUrl, "Request/GetData", "", new List<string>() {
                   "@SSID", "", "@ACTION", typeAction, "@OBJECTID", "", "@USERID", SEmployee.EmployeeCode, "@StatusType", status, "@TaskType", taskType, "@TaskID", ""
               }, "[dbo].[NI09_TaskList_Items_GetlList]"
            );

            List<ListTasklistStatusModel> dataResponse2 = DataFunction.GetCustomObjectListFromService<ListTasklistStatusModel>
            (
               serviceUrl, "Request/GetData", "", new List<string>() { }, "[dbo].[NI09_TaskList_GetListStatus]"
            );
            ViewBag.ListStatus = dataResponse2;
            return View(dataResponse);
        }

        public ActionResult ViewCalendar(string typeAction = "", string status = "", string taskType = "")
        {
            string serviceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
            List<TasklistModel> dataResponse = DataFunction.GetCustomObjectListFromService<TasklistModel>
            (
               serviceUrl, "Request/GetData", "", new List<string>() {
                   "@SSID", "", "@ACTION", typeAction, "@OBJECTID", "", "@USERID", SEmployee.EmployeeCode, "@StatusType", status, "@TaskType", taskType, "@TaskID", ""
               }, "[dbo].[NI09_TaskList_Items_GetlList]"
            );

            List<ListTasklistStatusModel> dataResponse2 = DataFunction.GetCustomObjectListFromService<ListTasklistStatusModel>
            (
               serviceUrl, "Request/GetData", "", new List<string>() { }, "[dbo].[NI09_TaskList_GetListStatus]"
            );
            ViewBag.ListStatus = dataResponse2;
            return View(dataResponse);
        }

        public ActionResult ViewCol(string typeAction = "", string status = "", string taskType = "")
        {
            string serviceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
            List<TasklistModel> dataResponse = DataFunction.GetCustomObjectListFromService<TasklistModel>
            (
               serviceUrl, "Request/GetData", "", new List<string>() {
                   "@SSID", "", "@ACTION", typeAction, "@OBJECTID", "", "@USERID", SEmployee.EmployeeCode, "@StatusType", status, "@TaskType", taskType, "@TaskID", ""
               }, "[dbo].[NI09_TaskList_Items_GetlList]"
            );

            List<ListTasklistStatusModel> dataResponse2 = DataFunction.GetCustomObjectListFromService<ListTasklistStatusModel>
            (
               serviceUrl, "Request/GetData", "", new List<string>() { }, "[dbo].[NI09_TaskList_GetListStatus]"
            );
            ViewBag.ListStatus = dataResponse2;
            return View(dataResponse);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public JsonResult ChangeStatus(string TaskID, string StatusType)
        {

            string serviceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
            PutDataResponse updateStatus = DataFunction.PutDataToService(serviceUrl, "Request/GetData", "", new List<string>() {
                   "@StatusType", StatusType, "@TaskID", TaskID, "@CheckList", "", "@IsFollow", ""
               }, "[dbo].[NI09_TaskList_Items_Update]");

            return Json(updateStatus, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Follow(string TaskID, string IsFollow)
        {

            string serviceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
            PutDataResponse updateStatus = DataFunction.PutDataToService(serviceUrl, "Request/GetData", "", new List<string>() {
                   "@IsFollow", IsFollow, "@TaskID", TaskID
               }, "[dbo].[NI09_TaskList_Items_Update]");

            return Json(updateStatus, JsonRequestBehavior.AllowGet);
        }

        public string ChangeIsCheckCheckList(string TaskID, string CheckList)
        {
            string serviceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
            PutDataResponse updateIsCheck = DataFunction.PutDataToService(serviceUrl, "Request/GetData", "", new List<string>() {
                   "@TaskID", TaskID, "@StatusType", "", "@CheckList", CheckList, "@IsFollow", "" }, "[dbo].[NI09_TaskList_Items_Update]");
            return JsonConvert.SerializeObject(updateIsCheck);
        }
        public JsonResult AddTaskType(string ItemName)
        {

            string serviceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
            List<BussinessObjects.SelectList> dataList = DataFunction.GetCustomObjectListFromService<BussinessObjects.SelectList>
                (
                   serviceUrl, "Request/GetData", "", new List<string>() {
                       "@ItemName", ItemName
                   }, "[dbo].[NI09_TaskList_ListTaskType_Save]"
                );
            return Json(dataList, JsonRequestBehavior.AllowGet);
        }
        public void DownLoadPopup(string Path, string FileName)
        {
            //FtpCdnAccount ftpCdnAccount = LoadDefaultCdn();
            //string l_Path = Path;
            //string l_filename = FileName;
            //if (l_Path != null && l_Path != "")
            //{
            //    string localContentPath = FileDownloadSFTP(ftpCdnAccount, "/Document/Files/" + l_Path, l_Path);
            //    FileInfo file = new FileInfo(Server.MapPath(localContentPath));
            //    Response.Clear();
            //    Response.AddHeader("Content-Disposition", "attachment; filename=\"" + l_filename + "\"");
            //    Response.ContentType = Helper.FunctionHelper.GetExtension(file.Extension);
            //    Response.TransmitFile(file.FullName);
            //    Response.End();
            //}
        }
    }
}