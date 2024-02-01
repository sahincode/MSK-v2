using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.Exceptions.OutOfRangesExceptions
{
    public class OutOfRangeImageSizeException :Exception
    {
        public string PropertyName { get; set; }
        public OutOfRangeImageSizeException() { }
        public OutOfRangeImageSizeException(string message) : base(message) { }
        public OutOfRangeImageSizeException(string propertyName, string message) : base(message)
        {
            PropertyName = propertyName;
        }

    }
}
