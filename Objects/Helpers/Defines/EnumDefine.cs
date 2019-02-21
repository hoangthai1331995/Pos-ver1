using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Defines
{
    public enum FieldType : int
    {
        Textbox = 1,
        Label = 2,
        Commbobox = 3,
        DateTime = 4,
        Radio = 5,
        Checkbox = 6
    }

    public enum FormatType : int
    {
        Text = 1,
        Money = 2,
        DateTime = 3,
        Single_Date = 4,
        Label_Success = 5,
        Label_Info = 6,
        Label_Warning = 7,
        Label_Danger = 8,
        Label_Default = 9
    }

    public enum ListPageSize : int
    {
        P10 = 10,
        P20 = 20,
        P50 = 50,
        P100 = 100,
        P200 = 200,
        P500 = 500,
        P1000 = 1000
    }
}
