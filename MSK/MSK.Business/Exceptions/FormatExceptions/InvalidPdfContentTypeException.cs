using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.Exceptions.FormatExceptions
{
    public class InvalidPdfContentTypeException :Exception
    {
        public string PropertyName { get; set; }
        public InvalidPdfContentTypeException() { }
        public InvalidPdfContentTypeException(string message) : base(message) { }
        public InvalidPdfContentTypeException(string propertyName, string message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
