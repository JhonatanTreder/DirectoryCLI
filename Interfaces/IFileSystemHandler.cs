using DirectoryCLI.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Interfaces
{
    internal interface IFileSystemHandler
    {
        void Create(string[] arguments);
        void Delete(string[] arguments);
        void DeleteRecursive(string[] arguments);
        void DeleteInPosition(string[] arguments);
    }
}
