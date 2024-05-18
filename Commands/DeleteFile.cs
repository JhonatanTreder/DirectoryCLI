using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Commands
{
    internal class DeleteFile : CommandsConfig
    {
        public string ArchiveName { get; }

        public DeleteFile(string archiveName)
        {
            ArchiveName = archiveName;
        }
        public void Execute(FileInfo directoryPath)
        {
            string fullPath = Path.Combine(directoryPath.FullName, ArchiveName);
            File.Delete(fullPath);
        }
    }
}
