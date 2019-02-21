using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.Services
{
    public class PartnerLoginRequest
    {
        public string client { get; set; }
        public string secretkey { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}