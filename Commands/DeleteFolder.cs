using DirectoryCLI.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Commands
{
    internal class DeleteFolder : Command
    {
        public string DirectoryName { get; }
        public DeleteFolder(string directoryName)
        {
            DirectoryName = directoryName;
        }
        public override void Execute(FileInfo directoryPath)
        {
            if (IsValidDirectoryPath(directoryPath) == true)
            {
                string directory = Path.Combine(directoryPath.FullName, DirectoryName);
                Directory.Delete(directory);
            }

            else
            {
                throw new DirectoryException("Path not found");
            }

        }
    }
}
