using DirectoryCLI.CommandStyles;
using DirectoryCLI.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Commands
{
    internal class CreateFile : CommandsConfig
    {
        public static void Execute(FileInfo directoryPath, string[] arguments)
        {

            for (int i = 0; i <= arguments.Length - 3; i++)
            {

                FileInfo archiveName = new FileInfo(arguments[2 + i]);
                bool nonExisxtentFile = !File.Exists(Path.Combine(directoryPath.FullName, archiveName.Name));

                try
                {
                    if (nonExisxtentFile)
                    {
                        //Variável para identificar se existe uma pasta com o mesmo nome do arquivo sem extensão
                        bool folderWithTheSameName = Directory.Exists(Path.Combine(directoryPath.FullName, archiveName.Name));

                        //Verifica se um arquivo não possui uma extensão
                        if (string.IsNullOrEmpty(archiveName.Extension) && folderWithTheSameName == true)
                        {
                            throw new FileWithoutExtensionException("Não foi possível criar um arquivo: ");
                        }

                        string fullPath = Path.Combine(directoryPath.FullName, arguments[2 + i]);
                        File.Create(fullPath).Close();

                        colors.Green();

                        Console.WriteLine($"Arquivo [{arguments[2 + i]}] criado em [{arguments[0]}].");
                        Colors.WhiteText();
                    }

                    else
                    {
                        throw new IOException();
                    }
                }
                catch (IOException)
                {
                    IOException ex = new IOException("Não foi possível criar um arquivo: ");
                    colors.DarkRed();

                    Console.Write(ex.Message);

                    Colors.WhiteText();

                    Console.WriteLine($"O arquivo '{arguments[2 + i]}' já existe neste diretório.");

                    Colors.WhiteText();

                }

                catch (FileWithoutExtensionException ex)
                {

                    colors.DarkRed();

                    Console.Write(ex.Message);

                    colors.Red();

                    Console.WriteLine($"Neste diretório já existe uma pasta com o nome '{arguments[2 + i]}'.");
                    Colors.WhiteText();
                }
            }
        }
    }
}
