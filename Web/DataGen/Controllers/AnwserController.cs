using eTraining.BussinessObjects;
using eTraining.DataObjects;
using System;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class AnwserController : AdminController
    {
        // GET: User
        public ActionResult Index()
        {
            ViewBag.TableName = "tblAnwser";
            return View();
        }

        [HttpPost]
        public ActionResult AddUpdate(Anwser model)
        {
            model.Status = 1;
            model.CreatedDate = DateTime.Now;
            if (new SqlAnwserDao().Insert(model))
            {
                TempData["success"] = "Cập nhật thành công";
            }
            else
            {
                TempData["error"] = "Cập nhật thất bại";
            }
            return Redirect("/Admin/Anwser");
        }
    }
}