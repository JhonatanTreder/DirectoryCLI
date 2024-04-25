using DirectoryCLI.Commands;
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

                        cmd = args[0];

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

                            
                            colors.Green();
                            Console.Write("#");

                            Console.ResetColor();

                            colors.DarkPurple();
                            Console.Write(System.Environment.UserName + " - " + System.Environment.MachineName + "/");

                            colors.Yellow();
                            Console.Write("using arguments: <directory> <command> <conclusion>");

                            Console.ResetColor();

                            Console.WriteLine();

                            for (int i = 0; i <= args.Length - 3; i++)
                            {
                                CreateFolder createFolder = new CreateFolder(args[2 + i]);
                                createFolder.Execute(directoryPath);
                                colors.Green();
                                Console.WriteLine($"Folder [{args[2 + i]}] created in [{args[0]}]");
                                Console.ResetColor();
                            }
                            Console.WriteLine();

                        break;

                        case "delete-folder":

                            colors = new Colors();
                            colors.Red();
                            Console.Write("#");

                            Console.ResetColor();

                            colors.DarkPurple();
                            Console.Write(System.Environment.UserName + " - " + System.Environment.MachineName + "/");

                            colors.Yellow();
                            Console.Write("using arguments: <directory> <command> <conclusion>");
                            Console.WriteLine();

                            for (int i = 0; i <= args.Length - 3; i++)
                            {
                                DeleteFolder deleteFolder = new DeleteFolder(args[2 + i]);
                                deleteFolder.Execute(directoryPath);
                                colors.Red();
                                Console.WriteLine();
                                Console.Write($"Folder [{args[2 + i]}] deleted in [{args[0]}]");
                                Console.ResetColor();
                            }
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.ResetColor();

                        break;

                        case "create-file":

                            colors = new Colors();
                            colors.Green();
                            Console.Write("#");

                            Console.ResetColor();

                            colors.DarkPurple();
                            Console.Write(System.Environment.UserName + " - " + System.Environment.MachineName + "/");

                            colors.Yellow();
                            Console.Write("using arguments: <directory> <command> <conclusion>");
                            Console.WriteLine();

                            for (int i = 0; i <= args.Length - 3; i++)
                            {
                                CreateFile createFile = new CreateFile(args[2 + i]);
                                createFile.Execute(directoryPath);
                                colors.Green();
                                Console.WriteLine();
                                Console.Write($"File [{args[2 + i]}] created in [{args[0]}]");
                                Console.ResetColor();
                            }
                            Console.WriteLine();
                            Console.ResetColor();


                        break;

                        case "delete-file":

                            colors = new Colors();
                            colors.Red();
                            Console.Write("#");

                            Console.ResetColor();

                            colors.DarkPurple();
                            Console.Write(System.Environment.UserName + " - " + System.Environment.MachineName + "/");

                            colors.Yellow();
                            Console.Write("using arguments: <directory> <command> <conclusion>");
                            Console.WriteLine();

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
                            Console.ResetColor();

                            break;

                        case "open":

                            colors = new Colors();
                            colors.Green();
                            Console.Write("#");

                            Console.ResetColor();

                            colors.DarkPurple();
                            Console.Write(System.Environment.UserName + " - " + System.Environment.MachineName + "/");

                            colors.Yellow();
                            Console.Write("using arguments: <directory> <command> <conclusion>");

                            Open open = new Open(args[2]);
                            open.Execute(directoryPath);

                            Console.WriteLine();
                            Console.ResetColor();

                        break;
                    }


                    switch (cmd)
                    {
                        case "open-site":

                            colors = new Colors();

                            colors.Green();
                            Console.Write("#");

                            Console.ResetColor();

                            colors.DarkPurple();
                            Console.Write(System.Environment.UserName + " - " + System.Environment.MachineName + "/");

                            colors.Yellow();
                            Console.Write("using arguments: <command> <site/domain name>");
                            Console.ResetColor();

                            OpenSite openSite = new OpenSite();
                            openSite.Execute(args[1]);

                            Console.WriteLine();
                            Console.ResetColor();

                        break;
                    }
                }
                catch (DirectoryNotFoundException)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Directory not found!");
                    Console.ResetColor();
                    Console.WriteLine();
                }

                catch (System.ComponentModel.Win32Exception )
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Path not found to open!" );
                    Console.ResetColor();
                    Console.WriteLine();
                }
            }
        }
    }
}
