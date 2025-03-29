using DirectoryCLI.CommandStyles;
using DirectoryCLI.Handlers;
using DirectoryCLI.Interfaces;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;
using System.Text;
using System;
using Console = Colorful.Console;

namespace DirectoryCLI
{
    internal class Program
    {
        //----------------------------------------------------------------------------------------
        //Plataformas suportadas
        [SupportedOSPlatform("Linux")]
        [SupportedOSPlatform("MacOS")]
        [SupportedOSPlatform("Windows")]
        //--------------------------------
        static async Task Main()
        {
            Console.Title = $"@Zenith - {Environment.CurrentDirectory}";

            //Instanciando os Handlers
            ILogHandler logHandler = new LogHandler();
            IFileSystemHandler fileHandler = new FileHandler(logHandler);
            ISystemHandler systemHandler = new SystemHandler();
            ICommandHelper commandHelper = new CommandHelper();
            ICommandValidator commandValidator = new CommandValidator();
            IFileSystemHandler folderHandler = new FolderHandler(commandValidator, logHandler);
            IDirectoryHandler directoryHandler = new DirectoryHandler(commandHelper, commandValidator, logHandler);

            Colors.BlackBG();
            Console.Clear();
            logHandler.UserAndMachineName();

            bool log = true;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                //----------------------------------------------------------------------------------------
                //Uso do "while" para deixar o programa rodando em looping

                while (true)
                {
                    Colors.BlackBG();
                    Console.Write("$ ");

                    //--------------------------------------------------------------
                    //Atribuindo o comando e melhorando a leitura dos argumentos
                    //(mesmo se forem separados por mais de um espaço)
                    string[] arguments = Console.ReadLine().Split(' ');
                    arguments = commandHelper.RemoveNullOrEmpty(arguments);
                    string command = commandHelper.AddCommand(arguments).ToLower();
                    //---------------------------------------------------------------

                    Console.WriteLine();

                    try
                    {
                        if (commandValidator.ArgumentsValidation(arguments, command) == false)
                        {
                            command = "none";
                        }
                        //Verificação dos argumentos fornecidos


                        //EXECUÇÃO DOS COMANDOS
                        switch (command)
                        {
                            //----------------------------------------------------------------------------------------

                            //CREATE-FOLDER
                            case "create-folder":

                                folderHandler.Create(arguments);
                                logHandler.ShowLog(command, log);

                                break;

                            //DELETE-FOLDER
                            case "delete-folder":

                                folderHandler.Delete(arguments);
                                logHandler.ShowLog(command, log);

                                break;

                            //CREATE-FILE
                            case "create-file":

                                fileHandler.Create(arguments);
                                logHandler.ShowLog(command, log);

                                break;

                            //DELETE-FILE
                            case "delete-file":

                                fileHandler.Delete(arguments);
                                logHandler.ShowLog(command, log);

                                break;

                            //OPEN
                            case "open":

                                directoryHandler.Open(arguments);
                                logHandler.ShowLog(command, log);

                                break;

                            //OPEN-SITE
                            case "open-site":

                                await systemHandler.OpenSite(arguments[1]);
                                logHandler.ShowLog(command, log);

                                break;

                            //SCAN
                            case "scan":

                                directoryHandler.ScanSize(arguments[0]);
                                logHandler.ShowLog(command, log);

                                break;

                            //LIST
                            case "list":

                                directoryHandler.ListItems(arguments);
                                logHandler.ShowLog(command, log);

                                break;

                            //MOVE
                            case "move":

                                directoryHandler.MoveItem(arguments);
                                logHandler.ShowLog(command, log);

                                break;

                            //EXTRACT
                            case "extract":

                                directoryHandler.ExtractZipFile(arguments);
                                logHandler.ShowLog(command, log);

                                break;

                            //ZIP
                            case "zip":

                                directoryHandler.ZipItem(arguments);
                                logHandler.ShowLog(command, log);
                                break;

                            //RENAME
                            case "rename":

                                directoryHandler.Rename(arguments);
                                logHandler.ShowLog(command, log);

                                break;

                            case "del-files":

                                fileHandler.DeleteRecursive(arguments);
                                logHandler.ShowLog(command, log);

                                break;

                            //DEL-FOLDERS
                            case "del-folders":

                                folderHandler.DeleteRecursive(arguments);
                                logHandler.ShowLog(command, log);

                                break;

                            case "log-off":

                                if (log == false)
                                {
                                    Console.WriteLine("Os logs já estão desativados.");
                                }

                                else
                                {
                                    log = false;
                                    Console.WriteLine("Logs desativados.");
                                }

                                logHandler.LogCommand(command);

                                break;


                            case "log-on":

                                if (log == true)
                                {
                                    Console.WriteLine("Os logs já estão ativados.");
                                    Console.WriteLine();
                                }

                                else
                                {
                                    log = true;
                                    Console.WriteLine("Logs ativados");
                                    Console.WriteLine();
                                }

                                logHandler.ShowLog(command, log);

                                break;

                            //CLEAR
                            case "clear":

                                Console.Clear();
                                logHandler.UserAndMachineName();

                                break;

                            //EXIT
                            case "exit":

                                Environment.Exit(0);

                                break;

                            //SYSTEM-INFO
                            case "system-info":

                                systemHandler.SystemInfo();
                                logHandler.ShowLog(command, log);

                                break;

                            //COMMANDS
                            case "cmds":

                                CommandsInfo.Commands();
                                logHandler.ShowLog(command, log);

                                break;

                            //CMD-SINTAXE
                            case "cmds-sintaxe":

                                CommandsInfo.CommandSintaxe();
                                logHandler.ShowLog(command, log);

                                break;

                            case "dir-cmds":

                                CommandsInfo.DirectoryCommands();
                                logHandler.ShowLog(command, log);

                                break;


                            default:

                                if ((string.IsNullOrEmpty(command) || (Directory.Exists(arguments[0]) && command == "none")))
                                {
                                    logHandler.ShowLog(command, log);
                                    break;
                                }

                                SystemHandler.ExecCommand(arguments);
                                logHandler.ShowLog(command, log);

                                break;
                                //----------------------------------------------------------------------------------------
                        }
                    }
                    //----------------------------------------------------------------------------------------

                    //----------------------------------------------------------------------------------------
                    //Tratando exceções

                    //Quando os logs estão desativados os logs de erros aparecem mesmo assim
                    //(talvez não era para isso acontecer)

                    catch (Exception ex)
                    {
                        logHandler.LogError(ex, log);
                    }

                    //----------------------------------------------------------------------------------------
                }
            }
            //-------------------------------------------------LINUX-PLATFORM----------------------------------------------------------------------------------------------
            //-----------------------------------------------------------------------------------------------------------------------------------------------

            //Plataforma Linux
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Colors.BlackBG();
                Console.Write("$ ");

                string[] arguments = Console.ReadLine().Split(' ');

                arguments = commandHelper.RemoveNullOrEmpty(arguments);

                string command = commandHelper.AddCommand(arguments).ToLower();

                Console.WriteLine();

                try
                {
                    //Verificação dos argumentos fornecidos
                    commandValidator.ArgumentsValidation(arguments, command);

                    switch (command)
                    {
                        case "-c-sd":

                            folderHandler.Create(arguments);
                            logHandler.LogCommand(command);

                            break;

                        case "-d-sd":

                            folderHandler.Delete(arguments);
                            logHandler.LogCommand(command);

                            break;
                    }
                }
                catch (Exception ex)
                {
                    logHandler.LogError(ex, log);
                }
            }
        }
    }
}
