using DirectoryCLI.CommandStyles;
using DirectoryCLI.Exceptions;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Commands
{
    internal class CommandsConfig
    {
        public string DirectoryPath { get; set; }

        public static bool PathValidation(FileInfo path)
        {
            if (Directory.Exists(path.FullName) == true)
            {
                return true;
            }

            else
            {
                throw new DirectoryNotFoundException();
            }
        }

        public static void DataValidation(string command, FileInfo directoryPath, FileInfo item, FileInfo destiny)
        {
            string itemPath = Path.Combine(directoryPath.FullName, item.Name);
            string destinyPath = Path.Combine(directoryPath.FullName, destiny.Name);

            switch (command)
            {
                case "zip":

                    if (!Directory.Exists(itemPath))
                    {
                        throw new InvalidDestinationPathException($"O item '{item}' não existe neste diretório.");
                    }

                    if (!File.Exists(destinyPath))
                    {
                        throw new InvalidDestinationPathException($"O lugar de destino '{destiny}' não existe neste diretório.");

                    }

                    if (destiny.Extension != ".zip")
                    {
                        if (File.Exists(destinyPath))
                        {
                            throw new InvalidDestinationPathException($"Você não pode zipar o item '{item}' em um arquivo '{destiny.Extension}'.");
                        }

                        else if (Directory.Exists(destinyPath))
                        {
                            throw new InvalidDestinationPathException($"Você não pode zipar o item '{item}' na pasta'{destiny}'.");
                        }

                        else
                        {
                            throw new InvalidDestinationPathException($"O arquivo de destino '{destiny}' não existe neste diretório.");
                        }
                    }
                    break;

                case "extract":

                    if (!File.Exists(itemPath))
                    {
                        throw new InvalidDestinationPathException($"O arquivo '{item}' não existe neste diretório.");
                    }

                    if (item.Extension == ".zip")
                    {
                        if (File.Exists(destinyPath))
                        {
                            throw new InvalidDestinationPathException($"Você não pode extrair um arquivo '{item.Extension}' para um arquivo qualquer");
                        }

                        else if (!Directory.Exists(destinyPath))
                        {
                            throw new InvalidDestinationPathException($"A pasta de destino '{destiny}' não existe neste diretório.");
                        }
                    }

                    else
                    {
                        throw new InvalidDestinationPathException($"Você não pode extrair um arquivo '{item}' sem extensão '.zip'.");
                    }

                    break;

                default:

                    if (!Directory.Exists(itemPath) && !File.Exists(itemPath))
                    {
                        throw new InvalidDestinationPathException($"O item '{item}' não existe neste diretório.");
                    }

                    if (File.Exists(itemPath))
                    {
                        if (!Directory.Exists(destinyPath))
                        {
                            if (File.Exists(destinyPath))
                            {
                                throw new InvalidDestinationPathException($"Você não pode mover o item '{item}' para um arquivo '{destiny.Extension}'.");
                            }

                            throw new InvalidDestinationPathException($"A pasta de destino '{destiny}' não existe.");
                        }
                    }

                    break;
            }
        }

        public static void ArgumentsValidation(string[] arguments)
        {
            foreach (string args in arguments)
            {
                if (string.IsNullOrWhiteSpace(args) || string.IsNullOrWhiteSpace(args))
                {
                    throw new ArgumentException("Comando inválido: ");
                }
            }
        }

        public static void CommandValidation(string[] arguments)
        {
            string command;
            
            switch (arguments.Length)
            {
                case 1:

                    command = arguments[0];

                    //Todos os comandos extras
                    bool extraCommands = arguments[0] != "exit" && arguments[0] != "clear" && arguments[0] != "commands" && arguments[0] != "commands-sintaxe" && arguments[0] != "system-info";

                    if (extraCommands)
                    {
                        throw new ArgumentException($"Comando '{command}' inválido: ");
                    }

                break;

                case 2:

                    if (Directory.Exists(arguments[0]))
                    {
                        command = arguments[1];

                        if (arguments[1] != "scan" && arguments[1] != "list")
                        {
                            throw new ArgumentException($"Comando '{command}' inválido: ");
                        }
                    }


                    else if (arguments[0] != "open-site")
                    {
                        command = arguments[0];

                        throw new ArgumentException($"Comando '{command}' inválido: ");
                    }

                break;

                default:

                    command = arguments[1];

                    if (Directory.Exists(arguments[0]))
                    {
                        //Todos os comandos que usam diretórios
                        //(zip, rename, create-folder/file, delete-folder/file, extract, move, ct)
                        bool directoryCommands = arguments[1] != "create-file" && arguments[1] != "zip" && arguments[1] != "extract" && arguments[1] != "delete-file" && arguments[1] != "create-folder" && arguments[1] != "delete-folder" && arguments[1] != "move" && arguments[1] != "rename" && arguments[1] != "ct" && arguments[1] != "open";

                        if (directoryCommands)
                        {
                            throw new ArgumentException($"Comando '{command}' inválido: ");
                        }
                    }

                break;

            }
        }
    }
}
