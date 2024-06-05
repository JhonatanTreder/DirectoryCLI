using DirectoryCLI.Commands;
using DirectoryCLI.CommandStyles;
using DirectoryCLI.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Xml.Linq;

namespace DirectoryCLI
{
    internal class Program
    {//----------------------------------------------------------------------------------------

        static void Main()
        {
            //Classe para formatar a CLI no final de cada comando
            FormatLogs formatLogs = new FormatLogs();

            formatLogs.UserAndMachineName();

            //----------------------------------------------------------------------------------------
            //Uso do "while" para deixar o programa rodando em looping

            while (true)
            {
                //----------------------------------------------------------------------------------------

                formatLogs = new FormatLogs();

                Colors colors = new Colors();//Classe para facilitar o acesso às cores

                string[] arguments = Console.ReadLine().Split(' ');
                string commands;

                //----------------------------------------------------------------------------------------
                //Switch para atribuir qual é o comando para a variável "cmd"

                switch (arguments.Length)
                {

                    case 2:

                        if (arguments[1] == "scan" || arguments[1] == "list")
                        {
                            commands = arguments[1];
                            Console.WriteLine();
                        }
                        else
                        {
                            commands = arguments[0];
                            Console.WriteLine();
                        }

                    break;

                    case 1:

                        commands = arguments[0];

                    break;

                    default:

                        if (arguments[0] == "create-template")
                        {
                            commands = arguments[0];
                            Console.WriteLine();
                        }

                        else
                        {
                            commands = arguments[1];
                            Console.WriteLine();
                        }
                    break;

                }
                //----------------------------------------------------------------------------------------

                //----------------------------------------------------------------------------------------
                try
                {
                    //Essa variável do tipo "FileInfo" será usada apenas nos comandos que usam diretórios
                    FileInfo directoryPath = new FileInfo(arguments[0]);

                    //----------------------------------------------------------------------------------------
                    //Switch uado para identificar qual é o comando e continuar dependendo de qual comando seja

                    switch (commands)
                    {
                        //----------------------------------------------------------------------------------------

                        //CREATE-FOLDER
                        case "create-folder":

                            for (int i = 0; i <= arguments.Length - 3; i++)
                            {
                                try
                                {
                                    bool directoryExist = !Directory.Exists(Path.Combine(directoryPath.FullName, arguments[2 + i]));

                                    if (directoryExist)
                                    {
                                        CreateFolder createFolder = new CreateFolder(arguments[2 + i]);

                                        createFolder.Execute(directoryPath);
                                        colors.Green();

                                        Console.WriteLine($"Folder [{arguments[2 + i]}] created in [{arguments[0]}].");
                                    }

                                    else
                                    {
                                        throw new IOException();
                                    }
                                }

                                //Coloquei a captura de exceção aqui para facilitar a busca pelo item já existente
                                catch (IOException)
                                {
                                    IOException ex = new IOException("Unable to create a folder: ");

                                    colors.DarkRed();

                                    Console.Write(ex.Message);

                                    colors.Red();

                                    if (File.Exists(Path.Combine(directoryPath.FullName, arguments[2 + i])))
                                    {
                                        Console.WriteLine($"This directory has a file without extension.");
                                        Console.ResetColor();
                                    }

                                    else
                                    {
                                        Console.WriteLine($"The '{arguments[2 + i]}' folder already exists in this directory.");
                                        Console.ResetColor();
                                    }
                                }
                            }

                            Console.WriteLine();
                            LogAndReset(commands);

                        break;

                        //DELETE-FOLDER
                        case "delete-folder":

                            for (int i = 0; i <= arguments.Length - 3; i++)
                            {
                                try
                                {
                                    if (Directory.Exists(Path.Combine(directoryPath.FullName, arguments[2 + i])))
                                    {
                                        DeleteFolder deleteFolder = new DeleteFolder(arguments[2 + i]);

                                        deleteFolder.Execute(directoryPath);
                                        colors.Red();

                                        Console.WriteLine($"Folder [{arguments[2 + i]}] deleted in [{arguments[0]}]");
                                    }

                                    else
                                    {
                                        throw new ItemNotFoundException("Unable to delete a folder: ");
                                    }
                                }

                                catch (ItemNotFoundException ex)
                                {
                                    colors.DarkRed();

                                    Console.Write(ex.Message);

                                    colors.Red();

                                    Console.WriteLine($"The '{arguments[2 + i]}' folder does not exist in this directory.");
                                    Console.ResetColor();
                                }
                            }

                            LogAndReset(commands);

                        break;

                        //CREATE-FILE
                        case "create-file":

                            for (int i = 0; i <= arguments.Length - 3; i++)
                            {

                                try
                                {

                                    bool fileExist = !File.Exists(Path.Combine(directoryPath.FullName, arguments[2 + i]));

                                    if (fileExist)
                                    {

                                        CreateFile createFile = new CreateFile();
                                        FileInfo archiveName = new FileInfo(arguments[2 + i]);

                                        //Variável para identificar se existe uma pasta com o mesmo nome do arquivo sem extensão
                                        bool folderWithTheSameName = Directory.Exists(Path.Combine(directoryPath.FullName, archiveName.Name));

                                        //Verifica se um arquivo não possui uma extensão
                                        if (string.IsNullOrEmpty(archiveName.Extension) && folderWithTheSameName == true)
                                        {
                                            throw new FileWithoutExtensionException("Unable to create a file: ");
                                        }

                                        //Executa normalmente
                                        createFile.Execute(directoryPath, archiveName);
                                        colors.Green();

                                        Console.WriteLine($"File [{arguments[2 + i]}] created in [{arguments[0]}].");
                                        Console.ResetColor();
                                    }

                                    else
                                    {
                                        throw new IOException();
                                    }
                                }
                                catch (IOException)
                                {
                                    IOException ex = new IOException("Unable to create a file: ");
                                    colors.DarkRed();

                                    Console.Write(ex.Message);

                                    colors.Red();


                                    Console.WriteLine($"The file '{arguments[2 + i]}' already exists in this directory.");



                                    Console.WriteLine($"This directory already has a folder with that name.");
                                    Console.ResetColor();

                                }

                                catch (FileWithoutExtensionException ex)
                                {

                                    colors.DarkRed();

                                    Console.Write(ex.Message);

                                    colors.Red();

                                    Console.WriteLine($"This directory already has a folder with that name.");
                                    Console.ResetColor();
                                }
                            }

                            Console.WriteLine();
                            LogAndReset(commands);

                        break;

                        //DELETE-FILE
                        case "delete-file":

                            for (int i = 0; i <= arguments.Length - 3; i++)
                            {
                                try
                                {
                                    if (File.Exists(Path.Combine(directoryPath.FullName, arguments[2 + i])))
                                    {
                                        DeleteFile deleteFile = new DeleteFile(arguments[2 + i]);

                                        deleteFile.Execute(directoryPath);
                                        colors.Red();

                                        Console.WriteLine($"File [{arguments[2 + i]}] deleted in [{arguments[0]}]");
                                    }
                                    else
                                    {
                                        throw new ItemNotFoundException("Unable to delete a file: ");
                                    }
                                }

                                catch (ItemNotFoundException ex)
                                {
                                    colors.DarkRed();

                                    Console.Write(ex.Message);

                                    colors.Red();

                                    Console.WriteLine($"The '{arguments[2 + i]}' file does not exist in this directory.");
                                    Console.ResetColor();
                                }
                            }

                            Console.WriteLine();
                            LogAndReset(commands);

                        break;

                        //OPEN
                        case "open":
                            //----------------------------------------------------------------------------------------
                            //Algoritmo para identificar se existe um diretório com pastas ou arquivos 

                            //Apenas diminui as verificações em duas variávies: "folderExist" e "archiveExist"

                            bool folderExist = Directory.Exists(arguments[0]) && Directory.Exists(Path.Combine(arguments[0], arguments[2]));
                            bool archiveExist = Directory.Exists(arguments[0]) && File.Exists(Path.Combine(arguments[0], arguments[2]));

                            if (folderExist || archiveExist)
                            {
                                Open open = new Open(arguments[2]);
                                
                                open.Execute(directoryPath);

                                LogAndReset(commands);
                            }
                            //----------------------------------------------------------------------------------------

                            //Lança uma exceção personalizada se isso não ocorrer

                            else
                            {
                                throw new OpenCommandException("Error when opening a file or directory. ");
                            }
                            //----------------------------------------------------------------------------------------
                            break;

                        //OPEN-SITE
                        case "open-site":

                            OpenSite openSite = new OpenSite();

                            formatLogs.SiteLog();
                            openSite.Execute(arguments[1]);

                            Console.WriteLine();

                            formatLogs.UserAndMachineName();

                        break;

                        //SCAN
                        case "scan":

                            Scan scan = new Scan();

                            long size = scan.Execute(arguments[0]);
                            string formatedBytes = scan.FormatBytes(size);

                            Console.Write("Size: ");

                            colors.DarkGray();

                            Console.WriteLine(size + formatedBytes);
                            Console.ResetColor();
                            Console.WriteLine();

                            LogAndReset(commands);

                        break;

                        //COMMANDS
                        case "commands":

                            CommandsInfo commandInfo = new CommandsInfo();

                            Console.WriteLine();
                            commandInfo.Execute();

                            LogAndReset(commands);

                        break;

                        //CMD-SINTAXE
                        case "commands-sintaxe":

                            commandInfo = new CommandsInfo();

                            Console.WriteLine();
                            commandInfo.CommandSintaxe();

                            LogAndReset(commands);

                        break;

                        //LIST
                        case "list":

                            DirectoryList list = new DirectoryList();

                            list.Execute(arguments[0]);

                            LogAndReset(commands);

                        break;

                        //MOVE
                        case "move":

                            Move move = new Move();
                            List<string> items = new List<string>();

                            for (int i = 2; i < arguments.Length - 2; i++)
                            {
                                try
                                {
                                    FileInfo item = new FileInfo(arguments[i]);
                                    FileInfo destiny = new FileInfo(arguments[arguments.Length - 1]);

                                    items.Add(arguments[i]);
                                    move.Execute(directoryPath, item, destiny);

                                    Console.Write("Item:");

                                    colors.DarkGray();

                                    Console.Write($" [{item}] moved to [{arguments[0]}\\{destiny}]");
                                    Console.WriteLine();
                                    Console.ResetColor();
                                }

                                //Coloquei a captura de exceção aqui para facilitar a busca pelo item já existente
                                catch (IOException)
                                {
                                    IOException ex = new IOException("Cannot move an item, because it already exists: ");
                                    colors.DarkRed();

                                    Console.Write(ex.Message);

                                    colors.Red();

                                    Console.WriteLine($"The item '{arguments[i]}' already exists in the final directory");
                                    Console.ResetColor();
                                }
                            }

                            Console.WriteLine();

                            LogAndReset(commands);

                        break;

                        //EXTRACT
                        case "extract":

                            Extract extract = new Extract();

                            for (int i = 2; i < arguments.Length - 2; i++)
                            {

                                FileInfo item = new FileInfo(arguments[i]);
                                FileInfo destiny = new FileInfo(arguments[arguments.Length - 1]);

                                extract.Execute(directoryPath, item, destiny);
                                colors.Blue();
                            }

                            Console.WriteLine();
                            LogAndReset(commands);

                        break;

                        //ZIP
                        case "zip":

                            Zip zip = new Zip();
                            List<string> elements = new List<string>();

                            try
                            {
                                for (int i = 2; i < arguments.Length - 2; i++)
                                {
                                    
                                    FileInfo item = new FileInfo(arguments[i]);
                                    FileInfo destiny = new FileInfo(arguments[arguments.Length - 1]);

                                    string pathValidation = (Path.Combine(directoryPath.FullName, destiny.Name));

                                    if (destiny.Extension != ".zip")
                                    {
                                        if (File.Exists(pathValidation))
                                        {
                                            throw new InvalidDestinationPathException($"Você não pode zipar o item '{item}' em um arquivo '{destiny.Extension}'");
                                        }

                                        else if (Directory.Exists(pathValidation))
                                        {
                                            throw new InvalidDestinationPathException($"Você não pode zipar o item '{item}' na pasta '{destiny}'");
                                        }

                                        else
                                        {
                                            throw new InvalidDestinationPathException($"O lugar de destino '{destiny}' não existe no diretório {directoryPath.FullName}");
                                        }
                                    }

                                    elements.Add(arguments[i]);
                                    zip.Execute(directoryPath, item, destiny);
                                    colors.Blue();
                                }

                                foreach (string element in elements)
                                {
                                    Console.ResetColor();
                                    Console.Write("Item:");

                                    colors.DarkGray();

                                    Console.WriteLine($" [{element}] zipped to [{arguments[0]}\\{arguments[arguments.Length - 1]}]");
                                }

                                LogAndReset(commands);
                            }

                            catch (InvalidDestinationPathException ex)
                            {
                                colors.DarkRed();
                                Console.Write("Erro ao zipar um item: ");

                                Console.ResetColor();
                                Console.WriteLine(ex.Message);

                                LogAndReset(commands);
                            }
                        break;

                        //RENAME
                        case "rename":

                            Rename rename = new Rename();
                            List<string> objects = new List<string>();

                            FileInfo atualName = new FileInfo(arguments[2]);
                            FileInfo finalName = new FileInfo(arguments[4]);

                            rename.Execute(directoryPath, atualName, finalName);

                            Console.ResetColor();
                            Console.Write("Item:");

                            colors.DarkGray();

                            Console.WriteLine($" [{atualName}] renamed to [{finalName}]");

                            Console.WriteLine();
                            LogAndReset(commands);

                        break;

                        //CREATE-TEMPLATE
                        case "ct":
                            //ct dotnet new mvc --framework netcoreapp3.1 -o MyNewProject --auth Individual --use-local-db true
                            CreateTemplate createTemplate = new CreateTemplate();
                            Dictionary<string, string> additionalParams = new Dictionary<string, string> { { arguments[9], arguments[10] }, { arguments[11], arguments[12] } };

                            createTemplate.CreateAspNetCoreMvcTemplate(arguments[6], arguments[8], additionalParams);

                        break;
                        //----------------------------------------------------------------------------------------
                        //Comandos simples

                        //CLEAR
                        case "clear":

                            Console.Clear();
                            formatLogs.UserAndMachineName();

                        break;

                        //EXIT
                        case "exit":

                            Environment.Exit(0);

                        break;
                        //----------------------------------------------------------------------------------------
                    }
                }
                //----------------------------------------------------------------------------------------

                //----------------------------------------------------------------------------------------
                //Tratando exceções

                //Quando o programa não encontra um diretório
                catch (DirectoryNotFoundException)
                {
                    colors.DarkRed();

                    Console.WriteLine($"Directory '{arguments[0]}' not found!");
                    Console.ResetColor();
                    Console.WriteLine();

                    formatLogs.UserAndMachineName();
                }

                //Quando o prorama não encontra algo para abrir
                catch (OpenCommandException ex)
                {
                    colors.DarkRed();
                    Console.Write(ex.Message + ":");

                    string pathNotFound = Path.Combine(arguments[0], arguments[2]);

                    colors.Red();

                    Console.WriteLine($"The path '{pathNotFound}' does not exist");

                    Console.WriteLine();
                    LogAndReset(commands);
                }
                catch (ArgumentException)
                {
                    ArgumentException ex = new ArgumentException("Argumentos invalidos: ");

                    colors.DarkRed();

                    Console.Write(ex.Message);

                    Console.ResetColor();
                    Console.WriteLine("Escreva 'commands' para ver a lista de comandos.");
                    Console.ResetColor();

                    Console.WriteLine();

                    formatLogs.UserAndMachineName();
                }
                //----------------------------------------------------------------------------------------
            }
        }

        public static void LogAndReset(string command)
        {
            FormatLogs formatLogs = new FormatLogs();

            switch (command) 
            {
               /*
                  Casos específicos:

                  'scan'
                  'commands'
                  'open-site' 

                */

                //Log para o comando 'scan'
                case "scan":

                    formatLogs.ScanAndListLogs();
                    formatLogs.UserAndMachineName();

                break;

                //Log para o comando 'list'
                case "list":

                    formatLogs.ScanAndListLogs();
                    formatLogs.UserAndMachineName();

                break;

                //Log para o comando 'commands'
                case "commands":

                    formatLogs.CommandLog();
                    formatLogs.UserAndMachineName();

                break;

                //Log para o comando 'open-site'
                case "open-site":

                    formatLogs.SiteLog();
                    formatLogs.UserAndMachineName();

                break;

                //Log para os comandos de diretórios
                default:

                    Console.WriteLine();
                    formatLogs.DirectoryLog();
                    formatLogs.UserAndMachineName();

                break;
            }
        }
    }
}
