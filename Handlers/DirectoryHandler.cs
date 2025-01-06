using DirectoryCLI.CommandStyles;
using DirectoryCLI.Exceptions;
using DirectoryCLI.Interfaces;
using Spectre.Console;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace DirectoryCLI.Handlers
{
    internal class DirectoryHandler : IDirectoryHandler
    {
        ICommandHelper CommandHelper;
        ICommandValidator CommandValidator;
        ILogHandler LogHandler;

        public DirectoryHandler(ICommandHelper commandHelper, ICommandValidator commandValidator, ILogHandler logHandler)
        {
            CommandHelper = commandHelper;
            CommandValidator = commandValidator;
            LogHandler = logHandler;
        }

        public void ListItems(string[] arguments)
        {
            Console.OutputEncoding = Encoding.UTF8;

            HashSet<string> extensionFiles = new HashSet<string>();

            if (CommandValidator.IsValidListCommand(arguments) == false)
            {
                return;
            }

            string[] files = Array.Empty<string>();
            try
            {
                files = Directory.GetFiles(arguments[0]);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Erro de permissão: " + ex.Message);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao acessar arquivos: " + ex.Message);
                return;
            }

            string extension;

            switch (arguments.Length)
            {
                case 4:

                    Table table = new Table();
                    DirectoryInfo directoryPath = new DirectoryInfo(arguments[0]);

                    extension = arguments[3];

                    table.AddColumns(Markup.Escape($"Arquivos {extension}"), "Caminho");

                    foreach (string file in files)
                    {
                        FileInfo archive = new FileInfo(file);

                        if (!string.IsNullOrEmpty(archive.Extension))
                        {
                            string extensionFile = archive.Name.Replace(archive.Extension, extension);
                            string fullPath = Path.Combine(directoryPath.FullName, extensionFile);

                            if (File.Exists(fullPath))
                            {
                                extensionFiles.Add(extensionFile);
                                table.AddRow(Markup.Escape(extensionFile), Markup.Escape(fullPath));
                            }
                        }
                    }

                    if (extensionFiles.Count == 0)
                    {
                        Console.WriteLine($"Nenhum arquivo com a extensão '{extension}' encontrado.");
                    }
                    else
                    {
                        AnsiConsole.Write(table);
                    }
                    break;

                default:

                    var directories = Array.Empty<string>();

                    try
                    {
                        directories = Directory.GetDirectories(arguments[0]);
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        Console.WriteLine("Erro de permissão: " + ex.Message);
                        return;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro ao acessar diretórios: " + ex.Message);
                        return;
                    }

                    Colors.WhiteText();

                    if (directories.Length == 0)
                    {
                        var folderTable = new Table();
                        folderTable.AddColumn($"Nenhuma pasta encontrada");
                        AnsiConsole.Write(folderTable);
                    }
                    else
                    {
                        var folderTable = new Table()
                        .AddColumns("Posição", $"Total de pastas: {directories.Length}", "Data de criação", "Caminho").Width(105);

                        for (int i = 0; i < directories.Length; i++)
                        {
                            var dirInfo = new DirectoryInfo(directories[i]);

                            string creationTime;
                            try
                            {
                                creationTime = dirInfo.CreationTime.ToString();
                            }
                            catch (PlatformNotSupportedException)
                            {
                                creationTime = "N/A";
                            }

                            string dirName = CommandHelper.Truncate(Path.GetFileName(directories[i]), 30);
                            string dirPath = CommandHelper.Truncate(dirInfo.FullName, 30);

                            folderTable.AddRow($"{i}", Markup.Escape(dirName), creationTime, Markup.Escape(dirPath));
                        }

                        AnsiConsole.Write(folderTable);
                    }

                    if (files.Length == 0)
                    {
                        var fileTable = new Table();
                        fileTable.AddColumn($"Nenhum arquivo encontrado");
                        AnsiConsole.Write(fileTable);
                    }
                    else
                    {
                        var fileTable = new Table()
                        .AddColumns("Posição", $"Total de arquivos: {files.Length}", "Extensão", "Caminho").Width(115);

                        for (int i = 0; i < files.Length; i++)
                        {
                            var fileInfo = new FileInfo(files[i]);
                            extension = string.IsNullOrEmpty(fileInfo.Extension) ? "NONE" : fileInfo.Extension;

                            string fileName = CommandHelper.Truncate(Path.GetFileName(files[i]), 30);
                            string filePath = CommandHelper.Truncate(fileInfo.FullName, 30);

                            fileTable.AddRow($"{i}", Markup.Escape(fileName), extension, Markup.Escape(filePath));
                        }

                        AnsiConsole.Write(fileTable);
                    }

                    break;
            }

            Console.WriteLine();
            Colors.WhiteText();
        }


        public void ZipItem(string[] arguments)
        {

            DirectoryInfo directoryPath = new DirectoryInfo(arguments[0]);
            FileInfo destiny = new FileInfo(arguments[arguments.Length - 1]);

            HashSet<string> items = new HashSet<string>();
            HashSet<string> zipFiles = new HashSet<string>();
            HashSet<string> nonExistingItems = new HashSet<string>();

            if (CommandValidator.IsValidZipCommand(arguments) == false)
            {
                return;
            }

            for (int i = 2; i < arguments.Length - 2; i++)
            {
                FileInfo item = new FileInfo(arguments[i]);

                string itemPath = Path.Combine(directoryPath.FullName, item.Name);

                if (File.Exists(itemPath) && item.Extension == ".zip")
                {
                    zipFiles.Add($" Arquivo: '{itemPath}'");
                    continue;
                }

                else
                {
                    string destinyPath = Path.Combine(directoryPath.FullName, destiny.Name);

                    bool itemToZip = Directory.Exists(itemPath) || File.Exists(itemPath);
                    bool zipFile = File.Exists(destinyPath);

                    //-------------------------------------------------------------------------------------------------------------------------------------------------

                    if (itemToZip == true)
                    {
                        using (ZipArchive archive = ZipFile.Open(destinyPath, ZipArchiveMode.Update))
                        {

                            if (File.Exists(itemPath) && zipFile)
                            {
                                archive.CreateEntryFromFile(itemPath, item.Name, CompressionLevel.Optimal);

                                items.Add($"Arquivo: '{itemPath}'");
                            }

                            else if (Directory.Exists(itemPath) && zipFile)
                            {
                                AddDirectoryToZip(archive, itemPath, item.Name);

                                items.Add($"Pasta: '{itemPath}'");
                            }

                            archive.Dispose();
                        }
                    }

                    else
                    {
                        nonExistingItems.Add(itemPath);
                    }
                    //------------------------------------------------------------------------
                }
            }

            LogHandler.ShowResult(" Itens zipados", items);
            LogHandler.ShowResult(" Itens não existentes", nonExistingItems);
            LogHandler.ShowResult(" Arquivos não zipáveis", zipFiles);
        }

        private void AddDirectoryToZip(ZipArchive archive, string sourceDir, string entryName)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(sourceDir);

            if (dirInfo.GetFiles().Length == 0 && dirInfo.GetDirectories().Length == 0)
            {
                archive.CreateEntry($"{entryName}/");
            }

            foreach (FileInfo file in dirInfo.GetFiles())
            {
                string entryPath = Path.Combine(entryName, file.Name);
                archive.CreateEntryFromFile(file.FullName, entryPath);
            }

            foreach (DirectoryInfo subDir in dirInfo.GetDirectories())
            {
                string subDirEntryName = Path.Combine(entryName, subDir.Name);
                AddDirectoryToZip(archive, subDir.FullName, subDirEntryName);
            }
        }

        public void ExtractZipFile(string[] arguments)
        {
            if (CommandValidator.IsValidExtractCommand(arguments) == false) return;

            DirectoryInfo directoryPath = new DirectoryInfo(arguments[0]);

            HashSet<string> zipFiles = new HashSet<string>();
            HashSet<string> zipFolders = new HashSet<string>();
            HashSet<string> filesWithOtherExtension = new HashSet<string>();
            HashSet<string> existingFiles = new HashSet<string>();
            HashSet<string> existingFolders = new HashSet<string>();

            string destinyPath;
            string parameterTo = arguments[arguments.Length - 2];

            if (parameterTo == "to")
            {
                FileInfo destiny = new FileInfo(arguments[arguments.Length - 1]);
                destinyPath = Path.Combine(directoryPath.FullName, destiny.Name);
            }

            else if (parameterTo == "to-new")
            {

                FileInfo destiny = new FileInfo(arguments[arguments.Length - 1]);
                destinyPath = Path.Combine(directoryPath.FullName, destiny.Name);

                Directory.CreateDirectory(destinyPath);
            }

            else if (arguments[arguments.Length - 1] == "to-here")
            {
                destinyPath = directoryPath.FullName;
            }

            else
            {
                Console.WriteLine("Comando inválido.");
                return;
            }

            for (int i = 0; i < arguments.Length; i++)
            {
                if (i < arguments.Length)
                {
                    FileInfo item = new FileInfo(arguments[2 + i]);

                    if (item.Extension == ".zip")
                    {

                        string itemPath = Path.Combine(directoryPath.FullName, item.Name);

                        using (ZipArchive archive = ZipFile.OpenRead(itemPath))
                        {
                            foreach (ZipArchiveEntry entry in archive.Entries)
                            {
                                string finalPath = Path.Combine(destinyPath, entry.FullName);

                                if (entry.FullName.EndsWith("/"))
                                {

                                    if (!Directory.Exists(finalPath))
                                    {
                                        Directory.CreateDirectory(finalPath);
                                        zipFolders.Add(finalPath);
                                    }

                                    else existingFolders.Add(finalPath);

                                }

                                else
                                {

                                    if (!File.Exists(finalPath))
                                    {
                                        entry.ExtractToFile(finalPath);
                                        zipFiles.Add(finalPath);
                                    }

                                    else existingFiles.Add(finalPath);

                                }
                            }

                            Console.WriteLine("Extração concluída.");
                            break;
                        }
                    }

                    else filesWithOtherExtension.Add(item.FullName);
                }
            }

            LogHandler.ShowResult("Diretórios extraídos ", zipFolders);
            LogHandler.ShowResult("Diretórios já existentes", existingFolders);
            LogHandler.ShowResult("Arquivos extraídos", zipFiles);
            LogHandler.ShowResult("Arquivos já existentes", existingFiles);
            LogHandler.ShowResult("Arquivos sem a extensão '.zip' ", filesWithOtherExtension);
        }

        public void ScanSize(string directory)
        {
            Storage(directory);
            Console.WriteLine();
            Colors.WhiteText();
        }
        //-------------------------------------------------------------

        private void Storage(string directory)
        {
            long totalSize = GetDirectorySize(directory);

            Table table = new Table()

                .AddColumns($"Diretório", "Tamanho total do diretório")
                .AddRow($"{Markup.Escape(directory)}", $"{CommandHelper.FormatBytes(totalSize)}");

            AnsiConsole.Write(table);
        }
        //----------------------------------------------------------

        private long GetDirectorySize(string directory)
        {
            long size = 0; ;
            try
            {
                foreach (string file in Directory.EnumerateFiles(directory))
                {
                    Table table = new Table();
                    table.AddColumns("Item", "Status");

                    string status;
                    string item = file;

                    try
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        size += fileInfo.Length;
                        status = "Success";
                    }

                    catch (UnauthorizedAccessException)
                    {
                        status = "Failure";
                        Console.WriteLine($"Sem permissão para acessar o arquivo: {file}");
                    }

                    table.AddRow(Markup.Escape(item), status);

                    AnsiConsole.Write(table);
                }
            }

            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"Sem permissão para acessar o diretório: {directory}");
                return size;
            }

            try
            {
                foreach (string dir in Directory.EnumerateDirectories(directory))
                {
                    size += GetDirectorySize(dir);
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"Sem permissão para acessar o diretório: {directory}");
            }

            return size;
        }

        //---------------------------------------------

        public void MoveItem(string[] arguments)
        {

            DirectoryInfo directoryPath = new DirectoryInfo(arguments[0]);
            DirectoryInfo finalDestiny = new DirectoryInfo(Path.Combine(directoryPath.FullName, arguments[arguments.Length - 1]));

            HashSet<string> movedFiles = new HashSet<string>();
            HashSet<string> movedFolders = new HashSet<string>();
            HashSet<string> existingFiles = new HashSet<string>();
            HashSet<string> existingFolders = new HashSet<string>();
            HashSet<string> nonExistingItems = new HashSet<string>();

            string finalPath = CommandHelper.Truncate(finalDestiny.FullName, 90);

            Table dirTable = new Table().AddColumns($"Caminho:  {Markup.Escape(finalPath)}");

            if (arguments[arguments.Length - 2] == "to-new")
            {
                if (!Directory.Exists(finalDestiny.FullName))
                {
                    Directory.CreateDirectory(finalDestiny.FullName);
                }

                else
                {
                    Console.WriteLine($"O diretório '{finalDestiny}' já existe.");
                    return;
                }
            }

            else if (arguments[arguments.Length - 2] == "to")
            {

                if (!Directory.Exists(finalDestiny.FullName))
                {
                    Console.WriteLine($"O diretório de destino '{finalDestiny}' não existe.");
                    return;
                }
            }

            else
            {
                Console.WriteLine($"  Argumento inválido '{arguments[arguments.Length - 2]}' inválido.");
                Console.WriteLine("  Soluções:");
                Console.WriteLine("  *Use 'to' para mover os itens para um diretório já existente.");
                Console.WriteLine("  *Use 'to-new' para criar um diretório e mover os itens para ele.");
                Console.WriteLine();
                return;
            }

            for (int i = 2; i < arguments.Length - 2; i++)
            {
                string itemPath = Path.Combine(directoryPath.FullName, arguments[i]);

                if (File.Exists(itemPath))
                {
                    string finalItemPath = Path.Combine(finalDestiny.FullName, arguments[i]);

                    if (!File.Exists(finalItemPath))
                    {
                        File.Move(itemPath, finalItemPath);
                        movedFiles.Add(finalItemPath);
                    }

                    else
                    {
                        nonExistingItems.Add(finalItemPath);
                    }
                }

                else if (Directory.Exists(itemPath))
                {
                    string finalItemPath = Path.Combine(finalDestiny.FullName, new DirectoryInfo(itemPath).Name);

                    if (!Directory.Exists(finalItemPath))
                    {
                        Directory.Move(itemPath, finalItemPath);
                        movedFolders.Add(finalItemPath);
                    }

                    else
                    {
                        nonExistingItems.Add(finalItemPath);
                    }
                }

                else
                {
                    nonExistingItems.Add(itemPath);
                }
            }

            LogHandler.ShowResult("Pastas movidas", movedFolders);
            LogHandler.ShowResult("Pastas já existentes", existingFolders);
            LogHandler.ShowResult("Arquivos movidos", movedFiles);
            LogHandler.ShowResult("Arquivos já existentes", existingFiles);
            LogHandler.ShowResult("Itens não existentes", nonExistingItems);
        }


        //------------------------------------------------------------------------
        //OPEN
        public void Open(string[] arguments)
        {

            DirectoryInfo directoryPath = new DirectoryInfo(arguments[0]);

            switch (arguments.Length)
            {
                case 3:

                    if (arguments[2] == "--this")
                    {
                        if (File.Exists(directoryPath.FullName))
                        {
                            Console.WriteLine($"Abrindo o arquivo '{directoryPath.FullName}'");
                            Console.WriteLine();
                            ExecuteProcess(directoryPath.FullName);

                            break;
                        }

                        else
                        {
                            Console.WriteLine($"Abrindo o diretório '{directoryPath.FullName}'");
                            Console.WriteLine();
                            ExecuteProcess(directoryPath.FullName);
                            break;
                        }
                    }

                    else
                    {
                        string itemPath = Path.Combine(directoryPath.FullName, arguments[2]);

                        bool isFile = File.Exists(itemPath);
                        bool isDirectory = Directory.Exists(itemPath);
                        bool itemExists = File.Exists(itemPath) || Directory.Exists(itemPath);

                        if (!itemExists)
                        {
                            Console.WriteLine($"Item '{itemPath}' inexistente.");
                            return;
                        }

                        if (isFile)
                        {
                            Console.WriteLine($"Abrindo o arquivo '{arguments[2]}'");
                            Console.WriteLine();
                            ExecuteProcess(itemPath);
                            break;
                        }

                        else if (isDirectory)
                        {
                            Console.WriteLine($"Abrindo o diretório '{arguments[2]}'");
                            Console.WriteLine();
                            ExecuteProcess(itemPath);
                            break;
                        }
                    }
                    break;

                case 4:


                    string parameter = arguments[2];

                    if (CommandValidator.IsValidOpenCommand(parameter) == false) break;

                    int index = int.Parse(arguments[3]);

                    if (parameter == "-d")
                    {
                        string[] directories = Directory.GetDirectories(directoryPath.FullName);

                        if (directories.Length < index || index < 0)
                        {
                            Console.WriteLine($"A pasta na posição '{index}' não existe ");
                            Console.WriteLine();
                            break;
                        }

                        string directory = directories[index];

                        Console.WriteLine($"Abrindo o diretório {directory}");
                        Console.WriteLine();

                        ExecuteProcess(directory);
                        break;
                    }

                    if (parameter == "-f")
                    {
                        string[] files = Directory.GetFiles(directoryPath.FullName);

                        if (files.Length < index || index < 0)
                        {
                            Console.WriteLine($"O arquivo na posição '{index}' não existe ");
                            Console.WriteLine();
                            break;
                        }

                        string file = files[index];

                        Console.WriteLine($"Abrindo o arquivo '{file}'");
                        Console.WriteLine();

                        ExecuteProcess(file);
                        break;
                    }


                    break;
            }
        }

        //-----------------------------------------------------------------
        private void ExecuteProcess(string path)
        {
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    Process.Start(new ProcessStartInfo("explorer", $"\"{path}\"") { UseShellExecute = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", path);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", path);
                }
                else
                {
                    Console.WriteLine("Sistema operacional não suportado.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao abrir o caminho: {ex.Message}");
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------

        public void Rename(string[] arguments)
        {
            ICommandValidator commandValidator = new CommandValidator();
            DirectoryInfo directoryPath = new DirectoryInfo(arguments[0]);

            string namePath = Path.Combine(directoryPath.FullName, arguments[2]);
            string finalNamePath = Path.Combine(directoryPath.FullName, arguments[4]);

            if (commandValidator.IsValidRenameCommand(arguments) == true)
            {
                if (Directory.Exists(namePath))
                {
                    Directory.Move(namePath, finalNamePath);
                }

                else if (File.Exists(namePath))
                {
                    File.Move(namePath, finalNamePath);
                }
            }
        }
    }
}
