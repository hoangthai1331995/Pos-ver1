using BussinessObjects;
using DataObjects;
using Helpers.BlankConnection;
using Helpers.Defines;
using Helpers.Functions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Configulation.Controllers
{
    public class AutoController : ConfigulationController
    {
        // GET: Auto
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddAutoTable()
        {
            DataTable table = new SqlTableRenderAutoDao().GetDataTable(new object[0], "cofTableRenderAuto_GetAllTableNotAdd");
            return View(table);
        }

        public ActionResult GetAllColumnInTable(string tableName = "")
        {
            object[] parms = new object[] { "@tableName", tableName.Trim() };
            DataTable table = new SqlFieldAddUpdateAutoDao().GetDataTable(parms, "cofTableRenderAuto_GetAllColumn");
            ViewBag.DenyFields = Mapper.SplitString(StaticConst.AddUpdateDenyFields);
            ViewBag.TableName = tableName;
            return View(table);
        }

        public ActionResult GetAllTableColumnForFilter(string tableName = "")
        {
            object[] parms = new object[] { "@tableName", tableName.Trim() };
            DataTable table = new SqlFieldFilterAutoDao().GetDataTable(parms, "cofTableRenderAuto_GetAllColumnForFilter");
            ViewBag.DenyFields = Mapper.SplitString(StaticConst.ListFilterDenyFields);
            ViewBag.TableName = tableName;
            return View(table);
        }

        public ActionResult GetAllTableColumnForList(string tableName = "")
        {
            object[] parms = new object[] { "@tableName", tableName.Trim() };
            DataTable table = new SqlFieldListAutoDao().GetDataTable(parms, "cofTableRenderAuto_GetAllColumnForList");
            ViewBag.DataSource = new SqlTableRenderAutoDao().GetTableRenderAuto(tableName);
            ViewBag.StoreProcedures = new SqlSelectListDao().GetSelectLists("[dbo].[spSelectList_GetFilterStoreProcedure]");
            ViewBag.TableName = tableName;
            return View(table);
        }

        [HttpPost]
        public JsonResult CreateFieldAddUpdate(string jColData = "")
        {
            try
            {
                int count = 0;
                List<FieldAddUpdateAuto> fields = JsonConvert.DeserializeObject<List<FieldAddUpdateAuto>>(jColData);
                if (fields != null && fields.Count() > 0)
                {
                    string tableName = fields.FirstOrDefault().TableRender;
                    new SqlFieldAddUpdateAutoDao().Clear(tableName);
                    foreach (FieldAddUpdateAuto field in fields)
                    {
                        field.Status = 1;
                        field.CreatedDate = DateTime.Now;
                        field.UpdatedDate = DateTime.Now;
                        if (new SqlFieldAddUpdateAutoDao().Insert(field))
                        {
                            count++;
                        }
                    }
                }
                return Json(count, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult CreateFieldFilter(string jColData = "")
        {
            try
            {
                int count = 0;
                List<FieldFilterAuto> fields = JsonConvert.DeserializeObject<List<FieldFilterAuto>>(jColData);
                if (fields != null && fields.Count() > 0)
                {
                    string tableName = fields.FirstOrDefault().TableRender;
                    new SqlFieldFilterAutoDao().Clear(tableName.Trim());
                    foreach (FieldFilterAuto field in fields)
                    {
                        field.TableRender = field.TableRender.Trim();
                        field.Status = 1;
                        field.CreatedDate = DateTime.Now;
                        field.UpdatedDate = DateTime.Now;
                        if (new SqlFieldFilterAutoDao().Insert(field))
                        {
                            count++;
                        }
                    }
                }
                return Json(count, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult CreateFieldList(string jColData = "",string tableName = "", int pageSize = 0, string storeProcedure = "")
        {
            try
            {
                int count = 0;
                #region 'Insert Header'
                new SqlTableRenderAutoDao().Clear(tableName);
                TableRenderAuto tableRender = new TableRenderAuto()
                {
                    TableName = tableName,
                    Status = 1,
                    PageSize = pageSize,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                };
                new SqlTableRenderAutoDao().Insert(tableRender);
                #endregion
                if(tableRender.ID > 0)
                {
                    if(String.IsNullOrEmpty(storeProcedure))
                    {
                        List<FieldListAuto> fields = JsonConvert.DeserializeObject<List<FieldListAuto>>(jColData);
                        if (fields != null && fields.Count() > 0)
                        {
                            new SqlFieldListAutoDao().Clear(tableName.Trim());
                            foreach (FieldListAuto field in fields)
                            {
                                field.TableRender = field.TableRender.Trim();
                                field.Status = 1;
                                field.CreatedDate = DateTime.Now;
                                field.UpdatedDate = DateTime.Now;
                                if (new SqlFieldListAutoDao().Insert(field))
                                {
                                    count++;
                                }
                            }
                        }
                    }
                    else
                    {
                        new SqlDataSourceListAutoDao().Clear(tableName);
                        DataSourceListAuto dataSource = new DataSourceListAuto()
                        {
                            TableRender = tableName,
                            DataSource = storeProcedure,
                            Status = 1,
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        };
                        if (new SqlDataSourceListAutoDao().Insert(dataSource))
                            count++;
                    }
                }
                return Json(count, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
    }
}