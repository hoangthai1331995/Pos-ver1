using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Configulation.Models
{
    public class ColumnInfo
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public int MaxValue { get; set; }
        public int NumericPrecision { get; set; }
        public int NumericScale { get; set; }

        public string Default { get; set; }
    }
}
