using DirectoryCLI.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Commands
{
    internal class DeleteFolder : CommandsConfig
    {
        public string DirectoryName { get; }
        public DeleteFolder(string directoryName)
        {
            DirectoryName = directoryName;
        }
        public void Execute(FileInfo directoryPath)
        {
            if (IsValidDirectoryPath(directoryPath) == true)
            {
                string fullPath = Path.Combine(directoryPath.FullName, DirectoryName);
                Directory.Delete(fullPath, true);
            }

            else
            {
                throw new DirectoryNotFoundException();
            }

        }
    }
}
