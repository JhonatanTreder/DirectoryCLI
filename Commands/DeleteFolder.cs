using DirectoryCLI.CommandStyles;
using DirectoryCLI.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Commands
{
    internal class DeleteFolder : CommandsConfig
    {
        public static void Execute(FileInfo directoryPath, string[] arguments)
        {
            for (int i = 0; i <= arguments.Length - 3; i++)
            {
                try
                {
                    string fullPath = Path.Combine(directoryPath.FullName, arguments[2 + i]);

                    Directory.Delete(fullPath, true);

                    colors.Red();
                    Console.WriteLine($"Pasta [{arguments[2 + i]}] deletada em [{arguments[0]}]");
                }

                catch 
                {

                }
            }
        }
    }
}
