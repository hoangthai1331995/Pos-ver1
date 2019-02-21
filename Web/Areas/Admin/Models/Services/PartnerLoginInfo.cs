using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.Services
{
    public class PartnerLoginInfo
    {
        public bool success { get; set; }
        public dataInfo data { get; set; }
    }

    public class dataInfo
    {
        public long id { get; set; }
        public long userProfileId { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public long orgId { get; set; }
        public string orgCode { get; set; }
        public string orgName { get; set; }
        public long orgTitleId { get; set; }
        public long companyId { get; set; }
        public long addressId { get; set; }
        //public long profileProtoId { get; set; }
        public string employeeCode { get; set; }
        public long directManagerId { get; set; }
        public string directManagerCode { get; set; }
        public int employeeTypeId { get; set; }
        public bool isOrgManager { get; set; }
        public bool isActive { get; set; }
        public bool isManagementVHX { get; set; }
        public string positionDetail { get; set; }
    }
}