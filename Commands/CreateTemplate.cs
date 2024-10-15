using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DirectoryCLI.Commands
{
    internal class CreateTemplate : CommandsConfig
    {
        public static void Execute(string[] arguments)
        {
            switch (arguments[0])
            {
                case "dotnet":

                    string dotnetCommand = AddParameters(arguments);
                    DotnetHandlerTemplate(dotnetCommand);

                break;

                case "java":

                    string javaCommand = AddParameters(arguments);
                    JavaHandlerTemplate(javaCommand);

                    break;
            }
        }

        //------------------------------------------------------------------------------------------
        //Handlers:

        //Dotnet
        private static void DotnetHandlerTemplate(string templateCommand)
        {
            string command = $"dotnet {templateCommand}";
            RunCommand(command);
        }

        //------------------------------------------------------------------------------------------
        //Java
        private static void JavaHandlerTemplate(string templateCommand)
        {

            string command = $"{templateCommand} archetype:generate -DinteractiveMode=false";
            RunCommand(command);
        }
        //------------------------------------------------------------------------------------------

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
        private static string AddParameters(string[] arguments)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            for (int i = 0; i < arguments.Length - 2; i += 2)
            {
                parameters.Add(arguments[i + 1], arguments[i + 2]);
            }
            
            string commandString = string.Join(" ", parameters.Select(kvp => $"{kvp.Key} {kvp.Value}"));

            return commandString;
        }
        //------------------------------------------------------------------------------------------

    }
}
