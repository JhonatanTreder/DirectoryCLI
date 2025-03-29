using DirectoryCLI.CommandStyles;
using DirectoryCLI.Interfaces;
using Spectre.Console;
using System.IO;
using System.Reflection;
using System.Runtime;
using System.Text;

namespace DirectoryCLI.Handlers
{
    internal class LogHandler : ILogHandler
    {
        //Método para gerenciar o tipo de log de cada comando.
        public void LogCommand(string command)
        {

            switch (command)
            {

                //Log para o comando 'scan'
                case "scan":
                case "list":

                    ScanAndListLogs();

                    break;

                //Log para o comando 'commands'

                case "commands":
                case "system-info":
                case "commands-sintaxe":
                case "log-on":

                    CommandLog();

                    break;

                //Log para o comando 'open-site'
                case "open-site":

                    SiteLog();

                    break;

                case "log-off":

                    UserAndMachineName();

                    break;

                case "exec":

                    CLICommands();

                    break;

                //Log para os comandos de diretórios
                default:

                    if (command != "none")
                    {
                        DirectoryLog();
                    }

                    break;
            }
        }

        //Método para mostrar log de comando.
        public void ShowLog(string command, bool log)
        {
            if (log == true && !string.IsNullOrEmpty(command))
            {
                LogCommand(command);
            }

            UserAndMachineName();
        }

        //Método para mostrar log de erro
        public void LogError(Exception ex, bool log)
        {
            if (log == true) 
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine();
                Console.WriteLine($"Usage error: [{ex.GetType()}]");
                UserAndMachineName();
            }

            else
            {
                UserAndMachineName();
                return;
            }
        }

        //Método para renderizar o nome do usuário e da máquina.
        public void UserAndMachineName()
        {
            Colors.WhiteText();

            Colors.Blue();
            Console.Write("#");

            Colors.DarkPurple();
            Console.WriteLine(Environment.UserName + " - " + Environment.MachineName);
            Colors.WhiteText();
        }

        //Log de comandos que usam diretório.
        private void DirectoryLog()
        {
            Colors.WhiteText();
            Console.WriteLine("Usage: [directory] [command] [parameters]");
        }

        //Log do comando 'open-site'.
        private void SiteLog()
        {
            Console.WriteLine("Usage: [command] [site/domain name]");
            Colors.WhiteText();
        }

        //Log dos comandos 'scan' e 'list'.
        private void ScanAndListLogs()
        {
            Console.WriteLine("Usage: [directory] [command]");
        }

        //Log para comandos de CLI.
        public void CommandLog()
        {
            Console.WriteLine("Usage: [command]");
        }

        public void CLICommands()
        {
            Console.WriteLine("Usage: <exec> [command] [arguments...]");
        }

        //Método para mostrar a saída de um comando em uma tabela.
        public void ShowResult(string title, HashSet<string> items)
        {
            //Uso do 'OutputEncoding' para garantir a renderização correta da tabela.
            Console.OutputEncoding = Encoding.UTF8;

            ICommandHelper commandHelper = new CommandHelper();
            Table table = new Table() { Width = 90};

            if (items.Count > 0)
            {

                string firstItem = items.ToArray().First();

                //Caso seja um diretório
                if (Directory.Exists(firstItem))
                {
                    table.AddColumns($"{title}: {items.Count}", "Data de criação", "Caminho");
                }

                //Caso seja um arquivo
                else if (File.Exists(firstItem))
                {
                    table.AddColumns($"{title}: {items.Count}", "Extensão", "Caminho");
                }

                //Caso o objeto não é um arquivo nem um diretório
                else
                {
                    table.AddColumns($"{title}: {items.Count}");
                }

                foreach (var item in items)
                {
                    //Formatando a tabela de acordo com o tipo de objeto (arquivo ou diretório) da lista "items".

                    //Diretório: [nome do diretório] [Data de criação] [Caminho]
                    if (Directory.Exists(item))
                    {
                        DirectoryInfo directory = new DirectoryInfo(item);

                        string dirName = commandHelper.Truncate(directory.Name, 30);
                        string dirPath = commandHelper.Truncate(directory.FullName, 30);

                        table.AddRow(Markup.Escape(dirName), directory.CreationTime.ToString(), Markup.Escape(dirPath));
                    }

                    //Arquivo: [Nome do arquivo] [Extensão] [Caminho]
                    else if (File.Exists(item))
                    {
                        FileInfo file = new FileInfo(item);

                        string fileName = commandHelper.Truncate(Path.GetFileName(item), 30);
                        string filePath = commandHelper.Truncate(file.FullName, 30);
                        string extension = string.IsNullOrEmpty(file.Extension) ? "NONE" : file.Extension;

                        table.AddRow(Markup.Escape(fileName), Markup.Escape(extension), Markup.Escape(filePath));
                    }

                    else
                    {
                        table.AddRow($" {Markup.Escape(item)}");
                    }
                }

                AnsiConsole.Write(table);
                Console.WriteLine();
            }
        }
    }
}
