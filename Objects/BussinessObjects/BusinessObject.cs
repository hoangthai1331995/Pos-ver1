using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Reflection;
using System.ComponentModel;
namespace BussinessObjects
{
    public abstract class BusinessObject
    {
        protected long id = -1;

        public virtual long ID
        {
            get { return id; }
            set
            {
                id = value;
            }
        }

        public string EcryptedID { get; set; }
        private IDictionary<string, object> dynamicProperties = new Dictionary<string, object>();

        public IDictionary<string, object> ExtentionProperty
        {
            get
            {
                return dynamicProperties;
            }
            set
            {
                dynamicProperties = value;
            }
        }

        public void AssignProperties(object source) //Vu.L Lưu ý vẫn cần phải set lại CreatedDate và UpdateDate mới đúng
        {
            foreach (var i in this.GetType().GetProperties())
            {
                PropertyInfo pInfo = source.GetType().GetProperty(i.Name);
                if (pInfo != null)
                {
                    i.SetValue(this, pInfo.GetValue(source, null), null);
                }
            }
        }
    }
}
