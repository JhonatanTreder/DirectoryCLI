using DirectoryCLI.CommandStyles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Commands
{
    internal class DirectoryList : CommandsConfig
    {
        public DirectoryList() 
        {

        }

        public void Execute(string directory)
        {
            Colors colors = new Colors();
            string[] files = Directory.GetFiles(directory);
            string[] directories = Directory.GetDirectories(directory);


            Console.Write("Total folders: ");
            colors.DarkGray();
            Console.WriteLine(directories.Length);
            Console.ResetColor();

            Console.Write("Total files: ");
            colors.DarkGray();
            Console.WriteLine(files.Length);
            Console.ResetColor();

            Console.WriteLine();

            foreach (string folder in directories) 
            {
                
                Console.Write("Folder: ");
                colors.DarkGray();

                Console.WriteLine($"[{ Path.GetFileName(folder) }]");
                Console.ResetColor();
            }

            foreach (string item in files)
            {
                Console.Write("File: ");
                colors.DarkGray();

                Console.WriteLine($"[{ Path.GetFileName(item) }]");
                Console.ResetColor();
            }

            Console.WriteLine();
        }
    }
}
