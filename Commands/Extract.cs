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
    internal class Extract : CommandsConfig
    {

        public Extract()
        {

        }

        public static void Execute(FileInfo directoryPath, string command, string[] arguments)
        {

            for (int i = 2; i < arguments.Length - 2; i++)
            {
                try
                {

                    FileInfo item = new FileInfo(arguments[i]);
                    FileInfo destiny = new FileInfo(arguments[arguments.Length - 1]);

                    string destinyPath = Path.Combine(directoryPath.FullName, destiny.Name);
                    string itemPath = Path.Combine(directoryPath.FullName, item.Name);

                    //Variável para identificar se o usuário digitou outra coisa além de 'to'.
                    bool userArgument = arguments[arguments.Length - 2] != "to";

                    if (userArgument)
                    {
                        throw new ArgumentException($"Erro ao tentar extrair um arquivo, '{arguments[arguments.Length - 2]}' não é um identificador válido: ");
                    }

                    else if (arguments[arguments.Length - 1] != "--here" && !Directory.Exists(destinyPath))
                    {
                        throw new ArgumentException($"Erro ao tentar extrair um arquivo, '{arguments[arguments.Length - 1]}' não é um identificador válido: ");
                    }

                    //-----------------------------------------------------------------------
                    //Validação dos dados (item, destiny) - EXTRACT

                    DataValidation(command, directoryPath, item, destiny);

                    //------------------------------------------------------------------------

                    ZipFile.ExtractToDirectory(itemPath, destinyPath);

                    using (ZipArchive archive = ZipFile.OpenRead(itemPath))
                    {
                        Console.WriteLine("Extracted contents:");
                        Console.WriteLine();

                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {

                            if (entry.FullName.EndsWith("/"))
                            {
                                Console.Write("Folder:");
                                colors.DarkGray();

                                Console.WriteLine($" [{entry.FullName.Remove(arguments.Length - 1)}]");
                                Colors.WhiteText();
                            }

                            else
                            {
                                Console.Write("File:");
                                colors.DarkGray();

                                Console.WriteLine($" [{entry.FullName}]");
                                Colors.WhiteText();
                            }
                        }
                    }
                }
                catch (InvalidDestinationPathException ex)
                {
                    colors.DarkRed();
                    Console.Write("Erro ao extrair um arquivo: ");

                    Colors.WhiteText();
                    Console.WriteLine(ex.Message);

                }
                catch (IOException ex)
                {
                    colors.DarkRed();
                    Console.Write("Erro ao extrair um arquivo: ");

                    Colors.WhiteText();
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine();
        }
    }
}
