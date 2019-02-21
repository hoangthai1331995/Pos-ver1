using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extension
{
    public class ServiceHelper
    {
        public enum GlobalStatus
        {
            Success,
            UnSuccess,
            Failed,
            Invalid,
            InvalidData,
            Maximum_Limited, // bị giới hạn
            Access_Denied,
            NotFound,
            AlreadyExists,
            Account_Username_NotFound,

            Account_NotFound,
            Account_Unauthorized,
            Account_IsLocked,
            Account_NotActiveYet,
            Account_IsDeleted,

            Account_Login_FailedManyTime,
            Account_LoginByAuthCode_EmailAlreadyInUse,
            Account_LoginByAuthCode_PhoneAlreadyInUse,

            Account_EmailAlreadyInUse,
            Account_PhoneAlreadyInUse,
            Account_PersonalIdAlreadyInUse,

            Account_ChangePassword_WrongUsernameOrPassword,
            Account_WrongPassword,
            Account_HaveNoPermission
        }

        public enum ReportScheduleStatus : short
        {
            Pending = 1,
            Finish = 2,
            Fail = 3,
            Cancel = 4,
            Processing = 5,
            SystemError = 6
        }

        public class EwHeaders
        {
            public static string X_Paging_Total_Count = "X-Pagination-Total-Count";
            public static string X_Paging_Total_Pages = "X-Pagination-Total-Pages";
            public static string X_Paging_Current_Page = "X-Pagination-Current-Page";
            public static string X_Paging_Limit = "X-Pagination-Limit";
            public static string X_Paging_Next = "X-Pagination-Next";
            public static string X_Paging_Prev = "X-Pagination-Prev";
            public static string X_Paging_First = "X-Pagination-First";
            public static string X_Paging_Last = "X-Pagination-Last";
            public static string X_Status = "X-Status";
            public static string X_Errors = "X-Errors";
            public static string X_Message = "X-Message";
            public static string X_Error_Message = "X-Error-Messages";

        }
    }
    //public static class ResponseHelpers
    //{
    //    public static GetDataResponse SetResponseResult(int _status, string _message, object _content)
    //    {
    //        GetDataResponse response = new GetDataResponse();
    //        response.Status = _status;
    //        response.Message = _message;
    //        response.Content = _content;
    //        return response;
    //    }
    //}

    public enum ResponseStatus
    {
        Error = -1,
        Success = 1,
        InvalidRequestParam = 2,
        RequestRejected = 3,
        IpRequestIsBanned = 4,
        InvalidMethod = 5,
        UnknowError = 100
    }

    public enum RequestType
    {
        Get = 1,
        Post = 2
    }

    public static class ResponseMessage
    {
        public static string Error = "Thất bại";
        public static string Success = "Thành công";
        public static string InvalidRequestParam = "Tham số truy vấn dữ liệu không khớp";
        public static string RequestRejected = "Truy cập bị từ chối";
        public static string IpRequestIsBanned = "Ip của bạn không được phép truy cập ứng dụng";
        public static string InvalidMethod = "Phương thức truy cập không chính xác";
        public static string UnknowError = "Lỗi không xác định";
    }
}