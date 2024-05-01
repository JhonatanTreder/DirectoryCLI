using DirectoryCLI.CommandStyles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Commands
{
    internal class CommandsConfig
    {
        public string DirectoryPath { get; set; }

        public static bool IsValidDirectoryPath(FileInfo path)
        {
            if (Directory.Exists(path.FullName) == true)
            {
                return true;
            }

            else
            {
                throw new DirectoryNotFoundException();
            }
        }

        public override string ToString()
        {
            Colors colors = new Colors();

            StringBuilder sb = new StringBuilder();
            colors.DarkGray();
            sb.Append("[open] - Abre uma pasta ou um arquivo específico - ");
            sb.AppendLine("sintaxe de uso: <diretório> <open> <arquivo/pasta>");
            sb.AppendLine();

            sb.Append("[open-site] - Acessa um site através de um nome de domínio (o nome de domínio tem que ser escrito de forma literal sem erros)");
            sb.AppendLine("sintaxe de uso: <open-site> <site/nome de domínio>");
            sb.AppendLine();

            sb.Append("[create-folder] - Cria uma pasta em um diretório especificado (podendo criar mais de uma pasta se separar pela tecla 'espace')");
            sb.AppendLine("sintaxe de uso: <diretório> <create-folder> <nome da pasta>");
            sb.AppendLine();

            sb.Append("[delete-folder] - Deleta uma pasta em um diretório especificado (podendo deletar mais de uma pasta se separar pela tecla 'espace')");
            sb.AppendLine("sintaxe de uso: <diretório> <delete-folder> <nome da pasta>");
            sb.AppendLine();

            sb.Append("[create-file] - Cria um arquivo em um diretório especificado (podendo criar mais de um arquivo se separar pela tecla 'espace')");
            sb.AppendLine("sintaxe de uso: <diretório> <create-file> <nome do arquivo>");
            sb.AppendLine();

            sb.Append("[delete-file] - Deleta um arquivo em um diretório especificado (podendo deletar mais de um arquivo se separar pela tecla 'espace')");
            sb.AppendLine("sintaxe de uso: <diretório> <delete-file> <nome do arquivo> ");
            sb.AppendLine();

            return sb.ToString();
        }
    }
}
