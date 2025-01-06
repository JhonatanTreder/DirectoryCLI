using DirectoryCLI.Commands;
using DirectoryCLI.CommandStyles;
using DirectoryCLI.Exceptions;
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
    {//----------------------------------------------------------------------------------------
        [SupportedOSPlatform("Linux")]
        [SupportedOSPlatform("MacOS")]
        [SupportedOSPlatform("Windows")]
        static async Task Main()
        {
            Console.Title = $"@Zenith - {Environment.CurrentDirectory}";

            ILogHandler logHandler = new LogHandler();
            IFileHandler fileHandler = new FileHandler();
            ISystemHandler systemHandler = new SystemHandler();
            ICommandHelper commandHelper = new CommandHelper();
            ICommandValidator commandValidator = new CommandValidator();
            IFolderHandler folderHandler = new FolderHandler(commandValidator, logHandler);
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
                    //----------------------------------------------------------------------------------------
                    Colors.BlackBG();
                    Console.Write("$ ");

                    string[] arguments = Console.ReadLine().Split(' ');

                    arguments = commandHelper.RemoveNullOrEmpty(arguments);

                    string command = commandHelper.AddCommand(arguments).ToLower();

                    Console.WriteLine();
                    //----------------------------------------------------------------------------------------

                    try
                    {
                        //Verificação dos argumentos fornecidos
                        commandValidator.ArgumentsValidation(arguments);

                        //EXECUÇÃO DOS COMANDOS
                        switch (command)
                        {
                            //----------------------------------------------------------------------------------------

                            //CREATE-FOLDER
                            case "create-folder":

                                folderHandler.Create(arguments);
                                logHandler.ShowLog(log, command);

                                break;

                            //DELETE-FOLDER
                            case "delete-folder":

                                folderHandler.Delete(arguments);
                                logHandler.ShowLog(log, command);

                                break;

                            //CREATE-FILE
                            case "create-file":

                                fileHandler.Create(arguments);
                                logHandler.ShowLog(log, command);

                                break;

                            //DELETE-FILE
                            case "delete-file":

                                fileHandler.Delete(arguments);
                                logHandler.ShowLog(log, command);

                                break;

                            //OPEN
                            case "open":

                                directoryHandler.Open(arguments);
                                logHandler.ShowLog(log, command);

                                break;

                            //OPEN-SITE
                            case "open-site":

                                await systemHandler.OpenSite(arguments[1]);
                                logHandler.ShowLog(log, command);

                                break;

                            //SCAN
                            case "scan":

                                directoryHandler.ScanSize(arguments[0]);
                                logHandler.ShowLog(log, command);

                                break;

                            //LIST
                            case "list":

                                directoryHandler.ListItems(arguments);
                                logHandler.ShowLog(log, command);

                                break;

                            //MOVE
                            case "move":

                                directoryHandler.MoveItem(arguments);
                                logHandler.ShowLog(log, command);

                                break;

                            //EXTRACT
                            case "extract":

                                directoryHandler.ExtractZipFile(arguments);
                                logHandler.ShowLog(log, command);

                                break;

                            //ZIP
                            case "zip":

                                directoryHandler.ZipItem(arguments);
                                logHandler.ShowLog(log, command);
                                break;

                            //RENAME
                            case "rename":

                                directoryHandler.Rename(arguments);
                                logHandler.ShowLog(log, command);

                                break;

                            case "del-files":

                                fileHandler.DeleteAllFiles(arguments);
                                logHandler.ShowLog(log, command);

                                break;

                            //DEL-FOLDERS
                            case "del-folders":

                                folderHandler.DeleteSubdirectories(arguments[0]);
                                logHandler.ShowLog(log, command);

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
                                }

                                else
                                {
                                    log = true;
                                    Console.WriteLine("Logs ativados");
                                }

                                logHandler.ShowLog(log, command);

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
                                logHandler.ShowLog(log, command);

                                break;

                            //COMMANDS
                            case "commands":

                                CommandsInfo.Commands();
                                logHandler.ShowLog(log, command);

                                break;

                            //CMD-SINTAXE
                            case "commands-sintaxe":

                                CommandsInfo.CommandSintaxe();
                                logHandler.ShowLog(log, command);

                                break;

                            default:

                                if (arguments[0] == "dotnet" || arguments[0] == "docker" || arguments[0] == "git")
                                {
                                    CommandsTemplate.ExecuteCommandTemplate(arguments);
                                    logHandler.ShowLog(log, command);
                                }

                                break;
                                //----------------------------------------------------------------------------------------
                        }
                    }
                    //----------------------------------------------------------------------------------------

                    //----------------------------------------------------------------------------------------
                    //Tratando exceções

                    //Quando os logs estão desativados os logs de erros aparecem mesmo assim
                    //(talvez não era para isso acontecer)

                    catch (FormatException ex)
                    {
                        logHandler.LogError(ex, log);
                    }

                    catch (ArgumentException ex)
                    {
                        logHandler.LogError(ex, log);
                    }

                    catch (FileNotFoundException ex)
                    {
                        logHandler.LogError(ex, log);
                    }

                    catch (DirectoryNotFoundException ex)
                    {
                        logHandler.LogError(ex, log);
                    }

                    catch (UnauthorizedAccessException ex)
                    {
                        logHandler.LogError(ex, log);
                    }

                    catch (PlatformNotSupportedException ex)
                    {
                        logHandler.LogError(ex, log);
                    }

                    catch (SecurityException ex)
                    {
                        logHandler.LogError(ex, log);
                    }

                    catch (IOException ex)
                    {
                        logHandler.LogError(ex, log);
                    }

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
                    commandValidator.ArgumentsValidation(arguments);

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
                catch (FormatException ex)
                {
                    logHandler.LogError(ex, log);
                }
                catch (ArgumentException ex)
                {
                    logHandler.LogError(ex, log);
                }
                catch (FileNotFoundException ex)
                {
                    logHandler.LogError(ex, log);
                }
                catch (DirectoryNotFoundException ex)
                {
                    logHandler.LogError(ex, log);
                }
                catch (UnauthorizedAccessException ex)
                {
                    logHandler.LogError(ex, log);
                }
                catch (PlatformNotSupportedException ex)
                {
                    logHandler.LogError(ex, log);
                }

                catch (SecurityException ex)
                {
                    logHandler.LogError(ex, log);
                }
                catch (IOException ex)
                {
                    logHandler.LogError(ex, log);
                }
                catch (Exception ex)
                {
                    logHandler.LogError(ex, log);
                }
            }
        }
    }
}
