using DirectoryCLI.Commands;
using DirectoryCLI.CommandStyles;
using DirectoryCLI.Exceptions;
using System;
using System.Drawing;
using Spectre.Console.Cli;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Xml.Linq;

namespace DirectoryCLI
{
    internal class Program
    {//----------------------------------------------------------------------------------------

        static void Main()
        {
            Colors.BlackBG();
            Console.Clear();

            //Classe para formatar a CLI no final de cada comando
            FormatLogs formatLogs = new FormatLogs();
            formatLogs.UserAndMachineName();

            //----------------------------------------------------------------------------------------
            //Uso do "while" para deixar o programa rodando em looping

            while (true)
            {
                //----------------------------------------------------------------------------------------

                formatLogs = new FormatLogs();

                //Classe para facilitar o acesso às cores
                Colors colors = new Colors();
                Colors.BlackBG();

                string[] arguments = Console.ReadLine().Split(' ');
                string command;

                //----------------------------------------------------------------------------------------
                //Switch para atribuir qual é o comando para a variável "cmd"

                switch (arguments.Length)
                {
                    case 2:

                        if (arguments[1] == "scan" || arguments[1] == "list")
                        {
                            command = arguments[1];
                            Console.WriteLine();
                        }
                        else
                        {
                            command = arguments[0];
                            Console.WriteLine();
                        }

                    break;

                    case 1:

                        command = arguments[0];
                        Console.WriteLine();

                    break;

                    default:

                        if (arguments[0] == "ct")
                        {
                            command = arguments[0];
                            Console.WriteLine();
                        }

                        else
                        {
                            command = arguments[1];
                            Console.WriteLine();
                        }
                    break;

                }

                try
                {
                    //----------------------------------------------------------------------------------------
                    //Verificação dos argumentos fornecidos
                    CommandsConfig.ArgumentsValidation(arguments);
                    //----------------------------------------------------------------------------------------

                    //----------------------------------------------------------------------------------------
                    //Verificação do comando fornecido
                    CommandsConfig.CommandValidation(arguments);
                    //----------------------------------------------------------------------------------------

                    //Essa variável do tipo "FileInfo" será usada apenas nos comandos que usam diretórios
                    FileInfo directoryPath = new FileInfo(arguments[0]);

                    //Switch uado para identificar qual é o comando e continuar dependendo de qual comando seja
                    switch (command)
                    {
                        //----------------------------------------------------------------------------------------

                        //CREATE-FOLDER
                        case "create-folder":

                            for (int i = 0; i <= arguments.Length - 3; i++)
                            {
                                try
                                {
                                    bool directoryExist = !Directory.Exists(Path.Combine(directoryPath.FullName, arguments[2 + i]));

                                    if (directoryExist)
                                    {
                                        CreateFolder createFolder = new CreateFolder(arguments[2 + i]);

                                        createFolder.Execute(directoryPath);
                                        colors.Green();

                                        Console.WriteLine($"Folder [{arguments[2 + i]}] created in [{arguments[0]}].");
                                    }

                                    else
                                    {
                                        throw new IOException();
                                    }
                                }

                                //Coloquei a captura de exceção aqui para facilitar a busca pelo item já existente
                                catch (IOException)
                                {
                                    IOException ex = new IOException("Unable to create a folder: ");

                                    colors.DarkRed();

                                    Console.Write(ex.Message);

                                    colors.Red();

                                    if (File.Exists(Path.Combine(directoryPath.FullName, arguments[2 + i])))
                                    {
                                        Console.WriteLine($"This directory has a file without extension.");
                                        Colors.WhiteText();
                                    }

                                    else
                                    {
                                        Console.WriteLine($"The '{arguments[2 + i]}' folder already exists in this directory.");
                                        Colors.WhiteText();
                                    }
                                }
                            }

                            LogAndReset(command);

                        break;

                        //DELETE-FOLDER
                        case "delete-folder":

                            for (int i = 0; i <= arguments.Length - 3; i++)
                            {
                                try
                                {
                                    if (Directory.Exists(Path.Combine(directoryPath.FullName, arguments[2 + i])))
                                    {
                                        DeleteFolder deleteFolder = new DeleteFolder(arguments[2 + i]);

                                        deleteFolder.Execute(directoryPath);
                                        colors.Red();

                                        Console.WriteLine($"Folder [{arguments[2 + i]}] deleted in [{arguments[0]}]");
                                    }

                                    else
                                    {
                                        throw new ItemNotFoundException("Unable to delete a folder: ");
                                    }
                                }

                                catch (ItemNotFoundException ex)
                                {
                                    colors.DarkRed();

                                    Console.Write(ex.Message);

                                    colors.Red();

                                    Console.WriteLine($"The '{arguments[2 + i]}' folder does not exist in this directory.");
                                    Colors.WhiteText();
                                }
                            }

                            LogAndReset(command);

                        break;

                        //CREATE-FILE
                        case "create-file":

                            for (int i = 0; i <= arguments.Length - 3; i++)
                            {

                                try
                                {

                                    bool fileExist = !File.Exists(Path.Combine(directoryPath.FullName, arguments[2 + i]));

                                    if (fileExist)
                                    {

                                        CreateFile createFile = new CreateFile();
                                        FileInfo archiveName = new FileInfo(arguments[2 + i]);

                                        //Variável para identificar se existe uma pasta com o mesmo nome do arquivo sem extensão
                                        bool folderWithTheSameName = Directory.Exists(Path.Combine(directoryPath.FullName, archiveName.Name));

                                        //Verifica se um arquivo não possui uma extensão
                                        if (string.IsNullOrEmpty(archiveName.Extension) && folderWithTheSameName == true)
                                        {
                                            throw new FileWithoutExtensionException("Unable to create a file: ");
                                        }

                                        //Executa normalmente
                                        createFile.Execute(directoryPath, archiveName);
                                        colors.Green();

                                        Console.WriteLine($"File [{arguments[2 + i]}] created in [{arguments[0]}].");
                                        Colors.WhiteText();
                                    }

                                    else
                                    {
                                        throw new IOException();
                                    }
                                }
                                catch (IOException)
                                {
                                    IOException ex = new IOException("Unable to create a file: ");
                                    colors.DarkRed();

                                    Console.Write(ex.Message);

                                    Colors.WhiteText();

                                    Console.WriteLine($"The file '{arguments[2 + i]}' already exists in this directory.");

                                    Colors.WhiteText();

                                }

                                catch (FileWithoutExtensionException ex)
                                {

                                    colors.DarkRed();

                                    Console.Write(ex.Message);

                                    colors.Red();

                                    Console.WriteLine($"This directory already has a folder with that name.");
                                    Colors.WhiteText();
                                }
                            }

                            LogAndReset(command);

                        break;

                        //DELETE-FILE
                        case "delete-file":

                            for (int i = 0; i <= arguments.Length - 3; i++)
                            {
                                try
                                {
                                    if (File.Exists(Path.Combine(directoryPath.FullName, arguments[2 + i])))
                                    {
                                        DeleteFile deleteFile = new DeleteFile(arguments[2 + i]);

                                        deleteFile.Execute(directoryPath);
                                        colors.Red();

                                        Console.WriteLine($"File [{arguments[2 + i]}] deleted in [{arguments[0]}]");
                                    }
                                    else
                                    {
                                        throw new ItemNotFoundException("Unable to delete a file: ");
                                    }
                                }

                                catch (ItemNotFoundException ex)
                                {
                                    colors.DarkRed();

                                    Console.Write(ex.Message);

                                    colors.Red();

                                    Console.WriteLine($"The '{arguments[2 + i]}' file does not exist in this directory.");
                                    Colors.WhiteText();
                                }
                            }

                            LogAndReset(command);

                        break;

                        //OPEN
                        case "open":
                            //----------------------------------------------------------------------------------------
                            //Algoritmo para identificar se existe um diretório com pastas ou arquivos 

                            //Apenas diminui as verificações em duas variávies: "folderExist" e "archiveExist"
                            FileInfo item = new FileInfo(Path.Combine(arguments[2]));

                            bool folderExist = Directory.Exists(arguments[0]) && Directory.Exists(Path.Combine(arguments[0], arguments[2]));
                            bool archiveExist = Directory.Exists(arguments[0]) && File.Exists(Path.Combine(arguments[0], arguments[2]));

                            //Variável booleana para saber se o arquivo expecificado termina com uma extensão que não é nula.
                            bool _NonExistentFile = !string.IsNullOrEmpty(item.Extension) && item.Name.EndsWith(item.Extension);

                            if (folderExist || archiveExist)
                            {
                                Open open = new Open(arguments[2]);
                                
                                open.Execute(directoryPath);

                                LogAndReset(command);
                            }
                            //----------------------------------------------------------------------------------------

                            //Lança uma exceção personalizada se isso não ocorrer

                            else if (_NonExistentFile)
                            {
                                throw new OpenCommandException("Erro ao abrir um arquivo: ");
                            }

                            else
                            {
                                throw new OpenCommandException("Erro ao abrir uma pasta: ");
                            }
                            //----------------------------------------------------------------------------------------
                            break;

                        //OPEN-SITE
                        case "open-site":

                            OpenSite openSite = new OpenSite();

                            formatLogs.SiteLog();
                            openSite.Execute(arguments[1]);

                            Console.WriteLine();

                            formatLogs.UserAndMachineName();

                        break;

                        //SCAN
                        case "scan":

                            Scan scan = new Scan();

                            long size = scan.Execute(arguments[0]);
                            string formatedBytes = scan.FormatBytes(size);

                            Console.Write("Size: ");

                            colors.DarkGray();

                            Console.WriteLine(size + formatedBytes);
                            Colors.WhiteText();
                            Console.WriteLine();

                            LogAndReset(command);

                        break;

                        //COMMANDS
                        case "commands":

                            CommandsInfo commandInfo = new CommandsInfo();

                            Console.WriteLine();
                            commandInfo.Execute();

                            LogAndReset(command);

                        break;

                        //CMD-SINTAXE
                        case "commands-sintaxe":

                            commandInfo = new CommandsInfo();

                            Console.WriteLine();
                            commandInfo.CommandSintaxe();

                            LogAndReset(command);

                        break;

                        //LIST
                        case "list":

                            DirectoryList list = new DirectoryList();

                            list.Execute(arguments[0]);

                            LogAndReset(command);

                        break;

                        //MOVE
                        case "move":

                            Move move = new Move();
                            List<string> items = new List<string>();

                            for (int i = 2; i < arguments.Length - 2; i++)
                            {
                                try
                                {
                                    item = new FileInfo(arguments[i]);
                                    FileInfo destiny = new FileInfo(arguments[arguments.Length - 1]);

                                    string destinyPath = Path.Combine(directoryPath.FullName, destiny.Name);
                                    string itemPath = Path.Combine(directoryPath.FullName, item.Name);

                                    //Variável para identificar se o usuário digitou outra coisa além de 'to'.
                                    bool userArgument = arguments[arguments.Length - 1] != "to";

                                    if (userArgument && File.Exists(itemPath))
                                    {
                                        throw new ArgumentException($"Erro ao tentar mover um arquivo, '{arguments[arguments.Length - 2]}' não é um identificador válido: ");
                                    }

                                    else if (userArgument && Directory.Exists(itemPath))
                                    {
                                        throw new ArgumentException($"Erro ao tentar mover uma pasta, '{arguments[arguments.Length - 2]}' não é um identificador válido: ");
                                    }

                                    //-----------------------------------------------------------------------
                                    //Validando argumentos do comando 'move'

                                    CommandsConfig.DataValidation(command, directoryPath, item, destiny);

                                    //------------------------------------------------------------------------

                                    items.Add(arguments[i]);
                                    move.Execute(directoryPath, item, destiny);

                                    Console.Write("Item:");

                                    colors.DarkGray();

                                    Console.Write($" [{item}] moved to [{arguments[0]}\\{destiny}]");
                                    Console.WriteLine();
                                    Colors.WhiteText();

                                    Console.WriteLine();

                                    LogAndReset(command);
                                }
                                //Coloquei as capturas de exceções aqui para facilitar a busca pelo item já existente
                                catch (IOException)
                                {
                                    IOException ex = new IOException("Cannot move an item, because it already exists: ");
                                    colors.DarkRed();

                                    Console.Write(ex.Message);

                                    colors.Red();

                                    Console.WriteLine($"The item '{arguments[i]}' already exists in the final directory");
                                    Colors.WhiteText();

                                    LogAndReset(command);
                                }
                                catch (InvalidDestinationPathException ex)
                                {
                                    colors.DarkRed();
                                    Console.Write("Erro ao mover um item: ");

                                    Colors.WhiteText();
                                    Console.WriteLine(ex.Message);

                                    LogAndReset(command);
                                }
                            }

                        break;

                        //EXTRACT
                        case "extract":

                            Extract extract = new Extract();

                            for (int i = 2; i < arguments.Length - 2; i++)
                            {
                                try
                                {
                                    item = new FileInfo(arguments[i]);
                                    FileInfo destiny = new FileInfo(arguments[arguments.Length - 1]);

                                    string destinyPath = Path.Combine(directoryPath.FullName, destiny.Name);
                                    string itemPath = Path.Combine(directoryPath.FullName, item.Name);

                                    //Variável para identificar se o usuário digitou outra coisa além de 'to'.
                                    bool userArgument = arguments[arguments.Length - 1] != "to";

                                    if (userArgument && File.Exists(itemPath))
                                    {
                                        throw new ArgumentException($"Erro ao tentar extrair um arquivo, '{arguments[arguments.Length - 2]}' não é um identificador válido: ");
                                    }

                                    //-----------------------------------------------------------------------
                                    //Validação dos dados (item, destiny) - EXTRACT

                                    CommandsConfig.DataValidation(command, directoryPath, item, destiny);

                                    //------------------------------------------------------------------------

                                    extract.Execute(directoryPath, item, destiny);
                                    colors.Blue();

                                    Console.Write("Item:");

                                    colors.DarkGray();

                                    Console.Write($" [{item}] extracted to [{arguments[0]}\\{destiny}]");
                                    Console.WriteLine();
                                    Colors.WhiteText();

                                    Console.WriteLine();

                                    LogAndReset(command);

                                }
                                catch (InvalidDestinationPathException ex)
                                {
                                    colors.DarkRed();
                                    Console.Write("Erro ao extrair um arquivo: ");

                                    Colors.WhiteText();
                                    Console.WriteLine(ex.Message);

                                    LogAndReset(command);
                                }
                            }

                        break;

                        //ZIP
                        case "zip":

                            Zip zip = new Zip();
                            List<string> elements = new List<string>();

                            try
                            {
                                for (int i = 2; i < arguments.Length - 2; i++)
                                {

                                    item = new FileInfo(arguments[i]);
                                    FileInfo destiny = new FileInfo(arguments[arguments.Length - 1]);

                                    string destinyPath = Path.Combine(directoryPath.FullName, destiny.Name);
                                    string itemPath = Path.Combine(directoryPath.FullName, item.Name);

                                    //Variável para identificar se o usuário digitou outra coisa além de 'to'.
                                    bool userArgument = arguments[arguments.Length - 1] != "to";

                                    if (userArgument && File.Exists(itemPath))
                                    {
                                        throw new ArgumentException($"Erro ao tentar zipar um arquivo, '{arguments[arguments.Length - 2]}' não é um identificador válido: ");
                                    }

                                    else if (userArgument && Directory.Exists(itemPath))
                                    {
                                        throw new ArgumentException($"Erro ao tentar zipar uma pasta, '{arguments[arguments.Length - 2]}' não é um identificador válido: ");
                                    }

                                    //-----------------------------------------------------------------------
                                    //Validação dos dados (item, destiny) - ZIP

                                    CommandsConfig.DataValidation(command, directoryPath, item, destiny);

                                    //------------------------------------------------------------------------

                                    elements.Add(arguments[i]);
                                    zip.Execute(directoryPath, item, destiny);
                                    colors.Blue();
                                }

                                foreach (string element in elements)
                                {
                                    Colors.WhiteText();
                                    Console.Write("Item:");

                                    colors.DarkGray();

                                    Console.WriteLine($" [{element}] zipped to [{arguments[0]}\\{arguments[arguments.Length - 1]}]");
                                }

                                LogAndReset(command);
                            }

                            catch (InvalidDestinationPathException ex)
                            {
                                colors.DarkRed();
                                Console.Write("Erro ao zipar um item: ");

                                Colors.WhiteText();
                                Console.WriteLine(ex.Message);

                                LogAndReset(command);
                            }

                        break;

                        //RENAME
                        case "rename":

                            Rename rename = new Rename();
                            List<string> objects = new List<string>();

                            FileInfo atualName = new FileInfo(arguments[2]);
                            FileInfo finalName = new FileInfo(arguments[4]);

                            rename.Execute(directoryPath, atualName, finalName);

                            Colors.WhiteText();
                            Console.Write("Item:");

                            colors.DarkGray();

                            Console.WriteLine($" [{atualName}] renamed to [{finalName}]");

                            Console.WriteLine();
                            LogAndReset(command);

                        break;

                        //CREATE-TEMPLATE
                        case "ct":

                            //ct dotnet new mvc --framework netcoreapp3.1 -o MyNewProject --auth Individual --use-local-db true
                            CreateTemplate createTemplate = new CreateTemplate();
                            Dictionary<string, string> additionalParams = new Dictionary<string, string> { { arguments[9], arguments[10] }, { arguments[11], arguments[12] } };

                            createTemplate.CreateAspNetCoreMvcTemplate(arguments[6], arguments[8], additionalParams);

                        break;
                        //----------------------------------------------------------------------------------------
                        //Comandos simples

                        //CLEAR
                        case "clear":

                            Console.Clear();
                            formatLogs.UserAndMachineName();

                        break;

                        //EXIT
                        case "exit":

                            Environment.Exit(0);

                        break;

                        //SYSTEM-INFO
                        case "system-info":

                            Console.WriteLine("Informações do sistema: ");
                            Console.WriteLine();

                            //Nome do computador
                            Console.Write("Nome do dispositivo: ");

                            colors.DarkGray();
                            Console.WriteLine($"{SystemInfo.DeviceName()}");
                            Colors.WhiteText();
                            //-----------------------------------------------

                            //Versão
                            Console.Write("Versão: ");

                            colors.DarkGray();
                            Console.WriteLine($"{SystemInfo.MachineVersion()}");
                            Colors.WhiteText();
                            //-----------------------------------------------

                            //Processador
                            Console.Write("Processador: ");

                            colors.DarkGray();
                            Console.WriteLine($"{SystemInfo.ProcessorName()}");
                            Colors.WhiteText();
                            //-----------------------------------------------

                            //Memória RAM
                            Console.Write("RAM instalada: ");

                            colors.DarkGray();
                            Console.WriteLine($"{SystemInfo.TotalRAM()}");
                            Colors.WhiteText();
                            //-----------------------------------------------

                            //Placa de vídeo
                            Console.Write("Placa de vídeo: ");

                            colors.DarkGray();
                            Console.WriteLine($"{SystemInfo.GraphicCard()}");
                            Colors.WhiteText();
                            //-----------------------------------------------

                            //Armazenamento
                            SystemInfo.Drive();

                            Colors.WhiteText();
                            //-----------------------------------------------

                            //Tipo do sistema
                            Console.Write("Tipo do sistema: ");

                            colors.DarkGray();
                            Console.WriteLine($"Sistema operacional de {SystemInfo.SystemType()}, {SystemInfo.ProcessorType()}");
                            Colors.WhiteText();
                            //-----------------------------------------------

                            Console.WriteLine();

                            formatLogs.UserAndMachineName();

                        break;
                        //----------------------------------------------------------------------------------------
                    }
                }
                //----------------------------------------------------------------------------------------

                //----------------------------------------------------------------------------------------
                //Tratando exceções

                //Quando o programa não encontra um diretório
                catch (DirectoryNotFoundException)
                {
                    colors.DarkRed();

                    Console.WriteLine($"Directory '{arguments[0]}' not found!");
                    Colors.WhiteText();
                    Console.WriteLine();

                    formatLogs.UserAndMachineName();
                }

                //Quando o prorama não encontra algo para abrir
                catch (OpenCommandException ex)
                {
                    colors.DarkRed();
                    Console.Write(ex.Message);
                    Colors.WhiteText();

                    Console.WriteLine($"O item '{arguments[2]}' não existe neste diretório.");

                    LogAndReset(command);
                }
                catch (ArgumentException ex)
                {
                    colors.DarkRed();

                    Console.Write(ex.Message);

                    Colors.WhiteText();
                    Console.WriteLine("Escreva 'commands' para ver a lista de comandos ou 'commands-sintaxe' para ver a sua sintaxe.");
                    Colors.WhiteText();

                    Console.WriteLine();

                    formatLogs.UserAndMachineName();
                }
                //----------------------------------------------------------------------------------------
            }
        }

        public static void LogAndReset(string command)
        {
            FormatLogs formatLogs = new FormatLogs();

            switch (command) 
            {
               /*
                  Casos específicos:

                  'scan'
                  'commands'
                  'open-site' 

                */

                //Log para o comando 'scan'
                case "scan":

                    formatLogs.ScanAndListLogs();
                    formatLogs.UserAndMachineName();

                break;

                //Log para o comando 'list'
                case "list":

                    formatLogs.ScanAndListLogs();
                    formatLogs.UserAndMachineName();

                break;

                //Log para o comando 'commands'
                case "commands":

                    formatLogs.CommandLog();
                    formatLogs.UserAndMachineName();

                break;

                //Log para o comando 'open-site'
                case "open-site":

                    formatLogs.SiteLog();
                    formatLogs.UserAndMachineName();

                break;

                //Log para os comandos de diretórios
                default:

                    Console.WriteLine();
                    formatLogs.DirectoryLog();
                    formatLogs.UserAndMachineName();

                break;
            }
        }
        
    }
}
