using DirectoryCLI.CommandStyles;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Commands
{
    internal class Extract : CommandsConfig
    {
        Colors colors;

        public Extract() 
        {

        }

        public void Execute(FileInfo directoryPath, FileInfo item, FileInfo destiny)
        {
            string itemPath = Path.Combine(directoryPath.FullName, item.Name);
            string destinyPath = Path.Combine(directoryPath.FullName, destiny.Name);

            using (ZipArchive archive = ZipFile.OpenRead(itemPath))
            {
                Console.WriteLine("Extracted contents:");

                foreach (ZipArchiveEntry entry in archive.Entries)
                {

                    if (entry.FullName.EndsWith("/"))
                    {
                        Console.Write("Folder:");
                        colors.DarkGray();
                        Console.WriteLine($" [{entry.FullName}]");
                    }

                    else
                    {
                        Console.Write("File:");
                        colors.DarkGray();
                        Console.WriteLine($" [{entry.FullName}]");
                    }
                }
            }

            ZipFile.ExtractToDirectory(itemPath, destinyPath);
        }
    }
}
