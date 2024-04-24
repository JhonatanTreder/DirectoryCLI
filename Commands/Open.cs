using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Commands
{
    internal class Open : CommandsConfig
    {
        public Open(string directoryPath) 
        {
            DirectoryPath = directoryPath;
        }

        public void Execute(FileInfo directoryPath)
        {
            string fullPath = Path.Combine(directoryPath.FullName, DirectoryPath);
            System.Diagnostics.Process.Start(fullPath);
        }
    }
}
