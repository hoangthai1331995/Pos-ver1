using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessModels
{
    public class FormControl
    {
        public FormControl(int _type, string _key, string _name, string _value, string _display, string _placeHolder, string _note)
        {
            Type = _type;
            Key = _key;
            Name = _name;
            Value = _value;
            Display = _display;
            PlaceHolder = _placeHolder;
            Note = _note;
            DataSource = "";
            IsRequire = false;
            IsNumber = false;
            IsEmail = false;
        }


        public int Type { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Display { get; set; }
        public string DataSource { get; set; }
        public string PlaceHolder { get; set; }
        public string Note { get; set; }
        public bool IsRequire { get; set; }
        public bool IsNumber { get; set; }
        public bool IsEmail { get; set; }
    }
}
