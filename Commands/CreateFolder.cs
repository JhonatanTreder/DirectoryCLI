using DirectoryCLI.CommandStyles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Commands
{
    internal class CreateFolder : CommandsConfig
    {

        public static void Execute(FileInfo directoryPath, string[] arguments)
        {
            for (int i = 0; i <= arguments.Length - 3; i++)
            {

                string fullPath = Path.Combine(directoryPath.FullName, arguments[2 + i]);

                try
                {
                    if (!Directory.Exists(fullPath))
                    {
                        Directory.CreateDirectory(fullPath);

                        colors.Green();

                        Console.WriteLine($"Folder [{arguments[2 + i]}] created in [{arguments[0]}].");
                    }

                    else
                    {
                        throw new IOException();
                    }
                }
                //Coloquei a captura de exceção dentro de um 'for' para facilitar a busca pelo item já existente
                catch (IOException)
                {
                    IOException ex = new IOException("Não foi possível criar uma pasta: ");

                    colors.DarkRed();

                    Console.Write(ex.Message);

                    if (File.Exists(fullPath))
                    {
                        Colors.WhiteText();
                        Console.WriteLine($"Este diretório possui um arquivo sem extensão, '{arguments[2 + i]}'.");
                    }

                    else
                    {
                        Colors.WhiteText();
                        Console.WriteLine($"A pasta '{arguments[2 + i]}' já existe neste diretório.");
                    }
                }
            }
        }
    }
}
