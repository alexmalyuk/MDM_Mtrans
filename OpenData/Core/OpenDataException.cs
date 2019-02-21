using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenData.Core
{
    class OpenDataException : Exception
    {
        public OpenDataException()
        {

        }
        public OpenDataException(string message) 
            : base(message)
        {

        }
        public OpenDataException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
