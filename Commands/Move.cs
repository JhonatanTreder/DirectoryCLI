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
    internal class Move : CommandsConfig
    {
        public Move()
        {

        }

        public static void Execute(FileInfo directoryPath,string command, string[] arguments)
        {

            List<string> items = new List<string>();

            for (int i = 2; i < arguments.Length - 2; i++)
            {
                try
                {
                    FileInfo item = new FileInfo(arguments[i]);
                    FileInfo destiny = new FileInfo(arguments[arguments.Length - 1]);

                    string itemPath = Path.Combine(directoryPath.FullName, item.Name);
                    string destinyPath = Path.Combine(directoryPath.FullName, destiny.Name);
                    string fullPath = Path.Combine(destinyPath, arguments[i]);

                    //Variável para identificar se o usuário digitou outra coisa além de 'to'.
                    bool userArgument = arguments[arguments.Length - 2] != "to";

                    if (userArgument && File.Exists(itemPath))
                    {
                        throw new ArgumentException($"Erro ao tentar mover um arquivo, '{arguments[arguments.Length - 2]}' não é um identificador válido: ");
                    }

                    else if (userArgument && Directory.Exists(itemPath))
                    {
                        throw new ArgumentException($"Erro ao tentar mover uma pasta, '{arguments[arguments.Length - 2]}' não é um identificador válido: ");
                    }

                    //-----------------------------------------------------------------------
                    //Validando argumentos do comando 'move'

                    DataValidation(command, directoryPath, item, destiny);

                    //------------------------------------------------------------------------

                    items.Add(arguments[i]);

                    Directory.Move(itemPath, fullPath);

                    Console.Write("Item:");

                    colors.DarkGray();

                    Console.Write($" [{item}] movido para [{arguments[0]}\\{destiny}]");
                    Colors.WhiteText();

                    Console.WriteLine();
                }
                //Coloquei as capturas de exceções aqui para facilitar a busca pelo item já existente
                catch (IOException)
                {
                    IOException ex = new IOException("Cannot move an item, because it already exists: ");
                    colors.DarkRed();

                    Console.Write(ex.Message);

                    colors.Red();

                    Console.WriteLine($"O item '{arguments[i]}' already exists in the final directory");
                    Colors.WhiteText();

                    LogAndReset(command);
                }
                catch (InvalidDestinationPathException ex)
                {
                    colors.DarkRed();
                    Console.Write("Erro ao mover um item: ");

                    Colors.WhiteText();
                    Console.WriteLine(ex.Message);

                    LogAndReset(command);
                }
            }


        }
    }
}
/* if (IsValidDirectoryPath(directoryPath) == true)
            {

            }

            else
            {
                throw new DirectoryNotFoundException();
            }



                D:\Test\Pasta
                D:\Test\hl

                D:\Test\Pasta\hl
                D:\Test\hl\Pasta
        */