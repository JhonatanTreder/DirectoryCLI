using DirectoryCLI.Interfaces;

namespace DirectoryCLI.Handlers
{
    internal class CommandValidator : ICommandValidator
    {
        //Valida os argumentos escritos pelo usuário.

        //-----------------------------------------------------------------------------------------
        public bool ArgumentsValidation(string[] arguments, string command) //Verifica se as strings são válidas.
        {
            foreach (string args in arguments)
            {
                if (string.IsNullOrWhiteSpace(args) && !string.IsNullOrEmpty(arguments[0]))
                {
                    throw new ArgumentException("Argumento inválido: ");
                }
            }
            //--------------------------------------------------------------------------------------


            //--------------------------------------------------------------------------------------
            //Verificação de cada comando.


            if (Directory.Exists(arguments[0]))
            {
                HashSet<string> directoryCommands = new HashSet<string>
                {
                        "scan",
                        "list",
                        "create-file",
                        "zip",
                        "extract",
                        "delete-file",
                        "create-folder",
                        "delete-folder",
                        "move",
                        "rename",
                        "open",
                        "del-files",
                        "del-folders",
                };

                if (!directoryCommands.Contains(command))
                {
                    Console.WriteLine("Erro: ");
                    Console.WriteLine($"O comando '{command}' não é reconhecido como um comando válido para uso em diretórios.");
                    Console.WriteLine("Escreva 'dir-cmds' para ver a lista de comandos válidos.");
                    Console.WriteLine();
                    return false;
                }

                return true;
            }

            else
            {
                HashSet<string> extraCommands = new HashSet<string>()
                {
                    "exit",
                    "system-info",
                    "clear",
                    "open-site",
                    "log-on",
                    "log-off",
                    "cmds-sintaxe",
                    "cmds",
                    "dir-cmds"

                };

                if (extraCommands.Contains(command))
                {
                    return true;
                }

                return false;
            }
            //--------------------------------------------------------------------------------------
        }

        //Validação básica para o comando 'zip'.
        public bool IsValidZipCommand(string[] arguments)
        {
            string destiny = arguments[arguments.Length - 1];
            string destinyFile = Path.Combine(arguments[0], destiny);

            if (arguments.Length < 5)
            {
                Console.WriteLine("Quantidade de argumentos inválidos");
                Console.WriteLine("Quantidade mínima: 5");
                Console.WriteLine("<diretório> <zip> <arquivo/pasta> <to> <arquivo .zip de destino>");
                Console.WriteLine();

                return false;
            }

            if (Directory.Exists(destinyFile))
            {
                Console.WriteLine("O arquivo de destino deve ser um arquivo .zip");
                Console.WriteLine();

                return false;
            }

            if (File.Exists(destinyFile))
            {
                FileInfo itemFile = new FileInfo(destinyFile);

                if (itemFile.Extension != ".zip")
                {
                    Console.WriteLine($"O arquivo de destino deve ser um arquivo .zip");
                    Console.WriteLine();

                    return false;
                }

                return true;
            }

            Console.WriteLine($"Arquivo de destino '{destinyFile}' não existe");

            return false;
        }

        //Validação básica para o comando 'list'.
        public bool IsValidListCommand(string[] arguments)
        {
            if (arguments.Length < 2)
            {
                Console.WriteLine("Comando inválido. O diretório e parâmetros são obrigatórios.");
                Console.WriteLine();
                return false;
            }

            DirectoryInfo directory = new DirectoryInfo(arguments[0]);

            if (!directory.Exists)
            {
                Console.WriteLine($"Diretório {directory.FullName} inexistente.");
                Console.WriteLine();
                return false;
            }

            if (arguments.Length == 2)
            {
                return true;
            }

            string parameter = arguments[2];

            if (parameter != "-e")
            {
                Console.WriteLine($"Parâmetro '{parameter}' inválido.");
                Console.WriteLine();
                return false;
            }

            if (arguments.Length < 4)
            {
                Console.WriteLine($"O parâmetro '-e' necessita de um tipo de extensão logo em seguida.");
                Console.WriteLine();
                return false;
            }

            return true;
        }

        //Validação básica para o comando 'open'.
        public bool IsValidOpenCommand(string parameter)
        {
            if (parameter == "-d" || parameter == "-f") return true;

            Console.WriteLine($"Parâmetro '{parameter}' inválido.");
            Console.WriteLine();

            return false;
        }

        //Validação básica para o comando 'extract'.
        public bool IsValidExtractCommand(string[] arguments)
        {
            if (arguments.Length < 4 || arguments.Length > 6)
            {
                Console.WriteLine("Número de argumentos inválidos.");
                Console.WriteLine();
                return false;
            }

            if (!Directory.Exists(arguments[0]))
            {
                Console.WriteLine($"Diretório '{arguments[0]}' inexistente");
                Console.WriteLine();
                return false;
            }

            if (arguments[arguments.Length - 1] == "to-here" || arguments[arguments.Length - 2] == "to" || arguments[arguments.Length - 2] == "to-new")
                return true;

            Console.WriteLine($"Parâmetro inválido.");
            Console.WriteLine();
            return false;
        }

        //Validação básica para o comando 'rename'.
        public bool IsValidRenameCommand(string[] arguments)
        {
            DirectoryInfo directoryPath = new DirectoryInfo(arguments[0]);

            string namePath = Path.Combine(directoryPath.FullName, arguments[2]);
            string finalNamePath = Path.Combine(directoryPath.FullName, arguments[4]);
            string parameter = arguments[3];

            if (parameter != "to")
            {
                Console.WriteLine($"Parâmetro '{parameter}' inválido.");
                Console.WriteLine();
                return false;
            }

            if (Directory.Exists(namePath))
            {
                if (Directory.Exists(finalNamePath))
                {
                    Console.WriteLine($"A pasta '{finalNamePath}' já existe.");
                    Console.WriteLine();
                    return false;
                }

                else if (File.Exists(finalNamePath))
                {
                    Console.WriteLine($"Não é possível renomear uma pasta para um arquivo já existente.");
                    Console.WriteLine();
                    return false;
                }

                return true;
            }

            else if (File.Exists(namePath))
            {
                if (Directory.Exists(finalNamePath))
                {
                    Console.WriteLine($"Não é possível renomear um arquivo para uma pasta já existente.");
                    Console.WriteLine();
                    return false;
                }

                else if (File.Exists(finalNamePath))
                {
                    Console.WriteLine($"A pasta '{finalNamePath}' já existe.");
                    Console.WriteLine();
                    return false;
                }

                return true;
            }

            else
            {
                Console.WriteLine($" O item '{namePath}' não existe para ser renomeado.");
                Console.WriteLine();
                return false;
            }
        }
    }
}
