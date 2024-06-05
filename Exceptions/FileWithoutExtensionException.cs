using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Exceptions
{
    internal class FileWithoutExtensionException : Exception
    {
        public FileWithoutExtensionException(string exception) :base(exception) 
        {

        }
    }
}
