using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.Exceptions
{
    public class NullEntityException : Exception
    {
        public string PropertyName { get; set; }
        public NullEntityException() { }
        public NullEntityException(string message) : base(message) { }
        public NullEntityException(string propertyName, string message) : base(message)
        {
            PropertyName = propertyName;
        }

    }
}
