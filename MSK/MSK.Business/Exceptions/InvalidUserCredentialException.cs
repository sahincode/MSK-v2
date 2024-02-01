﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.Exceptions
{
    public  class InvalidUserCredentialException :Exception
    {
        public string PropertyName { get; set; }
        public InvalidUserCredentialException() { }
        public InvalidUserCredentialException(string message) : base(message) { }
        public InvalidUserCredentialException(string propertyName, string message) : base(message)
        {
            PropertyName = propertyName;
        }

    }
}
