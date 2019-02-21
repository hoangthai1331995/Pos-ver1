using BussinessModels;
using BussinessObjects;
using DataObjects;
using Helper.Functions;
using Helpers.Functions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        private bool isADAuthenticate = bool.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["IsADAuthenticate"]);
        private string serviceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
        // GET: User
        public ActionResult Login(string returnUrl = "/TankList/Home")
        {
            LoginModel model = new LoginModel()
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            string result = "", actiontype = "";
            NI10_Employee employee = new SqlNI10_EmployeeDao().GetSingleByCustomDataSource(new object[] { "@employeeCode", model.LoginName }, "[dbo].[NI10_Employee_GetEmployeeInfo]");
            if (employee != null)
            {
                if (isADAuthenticate)
                {
                    if (ValidateUserLDAP(model.LoginName, model.PassWord.Trim()) == false)
                    {
                        UserPrincipal userPrincipal = LdapFunction.GetUserPrincipal(model.LoginName);
                        if (userPrincipal != null)
                        {
                            if (userPrincipal.AccountExpirationDate != null)
                            {
                                actiontype = "1";
                                result = "Tài khoản đã nghỉ việc. Vui lòng liên hệ support để hỗ trợ";
                            }
                            else if (userPrincipal.AccountLockoutTime != null)
                            {
                                actiontype = "1";
                                result = "Tài khoản của bạn đã bị khóa. Vui lòng liên hệ support để hỗ trợ";
                                Session["countLoginFail"] = "0";
                            }
                            else
                            {
                                result = "Username hoặc password không đúng. Vui lòng kiểm tra lại";
                                actiontype = "1";
                            }
                        }
                        TempData["error"] = result;
                    }
                    else
                    {
                        actiontype = "0";
                        this.FillLoginInfo(employee.JobTitle, model.LoginName.Trim(), employee.FullName, employee.EmployeeCode, "", false, "", "", employee.Avatar, "false", 0, StaticFunc.EncryptIntranet(model.PassWord.Trim()), employee.EmailAddress);
                        if (model.ReturnUrl == null)
                            return Redirect("/TankList/Home");
                        return Redirect(model.ReturnUrl);
                    }
                }
            }
            return View();
        }

        public ActionResult LoginInfo()
        {
            NI10_Employee employee = (Session["EmployeeLogin"] != null) ? (NI10_Employee)Session["EmployeeLogin"] : new NI10_Employee();
            return View(employee);
        }

        public ActionResult MenuInfo()
        {
            string serviceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
            NI10_Employee employee = (Session["EmployeeLogin"] != null) ? (NI10_Employee)Session["EmployeeLogin"] : new NI10_Employee();
            List<NI10_Menu> menus = DataFunction.GetCustomObjectListFromService<NI10_Menu>(serviceUrl, "Request/GetData", "", new List<string>() { "@employeeCode", employee.EmployeeCode }, "[dbo].[NI10_Menu_GetByEmployeeCode]");
            return View(menus);
        }

        public ActionResult Logout()
        {
            Session["EmployeeLogin"] = null;
            return Redirect("/User/Login?returnUrl=/");
        }

        public ActionResult Profile()
        {
            if (Session["EmployeeLogin"] == null)
                return Redirect("/User/Login?returnUrl=/User/Profile");
            NI10_Employee employee = (Session["EmployeeLogin"] != null) ? (NI10_Employee)Session["EmployeeLogin"] : new NI10_Employee();
            employee = new SqlNI10_EmployeeDao().GetSingleByCustomDataSource(new object[] { "@employeeCode", employee.EmployeeCode }, "[dbo].[NI10_Employee_GetEmployeeInfo]");
            ViewBag.UserInfo = employee;
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            try
            {
                NI10_Employee employee = (Session["EmployeeLogin"] != null) ? (NI10_Employee)Session["EmployeeLogin"] : new NI10_Employee();
                bool isADAuthenticate = bool.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["IsADAuthenticate"]);
                if (isADAuthenticate)
                {
                    bool isValidateUser = LdapFunction.IsExists(employee.EmployeeCode);
                    if (isValidateUser)
                    {
                        if (!String.IsNullOrEmpty(model.NewPassword))
                        {
                            if (StaticFunc.EncryptIntranet(model.CurrentPassword) == employee.CurrentPassword)
                            {
                                LdapFunction.ResetPassword(employee.EmployeeCode, model.NewPassword);
                                employee.CurrentPassword = StaticFunc.EncryptIntranet(model.NewPassword);
                                Session["EmployeeLogin"] = employee;
                            }
                            else
                            {
                                TempData["error"] = "Mật khẩu cũ không trùng khớp";
                            }
                        }
                    }
                    else
                    {
                        TempData["error"] = "Không tìm thấy tài khoản của bạn";
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("The password does not meet the password policy requirements"))
                {
                    TempData["error"] = "Mật khẩu không được chứa họ,tên,tên đệm";
                }
                else
                {
                    TempData["error"] = ex.ToString();
                }
            }
            if (TempData["error"] != null)
                return Redirect("/User/Profile");
            else
            {
                TempData["success"] = "Cập nhập thành công!";
                return Redirect("/User/Login/?returnUrl=/");
            }
        }

        [HttpPost]
        public JsonResult UpdateInfo(string phone, string address, int marry, string ipphone, string noteContact)
        {
            try
            {
                NI10_Employee employee = (Session["EmployeeLogin"] != null) ? (NI10_Employee)Session["EmployeeLogin"] : new NI10_Employee();
                PutDataResponse dataResponse = DataFunction.PutDataToService(serviceUrl, "Request/GetData", "", 
                                                                            new List<string>() {
                                                                                                    "@EmployeeCode", employee.EmployeeCode,
                                                                                                    "@PhoneNumber", phone,
                                                                                                    "@Address", address,
                                                                                                    "@Marry", marry.ToString(),
                                                                                                    "@IPphone", ipphone,
                                                                                                    "@NoteContact", noteContact }, 
                                                                            "[dbo].[NI10_Employee_UpdateInfo]");
               
                if (dataResponse.StatusCode == "DONE")
                    return Json("1", JsonRequestBehavior.AllowGet);
                else
                    return Json("0", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UploadAvatar(HttpPostedFileBase fileImport = null)
        {
            try
            {
                NI10_Employee employee = (Session["EmployeeLogin"] != null) ? (NI10_Employee)Session["EmployeeLogin"] : new NI10_Employee();
                string l_imageFileName = "";
                if (Request.Files != null)
                {
                    foreach (string file in Request.Files)
                    {
                        var uploadedFile = Request.Files[file];
                        if (uploadedFile.ContentLength > 0)
                        {
                            List<string> array = new List<string> { "jpg", "JPG", "jpeg", "JPEG", "icon", "BMP", "bmp", "png", "PNG" };
                            if (array.Contains(uploadedFile.ContentType.Split('/')[1].ToString()) == true)
                            {
                                Bitmap photoFile = StaticFunc.ResizeImage(uploadedFile.InputStream, 200, 200);
                                string img_duoi = "." + uploadedFile.ContentType.Split('/')[1].ToString();
                                l_imageFileName = employee.EmployeeCode + DateTime.Now.ToString("yyyyMMddHHmmss") + img_duoi;
                                var appData = Server.MapPath("/Files/Avatar/");
                                ImageFormat format = StaticFunc.ConvertImageToByteArray(img_duoi);
                                photoFile.Save(Server.MapPath("/Files/Avatar/" + l_imageFileName), format);
                                if (l_imageFileName != "")
                                {
                                    PutDataResponse dataResponse = DataFunction.PutDataToService(serviceUrl, "Request/GetData", "",
                                                                           new List<string>() {
                                                                                                    "@EmployeeCode", employee.EmployeeCode,
                                                                                                    "@Avatar", l_imageFileName
                                                                                              },
                                                                           "[dbo].[NI10_Employee_UpdateAvatar]");
                                    return Json("/Files/Avatar/" + l_imageFileName, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                                return Json("0", JsonRequestBehavior.AllowGet); // File không đúng định dạng ảnh. Vui lòng đổi ảnh khác
                        }
                    }
                }
                return Json("-1", JsonRequestBehavior.AllowGet); // Xảy ra lỗi trong quá trình xử lý dữ liệu
            }
            catch (Exception ex)
            {
                return Json("-1", JsonRequestBehavior.AllowGet); // Xảy ra lỗi trong quá trình xử lý dữ liệu
            }
        }

        public ActionResult RequiredPermission(string k = "")
        {
            string keyDescrypt = "";
            if (!String.IsNullOrEmpty(k))
            {
                keyDescrypt = StaticFunc.Base64Decode(k);
            }
            ViewBag.PermissionCode = keyDescrypt;
            return View();
        }

        public void FillLoginInfo(string jobTitleName, string userId, string displayName, string employeeCode, string warehouseCode, bool isAdmin, string roleId, string tabRole, string avatarLink, string isPopup, int changePass, string key, string email)
        {
            NI10_Employee currentLogin = new NI10_Employee
            {
                EmployeeCode = (userId == "admininside" ? "4195" : userId),
                JobTitle = jobTitleName,
                FullName = displayName,
                Avatar = avatarLink,
                CurrentPassword = key,
                EmailAddress = email
            };
            Session["EmployeeLogin"] = currentLogin;
        }

        public bool ValidateUserLDAP(string userName, string password)
        {
            try
            {
                bool kt = LdapFunction.ValidateUser(userName, password);
                return kt;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}