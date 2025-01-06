using DirectoryCLI.Exceptions;
using DirectoryCLI.Interfaces;
using Spectre.Console;

namespace DirectoryCLI.Handlers
{
    internal class FileHandler : IFileHandler
    {
        public void Create(string[] arguments)
        {
            ILogHandler logHandler = new LogHandler();

            HashSet<string> createdFiles = new HashSet<string>();
            HashSet<string> existingFiles = new HashSet<string>();
            HashSet<string> invalidFiles = new HashSet<string>();

            DirectoryInfo directoryPath = new DirectoryInfo(arguments[0]);

            for (int i = 2; i < arguments.Length; i++)
            {
                FileInfo fileName = new FileInfo(arguments[i]);

                string fullPath = Path.Combine(directoryPath.FullName, fileName.Name);

                if (!File.Exists(fullPath))
                {
                    if (Directory.Exists(fullPath))
                    {
                        invalidFiles.Add(fullPath);
                    }

                    else
                    {
                        File.Create(fullPath).Close();
                        createdFiles.Add(fullPath);
                    }
                }

                else
                {
                    existingFiles.Add(fullPath);
                }
            }

            logHandler.ShowResult("Arquivos criados", createdFiles);
            logHandler.ShowResult("Arquivos já existentes", existingFiles);
            logHandler.ShowResult("Pastas com o mesmo nome de arquivos", invalidFiles);
        }

        public void Delete(string[] arguments)
        {
            Console.WriteLine();

            ILogHandler logHandler = new LogHandler();

            HashSet<string> deletedFiles = new HashSet<string>();
            HashSet<string> nonExistingFiles = new HashSet<string>();
            HashSet<string> unauthorizedFiles = new HashSet<string>();
            DirectoryInfo directoryPath = new DirectoryInfo(arguments[0]);

            for (int i = 0; i <= arguments.Length - 3; i++)
            {
                string fullPath = Path.Combine(directoryPath.FullName, arguments[2 + i]);
                try  
                {

                    if (File.Exists(fullPath))
                    {
                        File.Delete(fullPath);
                        deletedFiles.Add(fullPath);
                    }

                    else nonExistingFiles.Add(fullPath);
                    
                }
                catch (UnauthorizedAccessException)
                {
                    unauthorizedFiles.Add(fullPath);
                }
            }

            logHandler.ShowResult(" Arquivos deletados", deletedFiles);
            logHandler.ShowResult(" Arquivos não encontrados", nonExistingFiles);
            logHandler.ShowResult(" Arquivos sem permissão para deleção", unauthorizedFiles);
        }

        public void DeleteAllFiles(string[] arguments)
        {

            ILogHandler logHandler = new LogHandler();
            HashSet<string> list = new HashSet<string>();
            string[] files = Directory.GetFiles(arguments[0]);

            switch (arguments.Length)
            {
                case 0:

                    Console.WriteLine("Nenhum arquivo para ser deletado.");

                    break;

                case 3:

                    string fullPath;
                    string parameter = arguments[2];

                    bool isNullParameter = parameter == "--null";

                    DirectoryInfo directoryPath = new DirectoryInfo(arguments[0]);

                    foreach (string file in files)
                    {
                        FileInfo archive = new FileInfo(file);

                        bool isNullOrEmpty = string.IsNullOrEmpty(archive.Extension);

                        if (isNullParameter == false)
                        {
                            string extensionFile = archive.Name.Replace(archive.Extension, parameter);
                            fullPath = Path.Combine(directoryPath.FullName, extensionFile);

                            if (File.Exists(fullPath))
                            {
                                File.Delete(fullPath);
                                list.Add(fullPath);
                            }
                        }

                        else
                        {
                            if (isNullOrEmpty)
                            {
                                fullPath = Path.Combine(directoryPath.FullName, archive.Name);

                                if (File.Exists(fullPath))
                                {
                                    File.Delete(fullPath);
                                    list.Add(fullPath);
                                }
                                else list.Add(fullPath);
                            }
                        }
                    }

                    if (list.Count != 0)
                    {
                        if (isNullParameter)
                        {
                            logHandler.ShowResult($"Arquivos sem extensão deletados", list);
                        }

                        else
                        {
                            logHandler.ShowResult($"Arquivos '{parameter}' deletados", list);
                        }
                    }

                    else
                    {

                        Table table = new Table();

                        if (isNullParameter)
                        {
                            table.AddColumn("Nenhum arquivo sem extensão encontrado");
                        }

                        else
                        {
                            table.AddColumn($"Nenhum arquivo com a extensão '{parameter}' encontrado.");
                        }

                        AnsiConsole.Write(table);
                        Console.WriteLine();
                    }

                    break;

                default:

                    foreach (string file in files)
                    {
                        list.Add(file);
                    }

                    if (list.Count != 0)
                    {
                        logHandler.ShowResult($"Arquivos deletados", list);
                    }

                    else
                    {
                        Console.WriteLine($"Nenhum arquivo encontrado");
                    }

                    foreach (string file in files)
                    {
                        File.Delete(file);
                    }

                    break;
            }
        }
    }
}
