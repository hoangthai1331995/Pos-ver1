using eTraining.BussinessObjects;
using eTraining.DataObjects;
using System;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class DepartmentController : AdminController
    {
        // GET: User
        public ActionResult Index()
        {
            ViewBag.TableName = "tblDepartment";
            return View();
        }

        [HttpPost]
        public ActionResult AddUpdate(Department model)
        {
            model.Status = 1;
            model.CreatedDate = DateTime.Now;
            if (new SqlDepartmentDao().Insert(model))
            {
                TempData["success"] = "Cập nhật thành công";
            }
            else
            {
                TempData["error"] = "Cập nhật thất bại";
            }
            return Redirect("/Admin/Department");
        }
    }
}