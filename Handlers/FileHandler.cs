using DirectoryCLI.Interfaces;
using Spectre.Console;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DirectoryCLI.Handlers
{
    internal class FileHandler : IFileSystemHandler
    {
        //Injeção de dependência por do construtor.

        readonly ILogHandler LogHandler;
        //----------------------------------------
        public FileHandler(ILogHandler logHandler)
        {
            LogHandler = logHandler;
        }
        //-----------------------------------------------------------------------

        //Comando 'create-file'
        public void Create(string[] arguments)
        {

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

            LogHandler.ShowResult("Arquivos criados", createdFiles);
            LogHandler.ShowResult("Arquivos já existentes", existingFiles);
            LogHandler.ShowResult("Pastas com o mesmo nome de arquivos", invalidFiles);
        }

        //Comando 'delete-file'.
        public void Delete(string[] arguments)
        {
            Console.WriteLine();

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

            LogHandler.ShowResult(" Arquivos deletados", deletedFiles);
            LogHandler.ShowResult(" Arquivos não encontrados", nonExistingFiles);
            LogHandler.ShowResult(" Arquivos sem permissão para deleção", unauthorizedFiles);
        }

        //Comando 'del-files'.
        public void DeleteRecursive(string[] arguments)
        {
            HashSet<string> archives = new HashSet<string>();
            string[] files = Directory.GetFiles(arguments[0]);

            switch (arguments.Length)
            {

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
                                archives.Add(fullPath);
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
                                    archives.Add(fullPath);
                                }

                                else archives.Add(fullPath);
                            }
                        }
                    }

                    if (archives.Count != 0)
                    {
                        if (isNullParameter)
                        {
                            LogHandler.ShowResult($"Arquivos sem extensão deletados", archives);
                        }

                        else
                        {
                            LogHandler.ShowResult($"Arquivos '{parameter}' deletados", archives);
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
                        archives.Add(file);
                    }

                    if (archives.Count != 0)
                    {
                        LogHandler.ShowResult($"Arquivos deletados", archives);
                    }

                    else
                    {
                        Console.WriteLine("Não existe arquivos para serem deletados neste diretório.");
                        Console.WriteLine();
                    }

                    foreach (string file in files)
                    {
                        File.Delete(file);
                    }

                    break;
            }
        }

        public void DeleteInPosition(string[] arguments)
        {

        }
    }
}
