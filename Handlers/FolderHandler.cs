using DirectoryCLI.Interfaces;

namespace DirectoryCLI.Handlers
{
    internal class FolderHandler : IFileSystemHandler
    {
        //Injeção de dependência por meio do construtor.

        ILogHandler LogHandler;
        //-----------------------------------------------------------------------------

        public FolderHandler(ICommandValidator commandValidator, ILogHandler logHandler)
        {
            LogHandler = logHandler;
        }
        //-----------------------------------------------------------------------

        //Comando 'create-folder'.
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

        //Comando 'delete-folder'.
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
                    if (arguments[2] == "-p")
                    {
                        string[] directories = Directory.GetDirectories(directoryPath.FullName);
                        int index = int.Parse(arguments[3 + i]);
                    }

                    if (Directory.Exists(fullPath))
                    {
                        Directory.Delete(fullPath, true);
                        deletedFolders.Add(fullPath);
                    }

                    else
                    {
                      nonExistingFolders.Add(fullPath);
                    }
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

        //Comando 'del-folders'.
        public void DeleteRecursive(string[] arguments)
        {

            string[] directories = Directory.GetDirectories(arguments[0]);

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

        public void DeleteInPosition(string[] arguments)
        {

        }
    }
}
