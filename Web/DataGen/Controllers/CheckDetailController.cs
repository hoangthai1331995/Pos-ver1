using eTraining.BussinessObjects;
using eTraining.DataObjects;
using System;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class CheckDetailController : AdminController
    {
        // GET: User
        public ActionResult Index()
        {
            ViewBag.TableName = "tblCheckDetail";
            return View();
        }

        [HttpPost]
        public ActionResult AddUpdate(CheckDetail model)
        {
            model.Status = 1;
            model.CreatedDate = DateTime.Now;
            if (new SqlCheckDetailDao().Insert(model))
            {
                TempData["success"] = "Cập nhật thành công";
            }
            else
            {
                TempData["error"] = "Cập nhật thất bại";
            }
            return Redirect("/Admin/CheckDetail");
        }
    }
}