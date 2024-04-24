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
        public override void Execute(FileInfo directoryPath)
        {
            string directory = Path.Combine(directoryPath.FullName, DirectoryName);
            Directory.CreateDirectory(directory);
        }
    }
}
