using System;
using DirectoryCLI.Commands;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DirectoryCLI
{
    internal class Program
    {
        static void Main()
        {
            while(true)
            {
                string[] args = Console.ReadLine().Split(' ');
                string cmd = args[1];
                FileInfo directoryPath = new FileInfo(args[0]);

                if (Command.IsValidDirectoryPath(directoryPath))
                {
                    try
                    {
                        switch (cmd) 
                        {
                            case "--create-folder":
                                CreateFolder createFolder = new CreateFolder(args[2]);
                                createFolder.Execute(directoryPath);
                            break;

                            case "--create-file":
                                CreateFile createFile = new CreateFile(directoryPath);
                            break;
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }
    }
}
