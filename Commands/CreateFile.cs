using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Commands
{
    internal class CreateFile : Command
    {
        public FileInfo DirectoryInfo { get; set; }
        public CreateFile(FileInfo directoryInfo)
        {
            DirectoryInfo = directoryInfo;
        }
        public override void Execute(FileInfo directoryPath)
        {
            directoryPath.Create();
        }
    }
}
