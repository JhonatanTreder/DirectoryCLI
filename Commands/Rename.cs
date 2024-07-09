using DirectoryCLI.CommandStyles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Commands
{
    internal class Rename : CommandsConfig
    {
        public Rename() 
        {

        }

        public static void Execute(FileInfo directoryPath, string[] arguments)
        {
            string atualName = arguments[2];
            string finalName = arguments[4];

            string namePath = Path.Combine(directoryPath.FullName, atualName);
            string finalNamePath = Path.Combine(directoryPath.FullName, finalName);

            if (Directory.Exists(namePath))
            {
                Directory.Move(namePath, finalNamePath);
            }

            else
            {
                File.Move(namePath, finalNamePath);
            }

            Colors.WhiteText();
            Console.Write("Item:");

            colors.DarkGray();

            Console.WriteLine($" '{atualName}' renomeado para '{finalName}'.");

        }
    }
}
