using DirectoryCLI.Interfaces;

namespace DirectoryCLI.Handlers
{
    internal class CommandValidator : ICommandValidator
    {
        public void ArgumentsValidation(string[] arguments)
        {
            foreach (string args in arguments)
            {
                if (string.IsNullOrWhiteSpace(args) && !string.IsNullOrEmpty(arguments[0]))
                {
                    throw new ArgumentException("Argumento inválido: ");
                }
            }

            string command;

            switch (arguments.Length)
            {
                case 1:

                    command = arguments[0];

                    HashSet<string> extraCommands = new HashSet<string> {

                        "exit",
                        "clear",
                        "commands",
                        "commands-sintaxe",
                        "system-info",
                        "log-on",
                        "log-off"

                    };

                    if (!extraCommands.Contains(command))
                    {
                        throw new ArgumentException($" Comando '{command}' inválido: ");
                    }

                    break;

                case 2:

                    if (!Directory.Exists(arguments[0]))
                    {
                        command = arguments[0].ToLower();
                    }

                    else
                    {
                        command = arguments[1].ToLower();
                    }

                    HashSet<string> twoArgumentsCommands = new HashSet<string> {

                        "scan",
                        "list",
                        "open-site",
                        "del-files",
                        "del-folders",
                        "dotnet",
                        "docker",
                        "git"

                    };

                    if (!twoArgumentsCommands.Contains(command))
                    {
                        throw new ArgumentException($" Comando '{command}' inválido: ");
                    }

                    break;

                default:

                    if (!Directory.Exists(arguments[0]))
                    {
                        command = arguments[0];
                    }

                    else
                    {
                        command = arguments[1];
                    }


                    HashSet<string> directoryCommands = new HashSet<string> {

                        "create-file",
                        "zip",
                        "extract",
                        "delete-file",
                        "create-folder",
                        "delete-folder",
                        "move",
                        "rename",
                        "dotnet",
                        "open",
                        "del-files",
                        "del-folders",
                        "list",
                        "docker",
                        "git"
                    };

                    if (!directoryCommands.Contains(command))
                    {
                        throw new ArgumentException($" Comando '{command}' inválido: ");
                    }

                    break;

            }
        }

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

        public bool IsValidOpenCommand(string parameter)
        {
            if (parameter == "-d" || parameter == "-f") return true;

            Console.WriteLine($"Parâmetro '{parameter}' inválido.");
            Console.WriteLine();

            return false;
        }

        public bool IsValidExtractCommand(string[] arguments)
        {

            if (!Directory.Exists(arguments[0]))
            {
                Console.WriteLine($"Diretório '{arguments[0]}' inexistente");
                Console.WriteLine();
                return false;
            }

            if (arguments.Length < 4)
            {
                Console.WriteLine("Número de argumentos inválidos.");
                Console.WriteLine();
                return false;
            }

            if (arguments[arguments.Length - 1] == "to-here" || arguments[arguments.Length - 2] == "to" || arguments[arguments.Length - 2] == "to-new")
                return true;

            return false;
        }

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
