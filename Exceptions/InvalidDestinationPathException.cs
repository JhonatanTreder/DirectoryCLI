using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Exceptions
{
    internal class InvalidDestinationPathException : Exception
    { 
        public InvalidDestinationPathException(string exception) : base(exception)
        {

        }
    }
}
