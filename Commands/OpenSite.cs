using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Commands
{
    internal class OpenSite : CommandsConfig
    {
        public OpenSite() 
        {
            
        }

        public void Execute(string domain)
        {
            Process.Start("https://www." + domain + ".com");
        }
    }
}
