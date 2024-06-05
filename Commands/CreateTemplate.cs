using System;
using System.Collections.Generic;
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

        public void CreateAspNetCoreMvcTemplate(string version, string outputDir, Dictionary<string, string> additionalParams)
        {
            string command = $"dotnet new mvc --framework {version} -o {outputDir}";

            foreach (var param in additionalParams)
            {
                command += $" --{param.Key} {param.Value}";
            }

            System.Diagnostics.Process.Start("CMD.exe", "/C " + command);
        }
    }
}
