using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.Exceptions.SizeExceptions
{
    public class OutOfRangePdfSizeException:Exception
    {
        public string PropertyName { get; set; }
        public OutOfRangePdfSizeException() { }
        public OutOfRangePdfSizeException(string message) : base(message) { }
        public OutOfRangePdfSizeException(string propertyName, string message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
