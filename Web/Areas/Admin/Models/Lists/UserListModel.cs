using BussinessObjects;
using Helpers.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.Lists
{
    public class UserListModel
    {
        public UserListModel()
        {
            LoginName = "";
            Status = 1;
            Users = new List<User>();
            PagingInfo = new Paging();
            PagingInfo.PageSize = 100;
        }
        public string LoginName { get; set; }
        public int Status { get; set; }
        public List<User> Users { get; set; }
        public Paging PagingInfo { get; set; }
    }
}