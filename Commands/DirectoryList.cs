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

        public static void Execute(string directory)
        {
            Colors colors = new Colors();
            string[] files = Directory.GetFiles(directory);
            string[] directories = Directory.GetDirectories(directory);


            Console.Write("Total folders: ");
            colors.DarkGray();
            Console.WriteLine(directories.Length);
            Colors.WhiteText();

            Console.Write("Total files: ");
            colors.DarkGray();
            Console.WriteLine(files.Length);
            Colors.WhiteText();

            Console.WriteLine();

            foreach (string folder in directories) 
            {
                
                Console.Write("Folder: ");
                colors.DarkGray();

                Console.WriteLine($"{ Path.GetFileName(folder) }");
                Colors.WhiteText();
            }

            foreach (string item in files)
            {
                Console.Write("File: ");
                colors.DarkGray();

                Console.WriteLine($"{ Path.GetFileName(item) }");
                Colors.WhiteText();
            }

            Console.WriteLine();
        }
    }
}
