using DirectoryCLI.Commands;
using DirectoryCLI.CommandStyles;
using DirectoryCLI.Exceptions;
using System;
using System.IO;
using System.Threading;

namespace DirectoryCLI
{
    internal class Program
    {//----------------------------------------------------------------------------------------
        /*Resumo do projeto:
         
        (FAZER QUANDO TERMINAR)

        */
        //----------------------------------------------------------------------------------------
        static void Main()
        {
            //Caminho no projeto: DirectoryCLI.CommandStyles
            Format formatLogs = new Format();//Classe para formatar a CLI no final de cada comando

            formatLogs.UserAndMachineName();

            //----------------------------------------------------------------------------------------
            //Uso do "while" para deixar o programa rodando em looping

            while (true)
            {
            //----------------------------------------------------------------------------------------

                formatLogs = new Format();

                //Caminho no projeto: DirectoryCLI.CommandStyles
                Colors colors = new Colors();//Classe para facilitar o acesso às cores

                string[] args = Console.ReadLine().Split(' ');
                string cmd;

                Console.WriteLine();

                //----------------------------------------------------------------------------------------
                //Switch para atribuir qual é o comando para a variável "cmd"

                switch (args.Length)
                {
                
                    case 2:

                        if (args[1] == "scan" || args[1] == "list")
                        {
                            cmd = args[1];
                        }
                        else
                        {
                            cmd = args[0];
                        }
                        
                    break;

                    case 1:

                        cmd = args[0];

                    break;

                    default:

                        cmd = args[1];

                    break;
                    
                }
                //----------------------------------------------------------------------------------------

                //----------------------------------------------------------------------------------------
                //Essa variável do tipo "FileInfo" será usada apenas nos comandos que usam diretórios

                FileInfo directoryPath = new FileInfo(args[0]);

                //----------------------------------------------------------------------------------------
                try
                {
                    //----------------------------------------------------------------------------------------
                    //Switch uado para identificar qual é o comando e continuar dependendo de qual comando seja

                    switch (cmd)
                    {
                    //----------------------------------------------------------------------------------------

                        //CREATE-FOLDER
                        case "create-folder":

                            for (int i = 0; i <= args.Length - 3; i++)
                            {
                                CreateFolder createFolder = new CreateFolder(args[2 + i]);

                                createFolder.Execute(directoryPath);
                                colors.Green();

                                Console.WriteLine($"Folder [{args[2 + i]}] created in [{args[0]}]");
                            }

                            formatLogs.DirectoryLog();
                            formatLogs.UserAndMachineName();

                        break;

                        //DELETE-FOLDER
                        case "delete-folder":

                            for (int i = 0; i <= args.Length - 3; i++)
                            {
                                DeleteFolder deleteFolder = new DeleteFolder(args[2 + i]);

                                deleteFolder.Execute(directoryPath);
                                colors.Red();

                                Console.WriteLine($"Folder [{args[2 + i]}] deleted in [{args[0]}]");
                            }

                            formatLogs.DirectoryLog();
                            formatLogs.UserAndMachineName();

                        break;

                        //CREATE-FILE
                        case "create-file":

                            for (int i = 0; i <= args.Length - 3; i++)
                            {
                                CreateFile createFile = new CreateFile(args[2 + i]);

                                createFile.Execute(directoryPath);
                                colors.Green();

                                Console.WriteLine();
                                Console.Write($"File [{args[2 + i]}] created in [{args[0]}]");
                                Console.ResetColor();
                            }

                            formatLogs.DirectoryLog();
                            formatLogs.UserAndMachineName();

                        break;

                        //DELETE-FILE
                        case "delete-file":
                            
                            for (int i = 0; i <= args.Length - 3; i++)
                            {
                                DeleteFile deleteFile = new DeleteFile(args[2 + i]);
                                deleteFile.Execute(directoryPath);
                                colors.Red();
                                Console.WriteLine();
                                Console.Write($"File [{args[2 + i]}] deleted in [{args[0]}]");
                            }

                            formatLogs.DirectoryLog();
                            formatLogs.UserAndMachineName();

                        break;

                        //OPEN
                        case "open":
                            //----------------------------------------------------------------------------------------
                            //Algoritmo para identificar se existe um diretório com pastas ou arquivos 

                            //Apenas diminui as verificações em duas variávies: "folderExist" e "fileExist"

                            bool folderExist = Directory.Exists(args[0]) && Directory.Exists(Path.Combine(args[0], args[2]));
                            bool fileExist = Directory.Exists(args[0]) && File.Exists(Path.Combine(args[0], args[2]));

                            if (folderExist || fileExist )
                            {
                                Open open = new Open(args[2]);

                                formatLogs.DirectoryLog();
                                open.Execute(directoryPath);
                                formatLogs.UserAndMachineName();
                            }
                            //----------------------------------------------------------------------------------------

                            //Lança uma exceção personalizada se isso não ocorrer

                            else
                            {
                                throw new OpenCommandException("Error when opening a file or directory");
                            }
                            //----------------------------------------------------------------------------------------
                        break;
                    
                        //OPEN-SITE
                        case "open-site":

                            OpenSite openSite = new OpenSite();

                            formatLogs.SiteLog();
                            openSite.Execute(args[1]);

                            Console.WriteLine();

                            formatLogs.UserAndMachineName();

                        break;

                        //SCAN
                        case "scan": 

                            Scan scan = new Scan();

                            long size = scan.Execute(args[0]);
                            string formatedBytes = scan.FormatBytes(size);

                            Console.Write("Size: ");

                            colors.DarkGray();

                            Console.WriteLine(size + formatedBytes);
                            Console.ResetColor();
                            Console.WriteLine();

                            formatLogs.ScanAndListLogs();
                            formatLogs.UserAndMachineName();

                        break;
                    
                        //COMMANDS
                        case "commands":

                            CommandsInfo commandInfo = new CommandsInfo();

                            commandInfo.Execute();
                            formatLogs.CommandLog();
                            formatLogs.UserAndMachineName();

                        break;

                        //CMD-SINTAXE
                        case "commands-sintaxe":

                            commandInfo = new CommandsInfo();

                            commandInfo.CommandSintaxe();
                            formatLogs.CommandLog();
                            formatLogs.UserAndMachineName();

                        break;

                        //LIST
                        case "list":

                            DirectoryList list = new DirectoryList();
                            list.Execute(args[0]);
                            formatLogs.ScanAndListLogs();

                            Console.WriteLine();

                            formatLogs.UserAndMachineName();

                        break;

                        case "move":

                            for (int i = 2; i < args.Length - 2; i++)
                            {
                                try
                                {
                                    FileInfo item = new FileInfo(args[i]);
                                    FileInfo destiny = new FileInfo(args[args.Length - 1]);
                                    Move move = new Move();

                                    string directoryExists = Path.Combine(directoryPath.FullName, destiny.Name);

                                    bool itemExists = Directory.Exists(Path.Combine(directoryPath.FullName, item.Name));
                                    bool itemExistsInDirectory = Directory.Exists(Path.Combine(directoryExists, item.Name));
                                    
                                    if (!itemExistsInDirectory)
                                    {
                                        move.Execute(directoryPath, item, destiny);
                                        colors.Blue();

                                        Console.Write($"Item [{item}] moved to [{args[0]}\\{destiny}]");
                                        Console.WriteLine();
                                    }
                                    
                                    else
                                    {
                                        throw new ExistingItemException($"Item '[{args[i]}]' cannot be moved");
                                    }
                                }

                                //Coloquei a captura de exceção aqui para facilitar a busca pelo item já existente
                                catch (ExistingItemException ex)
                                {
                                    colors.DarkRed();

                                    Console.Write(ex.Message);

                                    colors.Red();

                                    Console.WriteLine($": The item '{args[i]}' already exists in the final directory");
                                    Console.ResetColor();

                                    colors.Red();

                                    Console.WriteLine();
                                }
                            }

                            formatLogs.DirectoryLog();
                            formatLogs.UserAndMachineName();

                        break;
                    }
                }
                //----------------------------------------------------------------------------------------

                //----------------------------------------------------------------------------------------
                //Tratando exceções

                //Quando o programa não encontra um diretório
                catch (DirectoryNotFoundException)
                {

                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Directory not found!");
                    Console.ResetColor();
                    Console.WriteLine();

                    formatLogs.UserAndMachineName();
                }

                //Quando o prorama não encontra algo para abrir
                catch (OpenCommandException ex)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(ex.Message + ":");

                    string pathNotFound = Path.Combine(args[0], args[2]);

                    colors.Red();

                    Console.WriteLine($"The path '{pathNotFound}' does not exist");

                    formatLogs.DirectoryLog();
                    formatLogs.UserAndMachineName();
                }

                catch (UnauthorizedAccessException ex)
                {

                   
                }
                //----------------------------------------------------------------------------------------
            }
        }
    }
}
