using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObjects
{
    public class NI10_Employee : BusinessObject
    {
        public string EmployeeCode { get; set; }
        public string CurrentPassword { get; set; }
        public string FullName { get; set; }
        public string JobTitle { get; set; }
        public long GroupId { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Status { get; set; }
        public bool IsLocked { get; set; }
        public string Avatar { get; set; }
        public DateTime LastLogin { get; set; }
        public string Address { get; set; }
        public bool Gender { get; set; }
        public DateTime Birthday { get; set; }
        public bool Marry { get; set; }
        public bool FPTCare { get; set; }
        public string IDCard { get; set; }
        public string IDCardPlace { get; set; }
        public DateTime IDCardDate { get; set; }
        public string IDCardAddress { get; set; }
        public string IPPhone { get; set; }
        public string NoteContact { get; set; }
        public string OrganizationHierachyName { get; set; }
        public long CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
