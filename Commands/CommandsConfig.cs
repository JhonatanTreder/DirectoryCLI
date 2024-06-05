using DirectoryCLI.CommandStyles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Commands
{
    internal class CommandsConfig
    {
        public string DirectoryPath { get; set; }

        public static bool PathValidation(FileInfo path)
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

        public static void ExtensionValidation(FileInfo item)
        {
            
        }
    }
}
