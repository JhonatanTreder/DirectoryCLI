using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Commands
{
    internal class CreateTemplate : CommandsConfig
    {
        public CreateTemplate() 
        {

        }

        public static void Execute(string[] arguments)
        {
            string identifierCommand = arguments[0];
            string action = arguments[1];

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            for (int i = 0; i < arguments.Length - 2; i += 2)
            {
                parameters.Add(arguments[i + 1], arguments[i + 2]);
            }

            string commandString = string.Join(" ", parameters.Select(kvp => $"{kvp.Key} {kvp.Value}"));

            CreateAspNetCoreMvcTemplate(commandString, identifierCommand);
        }

        private static void CreateAspNetCoreMvcTemplate(string commandString, string identifierCommand)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd", $"/c {identifierCommand} " + commandString) 
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process process = new Process
            {
                StartInfo = processStartInfo
            };

            try
            {
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();

                // Exibindo a saída
                Console.WriteLine("Output:");
                Console.WriteLine(output);

            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
            }
            finally
            {
                process.Close();
            }
        }
    }
}
