using eTraining.BussinessObjects;
using eTraining.DataObjects;
using System;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class LibraryDocumentDetailController : AdminController
    {
        // GET: User
        public ActionResult Index()
        {
            ViewBag.TableName = "tblLibraryDocumentDetail";
            return View();
        }

        [HttpPost]
        public ActionResult AddUpdate(LibraryDocumentDetail model)
        {
            model.Status = 1;
            model.CreatedDate = DateTime.Now;
            if (new SqlLibraryDocumentDetailDao().Insert(model))
            {
                TempData["success"] = "Cập nhật thành công";
            }
            else
            {
                TempData["error"] = "Cập nhật thất bại";
            }
            return Redirect("/Admin/LibraryDocumentDetail");
        }
    }
}