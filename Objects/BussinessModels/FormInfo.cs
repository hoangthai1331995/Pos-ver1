using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessModels
{
    public class FormInfo
    {
        public FormInfo() { }
        public FormInfo(string _title, string _action)
        {
            Title = _title;
            Action = _action;
            Style = "";
            Width = 0;
            Height = 0;
            Controls = new List<FormControl>();
        }
        public string Title { get; set; }
        public string Action { get; set; }
        public string Style { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public List<FormControl> Controls { get; set; }
    }
}
