using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.Services
{
    public class PartnerLoginResponse
    {
        public bool success { get; set; }
        public data data { get; set; }
    }

    public class data
    {
        public string accessToken { get; set; }
        public string tokenType { get; set; }
        public int expiresIn { get; set; }
        public string refreshToken { get; set; }
    }
}