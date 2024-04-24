using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Commands
{
    abstract class CommandsConfig
    {
        public string DirectoryPath { get; set; }
        public abstract void Execute(FileInfo directoryPath);
        public static bool IsValidDirectoryPath(FileInfo path)
        {
            try
            {
                string fullPath = Path.GetFullPath(path.DirectoryName);
                return true;
            }

            catch (Exception)
            {
                return false;
            }

        }
    }
}
