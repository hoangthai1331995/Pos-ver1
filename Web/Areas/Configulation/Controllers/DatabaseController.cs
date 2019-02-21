using Configulation.Models;
using Helpers.BlankConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Configulation.Controllers
{
    public class DatabaseController : ConfigulationController
    {
        public DatabaseController()
        {
            NoDetectDatabase.Add("Connect");
        }
        // GET: Database
        public ActionResult Connect()
        {
            DatabaseModel model = new DatabaseModel
            {
                Server = "103.109.28.34",
                Database = "eTraining",
                UserLogin = "eTraining"
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Connect(DatabaseModel model)
        {
            if(TestConnection(model))
            {
                Session["SDatabase"] = model;
            }
            return Redirect("/Configulation/Panel");
        }

        private bool TestConnection(DatabaseModel serverInfo)
        {
            try
            {
                ETDataConnection gConn = new ETDataConnection(GetConnectionString(serverInfo));
                int test = int.Parse(gConn.ExecuteScalar("select 10").ToString());
                if (test == 10)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        
    }
}