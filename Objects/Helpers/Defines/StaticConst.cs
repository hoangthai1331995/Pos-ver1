using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Defines
{
    public static class StaticConst
    {
        public static string AddUpdateDenyFields = "CreatedDate,UpdatedDate";
        public static string ListFilterDenyFields = "CreatedDate,UpdatedDate";

        public static string AddUpdateAdditionalFields = "FromDate,ToDate";

        public static string PartnerService = ConfigurationManager.AppSettings["PartnerService"];
        public static string PartnerServiceClient = ConfigurationManager.AppSettings["PartnerServiceClient"];
        public static string PartnerServiceSecretKey = ConfigurationManager.AppSettings["PartnerServiceSecretKey"];
    }
}
