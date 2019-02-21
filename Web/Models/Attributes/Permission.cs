using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Models.Attributes
{
    public class Permission : ActionFilterAttribute
    {
        public string Key { get; set; }
    }
}