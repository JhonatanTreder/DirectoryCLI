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
                string cmd;

                Console.WriteLine();

                switch(args.Length)
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
                    if (CommandsConfig.IsValidDirectoryPath(directoryPath))
                    {
                        switch (cmd)
                        {
                            case "create-folder":

                                Colors colors = new Colors();
                                colors.Green();
                                Console.Write("#");

                                Console.ResetColor();

                                colors.DarkPurple();
                                Console.Write(System.Environment.UserName);

                                colors.Yellow();
                                Console.Write(" using arguments: <directory> <command> <conclusion>");

                                for (int i = 0; i <= args.Length - 3; i++)
                                {
                                    CreateFolder createFolder = new CreateFolder(args[2 + i]);
                                    createFolder.Execute(directoryPath);
                                }

                                Console.WriteLine();
                                Console.ResetColor();

                            break;

                            case "delete-folder":

                                colors = new Colors();
                                colors.Green();
                                Console.Write("#");

                                Console.ResetColor();

                                colors.DarkPurple();
                                Console.Write(System.Environment.UserName);

                                colors.Yellow();
                                Console.Write(" using arguments: <directory> <command> <conclusion>");

                                for (int i = 0; i <= args.Length - 3; i++)
                                {
                                    DeleteFolder deleteFolder = new DeleteFolder(args[2 + i]);
                                    deleteFolder.Execute(directoryPath);
                                }
                                Console.WriteLine();
                                Console.ResetColor();

                            break;

                            case "create-file":

                                colors = new Colors();
                                colors.Green();
                                Console.Write("#");

                                Console.ResetColor();

                                colors.DarkPurple();
                                Console.Write(System.Environment.UserName);

                                colors.Yellow();
                                Console.Write(" using arguments: <directory> <command> <conclusion>");

                                for (int i = 0; i <= args.Length - 3; i++)
                                {
                                    CreateFile createFile = new CreateFile(args[2 + i]);
                                    createFile.Execute(directoryPath);
                                }
                                Console.WriteLine();
                                Console.ResetColor();
                                

                            break;

                            case "delete-file":

                                colors = new Colors();
                                colors.Green();
                                Console.Write("#");

                                Console.ResetColor();

                                colors.DarkPurple();
                                Console.Write(System.Environment.UserName);

                                colors.Yellow();
                                Console.Write(" using arguments: <directory> <command> <conclusion>");

                                for (int i = 0; i <= args.Length - 3; i++)
                                {
                                    DeleteFile deleteFile = new DeleteFile(args[0], args[2 + i]);
                                    deleteFile.Execute(directoryPath);
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
                                Console.Write(System.Environment.UserName);

                                colors.Yellow();
                                Console.Write(" using arguments: <directory> <command> <conclusion>");

                                Open open = new Open(args[2]);
                                open.Execute(directoryPath);

                                Console.WriteLine();
                                Console.ResetColor();

                            break;
                        }
                    }

                    switch(cmd) 
                    {
                        case "open-site":

                            Colors colors = new Colors();

                            colors.Green();
                            Console.Write("#");

                            Console.ResetColor();

                            colors.DarkPurple();
                            Console.Write(System.Environment.UserName);
                            
                            colors.Yellow();
                            Console.Write(" using arguments: <command> <site/domain name>");

                            OpenSite openSite = new OpenSite();
                            openSite.Execute(args[1]);

                            Console.WriteLine();
                            Console.ResetColor();

                        break;
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
