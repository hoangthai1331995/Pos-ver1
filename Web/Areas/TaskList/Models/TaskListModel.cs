using BussinessObjects;
using Helpers.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.Models
{
    public class TasklistModel
    {
        public List<SelectList> TaskTypeList { get; set; }
        public List<SelectList> ObjectInChargeTypeList { get; set; }
        public List<SelectList> OptionListDB { get; set; }
        public List<OptionListModel> OptionListView { get; set; }
        public List<SelectList> PriorityList { get; set; }
        public int TaskID { get; set; }
        public int ParentID { get; set; }
        public string TaskTitle { get; set; }
        public string ItemsDesc { get; set; }
        public string TaskType { get; set; }
        public string StatusType { get; set; }
        public string StatusName { get; set; }
        public string FileString { get; set; }
        public string PriorityLevel { get; set; }
        public string PriorityName { get; set; }
        public string CheckList { get; set; }
        public string OptionString { get; set; }
        public DateTime FromDateTime { get; set; }
        public DateTime ToDateTime { get; set; }
        public DateTime CompletedDatetime { get; set; }
        public DateTime FromDateRepeat { get; set; }
        public DateTime ToDateRepeat { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string FromDateTimeStr { get { return FromDateTime.ToString("MM/dd/yyyy HH:ss"); } }
        public string ToDateTimeStr { get { return ToDateTime.ToString("MM-dd-yyyy HH:ss"); } }
        public string CompletedDatetimeStr { get { return CompletedDatetime.ToString("MM-dd-yyyy HH:ss"); } }
        public string FromDateRepeatStr { get { return FromDateRepeat.ToString("MM-dd-yyyy HH:ss"); } }
        public string ToDateRepeatStr { get { return ToDateRepeat.ToString("MM-dd-yyyy HH:ss"); } }
        public string DateCreatedStr { get { return DateCreated.ToString("MM-dd-yyyy HH:ss"); } }
        public string DateUpdatedStr { get { return DateUpdated.ToString("MM-dd-yyyy HH:ss"); } }
        public bool IsRepeated { get; set; }
        public bool IsActive { get; set; }
        public bool IsFollow { get; set; }
        public string RepeatType { get; set; }
        public string InTimeString { get; set; }
        public string InWeekdayString { get; set; }
        public string InMonthdayString { get; set; }
        public string ObjectInChargeType { get; set; }
        public string ObjectInCharge { get; set; }
        public string PersonsControl { get; set; }
        public string UserCreated { get; set; }
        public string UserUpdated { get; set; }
        public List<FileStringItem> FileStringItemList { get { return Mapper.MapJsonStringToListObject<FileStringItem>(FileString); } }
        public List<CheckListItem> CheckListItemList { get { return Mapper.MapJsonStringToListObject<CheckListItem>(CheckList); } }
        public List<OptionStringItem> OptionStringItemList { get { return Mapper.MapJsonStringToListObject<OptionStringItem>(OptionString); } }
        public string DownTime { get { return StaticFunc.CowndownTime(ToDateTime); } }
        public string FromDateTimeCal { get { return FromDateTime.AddDays(1).ToString("yyyy-MM-dd"); } }
        public string ToDateTimeCal { get { return ToDateTime.AddDays(1).ToString("yyyy-MM-dd"); } }
        public string ObjectInChargeName { get; set; }
        public string UserCreatedName { get; set; }
        public string PersonsControlName { get; set; }
        public string ParentName { get; set; }
        public string ObjectInChargeTypeName { get; set; }
        public string TaskTypeName { get; set; }
    }

    public class ObjectStringModel
    {
        public int TaskID { get; set; }
        public int ParentID { get; set; }
        public string TaskTitle { get; set; }
        public string ItemsDesc { get; set; }
        public string TaskType { get; set; }
        public string StatusType { get; set; }
        public List<FileStringItem> FileString { get; set; }
        public string PriorityLevel { get; set; }
        public List<CheckListItem> CheckList { get; set; }
        public List<OptionStringItem> OptionString { get; set; }
        public string FromDateTime { get; set; }
        public string ToDateTime { get; set; }
        public string CompletedDatetime { get; set; }
        public string FromDateRepeat { get; set; }
        public string ToDateRepeat { get; set; }
        public bool IsRepeated { get; set; }
        public string RepeatType { get; set; }
        public string InTimeString { get; set; }
        public string InWeekdayString { get; set; }
        public string InMonthdayString { get; set; }
        public string ObjectInChargeType { get; set; }
        public string ObjectInCharge { get; set; }
        public string IsActive { get; set; }
        public string IsFollow { get; set; }
        public string PersonsControl { get; set; }
    }

    public class FileStringItem
    {
        public int FileID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }

    public class CheckListItem
    {
        public int CheckListID { get; set; }
        public string CheckListTitle { get; set; }
        public bool IsCheck { get; set; }
    }

    public class OptionStringItem
    {
        public int KeyID { get; set; }
        public string KeyName { get; set; }
        public bool KeyVal { get; set; }
    }

    public class OptionListModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Val { get; set; }
    }

    public class ListTasklistStatusModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
    public class ListOptionModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
    public class FileItem
    {
        public string FileID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}
