using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Configulation.Controllers
{
    public class ControlController : ConfigulationController
    {
        public ControlController()
        {
            NoDetectDatabase.Add("ComboboxByDataSource");
        }
        // GET: Control
        [HttpPost]
        public JsonResult ComboboxByDataSource(string dataSource = "")
        {
            try
            {
                if (!String.IsNullOrEmpty(dataSource))
                {
                    List<BussinessObjects.SelectList> selectListItems = new SqlSelectListDao().GetSelectLists(dataSource);
                    return Json(selectListItems, JsonRequestBehavior.AllowGet);
                }
                return Json(new List<BussinessObjects.SelectList>(), JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new List<BussinessObjects.SelectList>(), JsonRequestBehavior.AllowGet);
            }
        }
    }
}