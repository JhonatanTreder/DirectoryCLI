using DirectoryCLI.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Commands
{
    internal class Open : CommandsConfig
    {
        public static void Execute(FileInfo directoryPath, string[] arguments)
        {

            //----------------------------------------------------------------------------------------
            //Algoritmo para identificar se existe um diretório com pastas ou arquivos 

            //Apenas diminui as verificações em duas variávies: "folderExist" e "archiveExist"
            FileInfo item = new FileInfo(Path.Combine(arguments[2]));

            bool folderExist = Directory.Exists(arguments[0]) && Directory.Exists(Path.Combine(arguments[0], arguments[2]));
            bool archiveExist = Directory.Exists(arguments[0]) && File.Exists(Path.Combine(arguments[0], arguments[2]));

            //Variável booleana para saber se o arquivo expecificado termina com uma extensão que não é nula.
            bool _NonExistentFile = !string.IsNullOrEmpty(item.Extension) && item.Name.EndsWith(item.Extension);

            string fullPath = Path.Combine(directoryPath.FullName, arguments[2]);

            if (folderExist || archiveExist)
            {
                System.Diagnostics.Process.Start(fullPath);
            }

            //----------------------------------------------------------------------------------------

            //Lança uma exceção personalizada se isso não ocorrer

            else if (_NonExistentFile)
            {
                throw new OpenCommandException("Erro ao abrir um arquivo: ");
            }

            else
            {
                throw new OpenCommandException("Erro ao abrir uma pasta: ");
            }
        }
    }
}
