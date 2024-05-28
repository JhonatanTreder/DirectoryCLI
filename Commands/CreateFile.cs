using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Commands
{
    internal class CreateFile : CommandsConfig
    {
        public CreateFile()
        {
            
        }
        public void Execute(FileInfo directoryPath, FileInfo archiveName)
        {
            string fullPath = Path.Combine(directoryPath.FullName, archiveName.Name);
            File.Create(fullPath).Close();
        }
    }
}
