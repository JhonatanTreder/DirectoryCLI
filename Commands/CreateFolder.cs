using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Commands
{
    internal class CreateFolder : CommandsConfig
    {
        public string DirectoryName { get; }

        public CreateFolder(string directoryName)
        {
            DirectoryName = directoryName;
        }

        public void Execute(FileInfo directoryPath)
        {
            if(PathValidation(directoryPath) == true)
            {
                string fullPath = Path.Combine(directoryPath.FullName, DirectoryName);
                Directory.CreateDirectory(fullPath);
            }

            else
            {
                throw new Exception();
            }
            
        }
    }
}
