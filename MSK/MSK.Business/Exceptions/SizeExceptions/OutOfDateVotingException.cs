using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.Exceptions.SizeExceptions
{
    public class OutOfDateVotingException :Exception
    {
        public string PropertyName { get; set; }
        public OutOfDateVotingException() { }
        public OutOfDateVotingException(string message) : base(message) { }
        public OutOfDateVotingException(string propertyName, string message) : base(message)
        {
            PropertyName = propertyName;
        }

    }
}
