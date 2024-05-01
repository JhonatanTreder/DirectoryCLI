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
            Colors colors = new Colors();

            colors.Blue();

            Console.Write("#");

            colors.DarkPurple();

            Console.WriteLine(System.Environment.UserName + " - " + System.Environment.MachineName);

            Console.ResetColor();

            while (true)
            {
                colors = new Colors();
                string[] args = Console.ReadLine().Split(' ');
                string cmd;

                Console.WriteLine();

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
                FileInfo directoryPath = new FileInfo(args[0]);

                try
                {

                    switch (cmd)
                    {
                        case "create-folder":

                            for (int i = 0; i <= args.Length - 3; i++)
                            {
                                CreateFolder createFolder = new CreateFolder(args[2 + i]);
                                createFolder.Execute(directoryPath);
                                colors.Green();
                                Console.WriteLine($"Folder [{args[2 + i]}] created in [{args[0]}]");
                            }

                            Console.ResetColor();

                            Console.WriteLine();

                            colors.Yellow();

                            Console.WriteLine("arguments used: <directory> <command> <conclusion>");

                            Console.ResetColor();

                            Console.WriteLine();

                            colors.Blue();

                            Console.Write("#");

                            colors.DarkPurple();
                            Console.WriteLine(System.Environment.UserName + " - " + System.Environment.MachineName);
                            Console.ResetColor();

                        break;

                        case "delete-folder":

                            for (int i = 0; i <= args.Length - 3; i++)
                            {
                                DeleteFolder deleteFolder = new DeleteFolder(args[2 + i]);
                                deleteFolder.Execute(directoryPath);
                                colors.Red();
                                Console.WriteLine($"Folder [{args[2 + i]}] deleted in [{args[0]}]");
                            }
                            Console.ResetColor();

                            Console.WriteLine();

                            colors.Yellow();
                            Console.WriteLine("arguments used: <directory> <command> <conclusion>");

                            Console.ResetColor();
                            
                            Console.WriteLine();

                            colors.Blue();

                            Console.Write("#");

                            colors.DarkPurple();

                            Console.WriteLine(System.Environment.UserName + " - " + System.Environment.MachineName);
                            Console.ResetColor();

                        break;

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

                            Console.ResetColor();

                            Console.WriteLine();

                            colors.Yellow();
                            Console.WriteLine("arguments used: <directory> <command> <conclusion>");
                            
                            Console.ResetColor();

                            Console.WriteLine();

                            colors.Blue();

                            Console.Write("#");

                            colors.DarkPurple();
                            Console.WriteLine(System.Environment.UserName + " - " + System.Environment.MachineName);
                            Console.ResetColor();

                        break;

                        case "delete-file":
                            
                            for (int i = 0; i <= args.Length - 3; i++)
                            {
                                DeleteFile deleteFile = new DeleteFile(args[0], args[2 + i]);
                                deleteFile.Execute(directoryPath);
                                colors.Red();
                                Console.WriteLine();
                                Console.Write($"File [{args[2 + i]}] deleted in [{args[0]}]");
                                Console.ResetColor();
                            }

                            Console.WriteLine();

                            colors.Yellow();
                            Console.WriteLine("arguments used: <directory> <command> <conclusion>");
                            
                            Console.ResetColor();

                            Console.WriteLine();

                            colors.Blue();

                            Console.Write("#");

                            colors.DarkPurple();
                            Console.WriteLine(System.Environment.UserName + " - " + System.Environment.MachineName);
                            Console.ResetColor();

                        break;

                        case "open":

                            colors.Yellow();
                            Console.WriteLine("arguments used: <directory> <command> <conclusion>");

                            Open open = new Open(args[2]);
                            open.Execute(directoryPath);
                            Console.ResetColor();

                            Console.WriteLine();

                            colors.Blue();

                            Console.Write("#");

                            colors.DarkPurple();
                            Console.WriteLine(System.Environment.UserName + " - " + System.Environment.MachineName);
                            Console.ResetColor();

                        break;
                    
                        case "open-site":

                            colors.Yellow();
                            Console.WriteLine("arguments used: <command> <site/domain name>");
                            Console.ResetColor();

                            OpenSite openSite = new OpenSite();
                            openSite.Execute(args[1]);

                            Console.WriteLine();

                            Console.ResetColor();

                            colors.Blue();

                            Console.Write("#");

                            colors.DarkPurple();
                            Console.WriteLine(System.Environment.UserName + " - " + System.Environment.MachineName);
                            Console.ResetColor();

                        break;

                        case "scan": 

                            Scan scan = new Scan();

                            long size = scan.Execute(args[0]);
                            string format = scan.FormatBytes(size);

                            Console.WriteLine(size + format);

                            Console.WriteLine();

                            Console.ResetColor();

                            colors.Blue();

                            Console.Write("#");

                            colors.DarkPurple();
                            Console.WriteLine(System.Environment.UserName + " - " + System.Environment.MachineName);
                            Console.ResetColor();

                            colors.Yellow();
                            Console.WriteLine("arguments used: <directory> <command>");

                        break;
                    
                        case "commands":

                            CommandsConfig commandsConfig = new CommandsConfig();
                            Console.Write(commandsConfig);

                        break;

                        case "list":

                            Contents list = new Contents();
                            list.Execute(args[0]);

                            colors.Yellow();
                            Console.WriteLine("arguments used: <directory> <command>");

                            Console.WriteLine();

                            Console.ResetColor();

                            colors.Blue();

                            Console.Write("#");

                            colors.DarkPurple();
                            Console.WriteLine(System.Environment.UserName + " - " + System.Environment.MachineName);
                            Console.ResetColor();

                        break;
                    }

                }
                catch (DirectoryNotFoundException)
                {

                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Directory not found!");
                    Console.ResetColor();
                    Console.WriteLine();

                    Console.ResetColor();

                    colors.Blue();

                    Console.Write("#");

                    colors.DarkPurple();
                    Console.WriteLine(System.Environment.UserName + " - " + System.Environment.MachineName);
                    Console.ResetColor();
                }

                catch (System.ComponentModel.Win32Exception )
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Path not found to open!" );
                    Console.ResetColor();
                    Console.WriteLine();

                    Console.ResetColor();

                    colors.Blue();

                    Console.Write("#");

                    colors.DarkPurple();
                    Console.WriteLine(System.Environment.UserName + " - " + System.Environment.MachineName);
                    Console.ResetColor();
                }
            }
        }
    }
}
