using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mtrans_MDM.Core
{
    public class InvalidChecksumException : Exception
    {
        public InvalidChecksumException()
        {

        }
        public InvalidChecksumException(string message) 
            : base(message)
        {

        }
        public InvalidChecksumException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}