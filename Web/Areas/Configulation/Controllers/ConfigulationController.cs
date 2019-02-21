using Configulation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Configulation.Controllers
{
    public class ConfigulationController : Controller
    {
        public List<string> NoDetectDatabase = new List<string>();
        public DatabaseModel SDatabase
        {
            get
            {
                if (Session != null)
                {
                    return (DatabaseModel)Session["SDatabase"];
                }
                return null;
            }
            set
            {
                if (value == null)
                    Session.Remove("SDatabase");
                else
                    Session["SDatabase"] = value;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string currentUrl = Request.Url.PathAndQuery;
            string actionName = filterContext.ActionDescriptor.ActionName;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            if (!filterContext.IsChildAction)
            {
                if (Session["SDatabase"] == null && !NoDetectDatabase.Contains(actionName))
                {
                    filterContext.Result = new RedirectResult("/Configulation/Database/Connect");
                }
            }
        }


        protected string GetConnectionString(DatabaseModel serverInfo)
        {
            string server = serverInfo.Server;
            string data = serverInfo.Database;
            string username = serverInfo.UserLogin;
            string password = serverInfo.Password;
            string connectionString = "";
            if (server == "")
                return "";
            if (data == "")
                return "";
            connectionString += "Data Source= " + server + ";Initial Catalog=" + data;
            if (username != "")
                connectionString += ";User Id=" + username;
            else
                connectionString += ";Integrated Security=True";

            if (password != "")
                connectionString += ";Password = " + password;

            return connectionString;
        }

        protected string ConvertDataType(string sqlDataType)
        {
            if (sqlDataType == "float")
                return "float";
            if (sqlDataType == "int")
                return "int";
            if (sqlDataType == "bigint")
                return "Int64";
            if (sqlDataType == "char")
                return "char";
            if (sqlDataType == "nvarchar"
                || sqlDataType == "varchar"
                || sqlDataType == "nchar"
                || sqlDataType == "text"
                || sqlDataType == "ntext")
                return "string";
            if (sqlDataType == "bit")
                return "bool";
            if (sqlDataType == "tinyint")
                return "Int16";
            if (sqlDataType == "datetime" || sqlDataType == "date" || sqlDataType == "smalldatetime")
                return "DateTime";
            if (sqlDataType == "decimal" || sqlDataType == "money")
                return "decimal";
            return "";
        }

        #region 'Private Function'
        protected string ParseNull(string value)
        {
            return (!String.IsNullOrEmpty(value) ? value : "");
        }
        #endregion
    }
}