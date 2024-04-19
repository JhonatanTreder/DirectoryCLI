using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Exceptions
{
    internal class DirectoryException : Exception
    {
        public DirectoryException(string directoryException) :base(directoryException)
        {

        }
    }
}
