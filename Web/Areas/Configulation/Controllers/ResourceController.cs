using BussinessObjects;
using DataObjects;
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
    public class ResourceController : ConfigulationController
    {
        public ResourceController()
        {
            NoDetectDatabase.Add("LoadField");
        }
        // GET: Resource
        public ActionResult Index(string par = "")
        {
            string tableName = "";
            string dataSource = "";
            if(!String.IsNullOrEmpty(par))
            {
                string parDecrypt = StaticFunc.Base64Decode(par);
                if(parDecrypt != "")
                {
                    string[] parArr = parDecrypt.Split('#');
                    if(parArr != null && parArr.Count() == 2)
                    {
                        tableName = parArr[0].ToString();
                        dataSource = parArr[1].ToString();
                    }
                }
            }
            ViewBag.Tables = new SqlSelectListDao().GetSelectLists("spSelectList_GetTableConfiged");
            ViewBag.TableName = tableName;
            ViewBag.DataSource = dataSource;
            return View();
        }

        public ActionResult LoadField(string dataSource = "", string tableName = "")
        {
            List<object> dataParams = new List<object>();
            dataParams.Add("@pageSize");
            dataParams.Add("1");
            dataParams.Add("@pageNum");
            dataParams.Add("1");
            object[] parms = new object[] { "@tableName", tableName.Trim() };
            DataTable fTable = new SqlFieldFilterAutoDao().GetDataTable(parms, "cofTableRenderAuto_GetAllColumnForFilter");
            if (fTable != null && fTable.Rows.Count > 0)
            {
                foreach (DataRow r in fTable.Rows)
                {
                    if (r["FieldType"] != null)
                    {
                        if (r["FieldType"].ToString() != "")
                        {
                            dataParams.Add(r["COLUMN_NAME"].ToString());
                            dataParams.Add(Request[r["COLUMN_NAME"].ToString()] != null ? Request[r["COLUMN_NAME"].ToString()] : "");
                        }
                    }
                }
            }
            #region 'Load List Data'
            DataTable result = new SqlFieldListAutoDao().GetDataTable(dataParams.ToArray(), dataSource);
            #endregion
            ViewBag.Resources = new SqlResourceListAutoDao().GetFilter(tableName);
            ViewBag.TableRender = tableName;
            ViewBag.DataSource = dataSource;
            return View(result);
        }

        [HttpPost]
        public JsonResult AddUpdate(string jData = "", string dataSource = "")
        {
            int count = 0;
            string tableName = "";
            if(!String.IsNullOrEmpty(jData))
            {
                List<ResourceListAuto> resources = JsonConvert.DeserializeObject<List<ResourceListAuto>>(jData);
                if(resources != null && resources.Count() > 0)
                {
                    tableName = resources.FirstOrDefault().TableRender;
                    new SqlResourceListAutoDao().Clear(tableName);
                    foreach(ResourceListAuto resource in resources)
                    {
                        resource.Status = 1;
                        resource.CreatedDate = DateTime.Now;
                        resource.UpdatedDate = DateTime.Now;
                        if (new SqlResourceListAutoDao().Insert(resource))
                            count++;
                    }
                }
            }
            if(count > 0)
            {
                string redirectUrl = "/Configulation/Resource/?par=" + StaticFunc.Base64Encode(tableName + "#" + dataSource);
                return Json(redirectUrl, JsonRequestBehavior.AllowGet);
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}