using DirectoryCLI.CommandStyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Commands
{
    internal class CommandsInfo
    {
        Colors colors = new Colors();
        
        public CommandsInfo()
        {

        }

        public void Execute()
        {
            Console.Write("[list]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.WriteLine("Lista todos os arquivos/pastas em um diretório");
            Console.ResetColor();

            Console.Write("[scan]");

            colors.Purple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.WriteLine("Escaneia um diretório/arquivo mostrando o tamanho total");
            Console.ResetColor();

            Console.Write("[open-site]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.WriteLine("Acessa um site através do DNS");
            Console.ResetColor();

            Console.Write("[open]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.WriteLine("Abre uma pasta ou um arquivo específico");
            Console.ResetColor();

            Console.Write("[create-folder]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.WriteLine("Cria uma pasta em um diretório específico");
            Console.ResetColor();

            Console.Write("[delete-folder]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.WriteLine("Deleta uma pasta em um diretório especifico");
            Console.ResetColor();

            Console.Write("[create-file]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.WriteLine("Cria um arquivo em um diretório especifico");
            Console.ResetColor();   

            Console.Write("[delete-file]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.WriteLine("Deleta um arquivo em um diretório especifico");
            Console.ResetColor();

            Console.WriteLine();
        }

        public void CommandSintaxe()
        {
            Console.Write("[list]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.Write("<diretório>");
            colors.Blue();

            Console.WriteLine(" <list>");
            Console.ResetColor();

            Console.Write("[scan]");

            colors.Purple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.Write("<diretório>");
            colors.Blue();

            Console.WriteLine(" <scan>");
            colors.Purple();

            Console.ResetColor();

            Console.Write("[open-site]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.Blue();
            Console.Write("<open-site> ");

            colors.DarkGray();
            Console.WriteLine("<site/nome de domínio>");
            Console.ResetColor();

            Console.Write("[open]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.Write("<diretório>");

            colors.Blue();
            Console.Write(" <open> ");
            colors.DarkGray();

            Console.WriteLine("<arquivo/pasta>");
            Console.ResetColor();

            Console.Write("[create-folder]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.Write("<diretório>");

            colors.Blue();
            Console.Write(" <create-folder> ");
            colors.DarkGray();

            Console.WriteLine("<nome da pasta>");
            Console.ResetColor();

            Console.Write("[delete-folder]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.Write("<diretório>");

            colors.Blue();
            Console.Write(" <delete-folder> ");

            colors.DarkGray();
            Console.WriteLine("<nome da pasta>");
            Console.ResetColor();

            Console.Write("[create-file]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.Write("<diretório>");
            colors.Blue();

            Console.Write(" <create-file> ");
            colors.DarkGray();

            Console.WriteLine("<nome do arquivo>");
            Console.ResetColor();

            Console.Write("[delete-file]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.Write("<diretório>");
            colors.Blue();

            Console.Write(" <delete-file> ");
            colors.DarkGray();

            Console.WriteLine("<nome do arquivo>");
            Console.ResetColor();

            Console.WriteLine();

        }
    }
}
