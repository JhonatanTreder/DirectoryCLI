using DirectoryCLI.CommandStyles;
using DirectoryCLI.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Commands
{
    internal class DeleteFile : CommandsConfig
    {
        static  string ArchiveName { get; set; }

        public DeleteFile(string archiveName)
        {
            ArchiveName = archiveName;
        }
        public static void Execute(FileInfo directoryPath, string[] arguments)
        {
            for (int i = 0; i <= arguments.Length - 3; i++)
            {
                try
                {
                    if (File.Exists(Path.Combine(directoryPath.FullName, arguments[2 + i])))
                    {
                        string fullPath = Path.Combine(directoryPath.FullName, ArchiveName);
                        File.Delete(fullPath);

                        colors.Red();

                        Console.WriteLine($"Arquivo [{arguments[2 + i]}] deletado em [{arguments[0]}]");
                    }
                    else
                    {
                        throw new ItemNotFoundException("Não foi possível criar um arquivo: ");
                    }
                }
                catch (ItemNotFoundException ex)
                {
                    colors.DarkRed();

                    Console.Write(ex.Message);

                    colors.Red();

                    Console.WriteLine($"O arquivo '{arguments[2 + i]}' não existe neste diretório.");
                    Colors.WhiteText();
                }
            }
        }
    }
}
