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

        public static bool IsValidDirectoryPath(FileInfo path)
        {
            if (Directory.Exists(path.FullName) == true)
            {
                return true;
            }

            else
            {
                throw new DirectoryNotFoundException();
            }
        }
    }
}
