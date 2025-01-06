using DirectoryCLI.Interfaces;

namespace DirectoryCLI.Handlers
{
    internal class FolderHandler : IFolderHandler
    {
        ICommandValidator CommandValidator;
        ILogHandler LogHandler;

        public FolderHandler(ICommandValidator commandValidator, ILogHandler logHandler)
        {
            CommandValidator = commandValidator;
            LogHandler = logHandler;
        }

        public void Create(string[] arguments)
        {

            HashSet<string> folders = new HashSet<string>();
            HashSet<string> existingFile = new HashSet<string>();
            HashSet<string> existingFolder = new HashSet<string>();

            DirectoryInfo directoryPath = new DirectoryInfo(arguments[0]);

            for (int i = 0; i <= arguments.Length - 3; i++)
            {
                string fullPath = Path.Combine(directoryPath.FullName, arguments[2 + i]);

                if (Directory.Exists(fullPath))
                {
                    existingFolder.Add(fullPath);
                }

                else if (File.Exists(fullPath))
                {
                    existingFile.Add(fullPath);
                }

                else
                {
                    Directory.CreateDirectory(fullPath);
                    folders.Add(fullPath);
                }
            }

            LogHandler.ShowResult($" Pastas criadas", folders);
            LogHandler.ShowResult($" Pastas já existentes", existingFolder);
            LogHandler.ShowResult($" Arquivos com o mesmo nome de pastas", existingFile);
        }

        public void Delete(string[] arguments)
        {

            DirectoryInfo directoryPath = new DirectoryInfo(arguments[0]);

            HashSet<string> deletedFolders = new HashSet<string>();
            HashSet<string> nonExistingFolders = new HashSet<string>();
            HashSet<string> unauthorizedFolders = new HashSet<string>();

            for (int i = 0; i <= arguments.Length - 3; i++)
            {
                string fullPath = Path.Combine(directoryPath.FullName, arguments[2 + i]);

                try
                {
                    if (Directory.Exists(fullPath))
                    {
                        Directory.Delete(fullPath, true);
                        deletedFolders.Add(fullPath);
                    }

                    else
                    {
                        throw new DirectoryNotFoundException();
                    }
                }

                catch (DirectoryNotFoundException)
                {
                    nonExistingFolders.Add(fullPath);
                }
                catch (UnauthorizedAccessException)
                {
                    unauthorizedFolders.Add(fullPath);
                }
            }

            LogHandler.ShowResult("Pastas deletadas", deletedFolders);
            LogHandler.ShowResult("Pastas não encontradas", nonExistingFolders);
            LogHandler.ShowResult("Pastas com problemas de permissão", unauthorizedFolders);
        }

        public void DeleteSubdirectories(string directory)
        {

            string[] directories = Directory.GetDirectories(directory);

            HashSet<string> folders = new HashSet<string>();
            HashSet<string> unauthorizedFolders = new HashSet<string>();

            if (directories.Length != 0)
            {
                foreach (string dir in directories)
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(dir);

                    try
                    {
                        Directory.Delete(dir, true);
                        folders.Add(dir);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        unauthorizedFolders.Add(dir);
                    }
                }

                LogHandler.ShowResult(" Pastas deletadas", folders);
                LogHandler.ShowResult(" Pastas sem permissão para deleção", unauthorizedFolders);
            }

            else
            {
                Console.WriteLine("Não existe pastas para serem deletadas neste diretório");
                Console.WriteLine();
            }
        }
    }
}
