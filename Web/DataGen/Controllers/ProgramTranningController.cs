using eTraining.BussinessObjects;
using eTraining.DataObjects;
using System;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class ProgramTranningController : AdminController
    {
        // GET: User
        public ActionResult Index()
        {
            ViewBag.TableName = "tblProgramTranning";
            return View();
        }

        [HttpPost]
        public ActionResult AddUpdate(ProgramTranning model)
        {
            model.Status = 1;
            model.CreatedDate = DateTime.Now;
            if (new SqlProgramTranningDao().Insert(model))
            {
                TempData["success"] = "Cập nhật thành công";
            }
            else
            {
                TempData["error"] = "Cập nhật thất bại";
            }
            return Redirect("/Admin/ProgramTranning");
        }
    }
}