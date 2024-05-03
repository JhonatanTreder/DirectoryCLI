using DirectoryCLI.Commands;
using DirectoryCLI.CommandStyles;
using DirectoryCLI.Exceptions;
using System;
using System.IO;
using System.Threading;

namespace DirectoryCLI
{
    internal class Program
    {
        static void Main()
        {
            Format formatLogs = new Format();

            formatLogs.UserAndMachineName();

            //----------------------------------------------------------------------------------------
            //Uso do "while" para deixar o programa rodando em looping

            while (true)
            {
            //----------------------------------------------------------------------------------------

                formatLogs = new Format();
                Colors colors = new Colors();
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
                                DeleteFile deleteFile = new DeleteFile(args[0], args[2 + i]);
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

                            Console.WriteLine(size + formatedBytes);

                            Console.WriteLine();

                            formatLogs.UserAndMachineName();
                            formatLogs.ScanAndListLogs();

                        break;
                    
                        //COMMANDS
                        case "commands":

                            CommandsConfig commandsConfig = new CommandsConfig();
                            Console.Write(commandsConfig);

                        break;

                        //LIST
                        case "list":

                            Contents list = new Contents();
                            list.Execute(args[0]);

                            formatLogs.ScanAndListLogs();
                            Console.WriteLine();
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
                    Console.WriteLine($" The path '{pathNotFound}' does not exist");

                    formatLogs.DirectoryLog();
                    formatLogs.UserAndMachineName();
                }
                //----------------------------------------------------------------------------------------
            }
        }
    }
}
