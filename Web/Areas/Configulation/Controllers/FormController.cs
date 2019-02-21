using BussinessModels;
using BussinessObjects;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Configulation.Controllers
{
    public class FormController : ConfigulationController
    {
        // GET: Form
        public ActionResult AURender(string tableName = "", string formTitle = "", string targetArea = "")
        {
            FormInfo formInfo = new FormInfo(formTitle, "/" + targetArea + "/" + tableName.Replace("tbl", "") + "/AddUpdate");
            object[] parms = new object[] { "@tableName", tableName.Trim() };
            DataTable table = new SqlFieldAddUpdateAutoDao().GetDataTable(parms, "cofTableRenderAuto_GetAllColumn");
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (row["FieldType"] != null)
                    {
                        if (row["FieldType"].ToString() != "")
                        {
                            int fieldType = Convert.ToInt32(row["FieldType"]);
                            FormControl control = new FormControl(fieldType, row["COLUMN_NAME"].ToString(), row["COLUMN_NAME"].ToString(), "", row["DisplayName"].ToString(), row["PlaceHolder"].ToString(), row["Note"].ToString());
                            if (row["DataSource"] != null)
                            {
                                control.DataSource = row["DataSource"].ToString();
                            }
                            formInfo.Controls.Add(control);
                        }
                    }
                }
            }
            return View(formInfo);
        }

        public ActionResult FRender(string tableName = "", string formTitle = "", string targetArea = "", bool allowAU = false)
        {
            FormInfo formInfo = new FormInfo(formTitle, "/" + targetArea + "/" + tableName.Replace("tbl", ""));
            object[] parms = new object[] { "@tableName", tableName.Trim() };
            DataTable table = new SqlFieldFilterAutoDao().GetDataTable(parms, "cofTableRenderAuto_GetAllColumnForFilter");
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (row["FieldType"] != null)
                    {
                        if (row["FieldType"].ToString() != "")
                        {
                            int fieldType = Convert.ToInt32(row["FieldType"]);
                            FormControl control = new FormControl(fieldType, row["COLUMN_NAME"].ToString(), row["COLUMN_NAME"].ToString(), Request[row["COLUMN_NAME"].ToString()] != null ? Request[row["COLUMN_NAME"].ToString()].ToString() : "", row["DisplayName"].ToString(), row["PlaceHolder"].ToString(), "");
                            if (row["DataSource"] != null)
                            {
                                control.DataSource = row["DataSource"].ToString();
                            }
                            formInfo.Controls.Add(control);
                        }
                    }
                }
            }
            ViewBag.AllowAU = allowAU;
            return View(formInfo);
        }

        public ActionResult LRender(string tableName = "", string titleForm = "", string targetArea = "", bool allowAU = false)
        {
            #region 'Init'
            List<object> dataParams = new List<object>();
            ListInfo model = new ListInfo();
            model.ActiveController = tableName.Replace("tbl", "");
            model.ActiveArea = targetArea;
            model.Title = titleForm;
            int currentPage = Request["currentPage"] != null ? Convert.ToInt32(Request["currentPage"].ToString()) : 1;
            #endregion
            #region 'Load Field & Data Source'
            object[] parms = new object[] { "@tableName", tableName.Trim() };
           
            TableRenderAuto dataSource = new SqlTableRenderAutoDao().GetTableRenderAuto(tableName);
            #endregion

            dataParams.Add("@pageSize");
            dataParams.Add(dataSource != null ? dataSource.PageSize : 10);
            dataParams.Add("@pageNum");
            dataParams.Add(currentPage);
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
            if (dataSource != null && dataSource.ExtentionProperty["DataSource"] != null)
            {
                if(dataSource.ExtentionProperty["DataSource"].ToString() != "")
                {
                    model.DataInfo = new SqlFieldListAutoDao().GetDataTable(dataParams.ToArray(), dataSource.ExtentionProperty["DataSource"].ToString());
                    if (model.DataInfo != null)
                    {
                        model.DataInfo.Columns.Add("StatusDisplay", typeof(System.String));
                        if (model.DataInfo.Rows.Count > 0)
                        {
                            foreach (DataRow r in model.DataInfo.Rows)
                            {
                                for (int c = 0; c < model.DataInfo.Columns.Count; c++)
                                {
                                    if (model.DataInfo.Columns[c].ToString() == "Status" || model.DataInfo.Columns[c].ToString() == "status" || model.DataInfo.Columns[c].ToString() == "STATUS")
                                    {
                                        r["StatusDisplay"] = StatusMapping(Convert.ToInt32(r[c].ToString()), tableName);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            #region 'Paging'
            model.PagingInfo.CurrentPage = currentPage;
            if (model.DataInfo != null && model.DataInfo.Rows.Count > 0)
            {
                model.PagingInfo.TotalRecords = Convert.ToInt32(model.DataInfo.Rows[0]["TotalRec"]);
            }
            #endregion

            #region 'Load Resource'
            model.Resources = new SqlResourceListAutoDao().GetFilter(tableName);
            #endregion

            ViewBag.AllowAU = allowAU;
            return View(model);
        }

        #region 'Status Mapping'
        public string StatusMapping(int value, string tableName)
        {
            List<DisplayStatus> statusDisplay = (List<DisplayStatus>)new SqlDisplayStatusDao().GetListByCustomDataSource(new object[] { "@tableName", tableName }, "cofDisplayStatus_GetListByTable");
            if(statusDisplay != null && statusDisplay.Count() > 0)
            {
                foreach(DisplayStatus s in statusDisplay)
                {
                    if (s.StatusValue == value)
                        return s.StatusDisplay;
                }
            }
            else // Default Status Display
            {
                if (value == 1)
                    return "Kích hoạt";
                else if (value == 2)
                    return "Không kích hoạt";
                else
                    return value.ToString();
            }
            return value.ToString();
        }
        #endregion
    }
}