using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Configulation.Controllers
{
    public class PanelController : ConfigulationController
    {
        // GET: Panel
        public ActionResult Index()
        {
            return View();
        }
    }
}