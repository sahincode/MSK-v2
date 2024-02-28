using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.Exceptions
{
    public class NotChangedRoleException :Exception
    {
        public string PropertyName { get; set; }
        public NotChangedRoleException() { }
        public NotChangedRoleException(string message) : base(message) { }
        public NotChangedRoleException(string propertyName, string message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
