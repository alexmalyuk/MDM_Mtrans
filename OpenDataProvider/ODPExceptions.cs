using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDataProvider
{
    class OpenDataProviderException : Exception
    {
        public OpenDataProviderException()
        {

        }
        public OpenDataProviderException(string message) 
            : base(message)
        {

        }
        public OpenDataProviderException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
