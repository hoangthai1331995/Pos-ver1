using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configulation.Models
{
    public class DatabaseModel
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string UserLogin { get; set; }
        public string Password { get; set; }
    }
}
