using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.Actions
{
    public class LoginModel
    {
        public string LoginName { get; set; }
        public string PassWord { get; set; }
        public bool IsRemember { get; set; }
        public string ReturnUrl { get; set; }
    }
}