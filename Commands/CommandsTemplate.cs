using DirectoryCLI.Handlers;
using DirectoryCLI.Interfaces;
using System.Diagnostics;
using System.Text;

namespace DirectoryCLI.Commands
{
    internal class CommandsTemplate : CommandsConfig
    {

        //------------------------------------------------------------------------------------------
        //Métodos:

        //Dotnet
        //Docker

        //------------------------------------------------------------------------------------------
        public static void ExecuteCommandTemplate(string[] arguments)
        {
            ICommandHelper commandHelper = new CommandHelper();

            string templateCommand = commandHelper.AddParameters(arguments);
            RunCommand(templateCommand);
        }
        //------------------------------------------------------------------------------------------

        //Métodos auxiliares

        //RunCommand()
        private static void RunCommand(string commandString)
        {
            Console.OutputEncoding = Encoding.UTF8;

            ProcessStartInfo processStartInfo = new ProcessStartInfo()
            {
                FileName = "cmd.exe",
                Arguments = $"/C {commandString}",
                CreateNoWindow = false,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };

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
                    Console.WriteLine("Error: " + error.TrimEnd());
                    Console.WriteLine();
                }
            }
        }
        //------------------------------------------------------------------------------------------

        //AddParameters()
        //------------------------------------------------------------------------------------------
    }
}
