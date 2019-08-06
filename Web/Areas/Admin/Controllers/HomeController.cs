using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Health()
        {
            return View();
        }
        public ActionResult Chart()
        {
            return View();
        }
        public ActionResult ChartExample1()
        {
            return View();
        }
        public ActionResult ChartExample2()
        {
            return View();
        }
    }
}