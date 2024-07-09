using DirectoryCLI.Commands;
using DirectoryCLI.CommandStyles;
using DirectoryCLI.Exceptions;
using System;
using CommandLine;
using System.Windows.Input;
using System.Drawing;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Xml.Linq;
using Console = Colorful.Console;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
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

            //Classe para facilitar o acesso às cores
            Colors colors = new Colors();

            formatLogs.UserAndMachineName();

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

                            if (arguments[0] == "dotnet")
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
                        // CommandsConfig.CommandValidation(arguments);
                        //----------------------------------------------------------------------------------------

                        //Essa variável do tipo "FileInfo" será usada apenas nos comandos que usam diretórios
                        FileInfo directoryPath = new FileInfo(arguments[0]);

                        //EXECUÇÃO DOS COMANDOS
                        switch (command)
                        {
                            //----------------------------------------------------------------------------------------

                            //CREATE-FOLDER
                            case "create-folder":


                                CreateFolder.Execute(directoryPath, arguments);
                                CommandsConfig.LogAndReset(command);

                                break;

                            //DELETE-FOLDER
                            case "delete-folder":

                                DeleteFolder.Execute(directoryPath, arguments);
                                CommandsConfig.LogAndReset(command);

                                break;

                            //CREATE-FILE
                            case "create-file":

                                CreateFile.Execute(directoryPath, arguments);
                                CommandsConfig.LogAndReset(command);

                                break;

                            //DELETE-FILE
                            case "delete-file":

                                DeleteFile.Execute(directoryPath, arguments);
                                CommandsConfig.LogAndReset(command);

                                break;

                            //OPEN
                            case "open":

                                Open.Execute(directoryPath, arguments);
                                CommandsConfig.LogAndReset(command);

                                break;

                            //OPEN-SITE
                            case "open-site":


                                OpenSite.Execute(arguments[1]);
                                CommandsConfig.LogAndReset(command);

                                break;

                            //SCAN
                            case "scan":

                                Scan.Execute(arguments[0]);
                                CommandsConfig.LogAndReset(command);

                                break;

                            //COMMANDS
                            case "commands":

                                CommandsInfo.Execute();
                                CommandsConfig.LogAndReset(command);

                                break;

                            //CMD-SINTAXE
                            case "commands-sintaxe":

                                CommandsInfo.CommandSintaxe();
                                CommandsConfig.LogAndReset(command);

                                break;

                            //LIST
                            case "list":

                                DirectoryList.Execute(arguments[0]);
                                CommandsConfig.LogAndReset(command);

                                break;

                            //MOVE
                            case "move":

                                Move.Execute(directoryPath, command, arguments);
                                CommandsConfig.LogAndReset(command);

                                break;

                            //EXTRACT
                            case "extract":

                                Extract.Execute(directoryPath, command, arguments);
                                CommandsConfig.LogAndReset(command);

                                break;

                            //ZIP
                            case "zip":

                                Zip.Execute(directoryPath, command, arguments);
                                CommandsConfig.LogAndReset(command);

                                break;

                            //RENAME
                            case "rename":

                                Rename.Execute(directoryPath, arguments);
                                CommandsConfig.LogAndReset(command);

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



                            //CREATE-TEMPLATE
                            default:

                                CreateTemplate.Execute(arguments);
                                CommandsConfig.LogAndReset(command);

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

                        Console.WriteLine($"Diretório '{arguments[0]}' não encontrado!");
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

                        CommandsConfig.LogAndReset(command);
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

            //-------------------------------------------------LINUX-PLATFORM----------------------------------------------------------------------------------------------
            //-----------------------------------------------------------------------------------------------------------------------------------------------

            //Plataforma Linux
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                //----------------------------------------------------------------------------------------
                Colors.BlackBG();

                string[] arguments = Console.ReadLine().Split(' ');
                string command;

                //----------------------------------------------------------------------------------------
                //Switch para atribuir qual é o comando para a variável "cmd"
                while (true)
                {
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
                        //Essa variável do tipo "FileInfo" será usada apenas nos comandos que usam diretórios
                        FileInfo directoryPath = new FileInfo(arguments[0]);
                        //----------------------------------------------------------------------------------------
                        //Verificação dos argumentos fornecidos
                        CommandsConfig.ArgumentsValidation(arguments);
                        //----------------------------------------------------------------------------------------

                        switch (command)
                        {

                            case "-c-sd":

                                CreateFolder.Execute(directoryPath, arguments);
                                CommandsConfig.LogAndReset(command);

                                break;

                            case "-d-sd":

                                DeleteFolder.Execute(directoryPath, arguments);
                                CommandsConfig.LogAndReset(command);

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
