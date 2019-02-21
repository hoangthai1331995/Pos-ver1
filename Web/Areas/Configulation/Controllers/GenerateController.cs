using Configulation.Models;
using Helpers.BlankConnection;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Configulation.Controllers
{
    public class GenerateController : ConfigulationController
    {
        // GET: Generate
        public ActionResult Index()
        {
            ETDataConnection gConn = new ETDataConnection(GetConnectionString(SDatabase));
            DataTable tables = gConn.GetDataTable("SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME NOT LIKE N'%cof%'");
            return View(tables);
        }

        [HttpPost]
        public JsonResult Index(string jsonTable = "")
        {
            try
            {
                if (!String.IsNullOrEmpty(jsonTable))
                {
                    List<GenerateTableModel> generateTables = JsonConvert.DeserializeObject<List<GenerateTableModel>>(jsonTable);
                    if (generateTables != null && generateTables.Count() > 0)
                    {
                        foreach (GenerateTableModel table in generateTables)
                        {
                            ETDataConnection gConn = new ETDataConnection(GetConnectionString(SDatabase));
                            TableInfo tableInfo = new TableInfo(table.TableName, gConn);
                            ClassInfo classInfo = new ClassInfo(tableInfo);
                            string fileTemplatePath = Server.MapPath("/DataTemp");
                            string fileResultPath = Server.MapPath("/DataGen");
                            classInfo.GenerateSourceCode(fileTemplatePath);
                            classInfo.Save(fileResultPath);
                            tableInfo.CreateStoreProcedure();
                        }
                    }
                }
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        #region 'Create Object'
        public ActionResult CreateObject()
        {
            ETDataConnection gConn = new ETDataConnection(GetConnectionString(SDatabase));
            DataTable tables = gConn.GetDataTable("SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME NOT LIKE N'%cof%'");
            return View(tables);
        }

        [HttpPost]
        public JsonResult CreateObject(string jsonTable = "")
        {
            try
            {
                if (!String.IsNullOrEmpty(jsonTable))
                {
                    List<GenerateTableModel> generateTables = JsonConvert.DeserializeObject<List<GenerateTableModel>>(jsonTable);
                    if (generateTables != null && generateTables.Count() > 0)
                    {
                        foreach (GenerateTableModel table in generateTables)
                        {
                            ETDataConnection gConn = new ETDataConnection(GetConnectionString(SDatabase));
                            TableInfo tableInfo = new TableInfo(table.TableName, gConn);
                            ClassInfo classInfo = new ClassInfo(tableInfo);
                            string fileTemplatePath = Server.MapPath("/DataTemp");
                            string fileResultPath = Server.MapPath("/DataGen");
                            classInfo.GenerateSourceCode(fileTemplatePath);
                            classInfo.Save(fileResultPath);
                        }
                    }
                }
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region 'Create Store'
        public ActionResult CreateStore()
        {
            ETDataConnection gConn = new ETDataConnection(GetConnectionString(SDatabase));
            DataTable tables = gConn.GetDataTable("SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME NOT LIKE N'%cof%'");
            return View(tables);
        }

        [HttpPost]
        public JsonResult CreateStore(string jsonTable = "")
        {
            try
            {
                if (!String.IsNullOrEmpty(jsonTable))
                {
                    List<GenerateTableModel> generateTables = JsonConvert.DeserializeObject<List<GenerateTableModel>>(jsonTable);
                    if (generateTables != null && generateTables.Count() > 0)
                    {
                        foreach (GenerateTableModel table in generateTables)
                        {
                            ETDataConnection gConn = new ETDataConnection(GetConnectionString(SDatabase));
                            TableInfo tableInfo = new TableInfo(table.TableName, gConn);
                            tableInfo.CreateStoreProcedure();
                        }
                    }
                }
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 'Create Controller'
        public ActionResult CreateController()
        {
            ETDataConnection gConn = new ETDataConnection(GetConnectionString(SDatabase));
            DataTable tables = gConn.GetDataTable("SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME NOT LIKE N'%cof%'");
            return View(tables);
        }

        [HttpPost]
        public JsonResult CreateController(string jsonTable = "", string module = "")
        {
            try
            {
                if (!String.IsNullOrEmpty(jsonTable))
                {
                    List<GenerateTableModel> generateTables = JsonConvert.DeserializeObject<List<GenerateTableModel>>(jsonTable);
                    if (generateTables != null && generateTables.Count() > 0)
                    {
                        foreach (GenerateTableModel table in generateTables)
                        {
                            ETDataConnection gConn = new ETDataConnection(GetConnectionString(SDatabase));
                            TableInfo tableInfo = new TableInfo(table.TableName, gConn);
                            ClassInfo classInfo = new ClassInfo(tableInfo, module);
                            string fileTemplatePath = Server.MapPath("/DataTemp");
                            string fileResultPath = Server.MapPath("/DataGen");
                            classInfo.GenerateController(fileTemplatePath);
                            classInfo.SaveController(fileResultPath);
                        }
                    }
                }
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

    }
}