using DirectoryCLI.Interfaces;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace DirectoryCLI.Handlers
{
    internal class CommandHelper : ICommandHelper
    {
        //Função para diminuir a string na hora de exibir uma tabela de mensagem.
        public string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength) + "...";
        }

        //Função para formatar o tamanho de espaço.
        public string FormatBytes(long bytes)
        {
            if (bytes >= 0x1000000000000000) { return ((double)(bytes >> 50) / 1024).ToString("0.### EB"); }
            if (bytes >= 0x4000000000000) { return ((double)(bytes >> 40) / 1024).ToString("0.### PB"); }
            if (bytes >= 0x10000000000) { return ((double)(bytes >> 30) / 1024).ToString("0.### TB"); }
            if (bytes >= 0x40000000) { return ((double)(bytes >> 20) / 1024).ToString("0.### GB"); }
            if (bytes >= 0x100000) { return ((double)(bytes >> 10) / 1024).ToString("0.### MB"); }
            if (bytes >= 0x400) { return ((double)bytes / 1024).ToString("0.###") + " KB"; }

            return bytes.ToString("0 Bytes");
        }

        //Algoritmo para identificação do comando.
        public string AddCommand(string[] arguments)
        {
            bool directoryExists = Directory.Exists(arguments[0]);

            if (directoryExists && arguments.Length != 1)
            {
                return arguments[1];
            }

            else if (!directoryExists && arguments.Length >= 3)
            {
                return arguments[1];
            }

            else
            {
                return arguments[0];
            }
        }

        //Algoritmo para remover argumentos vazios.
        public string[] RemoveNullOrEmpty(string[] arguments)
        {
            if (arguments.Length == 0 || string.IsNullOrEmpty(arguments[0]))
            {
                return arguments;
            }

            List<string> newArgumentArray = new List<string>();

            for (int i = 0; i < arguments.Length; i++)
            {
                if (!string.IsNullOrEmpty(arguments[i]))
                {
                    newArgumentArray.Add(arguments[i]);
                }
            }

            return newArgumentArray.ToArray();
        }

        //Função para rodar comandos de CLI.
        public void RunCommand(string commandString)
        {
            Console.OutputEncoding = Encoding.UTF8;

            ProcessStartInfo processStartInfo = new ProcessStartInfo();

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                processStartInfo.FileName = "cmd.exe";
                processStartInfo.Arguments = $"/C {commandString}";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                processStartInfo.FileName = "/bin/bash";
                processStartInfo.Arguments = $"-c \"{commandString}\"";
            }

            processStartInfo.CreateNoWindow = false;
            processStartInfo.UseShellExecute = false;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardError = true;

            using (Process process = new Process())
            {
                process.StartInfo = processStartInfo;
                process.Start();

                string result = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();

                if (!string.IsNullOrEmpty(result))
                {
                    Console.WriteLine(result.TrimEnd());
                    Console.WriteLine();
                }

                if (!string.IsNullOrEmpty(error))
                {
                    Console.Write("Error: ");

                    Console.WriteLine();
                    Console.WriteLine(error.TrimEnd());
                    Console.WriteLine();
                }
            }
            //------------------------------------------------------------------------------------------
        }
    }
}
