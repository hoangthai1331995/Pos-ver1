using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Categories.Controllers
{
    public class CategoriesBaseController : Controller
    {
        // GET: CategoriesBase
        public ActionResult Index()
        {
            return View();
        }
    }
}