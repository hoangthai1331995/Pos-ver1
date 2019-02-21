using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class LoginModel
    {
        public string LoginName { get; set; }
        public string PassWord { get; set; }
        public string ReturnUrl { get; set; }
    }
}