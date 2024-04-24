using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Commands
{
    internal class CreateFile : CommandsConfig
    {
        public string ArchiveName { get; }
        public CreateFile(string archiveName)
        {
            ArchiveName = archiveName;
        }
        public void Execute(FileInfo directoryPath)
        {
            string fullPath = Path.Combine(directoryPath.FullName, ArchiveName);
            File.Create(fullPath).Close();
        }
    }
}
