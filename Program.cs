using DirectoryCLI.Commands;
using DirectoryCLI.Exceptions;
using System;
using System.IO;

namespace DirectoryCLI
{
    internal class Program
    {
        static void Main()
        {
            while (true)
            {
                string[] args = Console.ReadLine().Split(' ');

                Console.WriteLine();
                string cmd = args[1];
                FileInfo directoryPath = new FileInfo(args[0]);



                try
                {
                    if (CommandsConfig.IsValidDirectoryPath(directoryPath))
                    {
                        switch (cmd)
                        {
                            case "create-folder":

                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("using arguments: <directory> <command> <conclusion>");

                                for (int i = 0; i <= args.Length - 3; i++)
                                {
                                    CreateFolder createFolder = new CreateFolder(args[2 + i]);
                                    createFolder.Execute(directoryPath);
                                }

                                Console.WriteLine();
                                Console.ResetColor();

                            break;

                            case "delete-folder":

                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("using arguments: <directory> <command> <conclusion>");

                                for (int i = 0; i <= args.Length - 3; i++)
                                {
                                    DeleteFolder deleteFolder = new DeleteFolder(args[2 + i]);
                                    deleteFolder.Execute(directoryPath);
                                }
                                Console.WriteLine();
                                Console.ResetColor();

                            break;

                            case "create-file":

                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("using arguments: <directory> <command> <conclusion>");

                                for (int i = 0; i <= args.Length - 3; i++)
                                {
                                    CreateFile createFile = new CreateFile(args[2 + i]);
                                    createFile.Execute(directoryPath);
                                }
                                Console.WriteLine();
                                Console.ResetColor();

                            break;

                            case "delete-file":

                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("using arguments: <directory> <command> <conclusion>");

                                for (int i = 0; i <= args.Length - 3; i++)
                                {
                                    DeleteFile deleteFile = new DeleteFile(args[0], args[2 + i]);
                                    deleteFile.Execute(directoryPath);
                                }
                                Console.WriteLine();
                                Console.ResetColor();

                            break;
                        }
                    }
                }
                catch (DirectoryException ex)
                {
                    Console.WriteLine("An error as ocurred: " + ex.Message);
                }
            }
        }
    }
}
