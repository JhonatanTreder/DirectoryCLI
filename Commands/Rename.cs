using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Commands
{
    internal class Rename : CommandsConfig
    {
        public Rename() 
        {

        }

        public void Execute(FileInfo directoryPath, FileInfo name, FileInfo nameFinal)
        {
            string namePath = Path.Combine(directoryPath.FullName, name.Name);
            string finalNamePath = Path.Combine(directoryPath.FullName, nameFinal.Name);

            if (Directory.Exists(namePath))
            {
                Directory.Move(namePath, finalNamePath);
            }

            else
            {
                File.Move(namePath, finalNamePath);
            }
        }
    }
}
