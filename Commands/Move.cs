using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Commands
{
    internal class Move : CommandsConfig
    {

        public Move()
        {

        }

        public void Execute(FileInfo directoryPath, FileInfo item, FileInfo destiny)
        {    
            
            string initialDirectory = Path.Combine(directoryPath.FullName, item.Name);
            string destinyPath = Path.Combine(directoryPath.FullName, destiny.Name);
            string fullPath = Path.Combine(destinyPath, item.Name);
            
            Directory.Move(initialDirectory, fullPath);

        }
    }
}
/* if (IsValidDirectoryPath(directoryPath) == true)
            {

            }

            else
            {
                throw new DirectoryNotFoundException();
            }



                D:\Test\Pasta
                D:\Test\hl

                D:\Test\Pasta\hl
                D:\Test\hl\Pasta
        */