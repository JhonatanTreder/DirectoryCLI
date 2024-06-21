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

            Console.Write("[zip]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.WriteLine("Zipa um arquivo ou pasta para um arquivo zipado de destino");
            Colors.WhiteText();

            Console.Write("[move]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.WriteLine("Move um arquivo ou pasta para um diretório de destino");
            Colors.WhiteText();

            Console.Write("[list]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.WriteLine("Lista todos os arquivos/pastas em um diretório");
            Colors.WhiteText();

            Console.Write("[scan]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.WriteLine("Escaneia um diretório/arquivo mostrando o tamanho total");
            Colors.WhiteText();

            Console.Write("[open]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.WriteLine("Abre uma pasta ou um arquivo específico");
            Colors.WhiteText();

            Console.Write("[exit]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.WriteLine("Encerra o programa");
            Colors.WhiteText();

            Console.Write("[clear]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.WriteLine("Limpa a tela do terminal");
            Colors.WhiteText();

            Console.Write("[rename]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.WriteLine("Renomeia um arquivo ou pasta");
            Colors.WhiteText();

            Console.Write("[extract]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.WriteLine("Extrai o conteúdo de um arquivo zipado para um diretório de destino");
            Colors.WhiteText();

            Console.Write("[open-site]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.WriteLine("Acessa um site através do DNS");
            Colors.WhiteText();

            Console.Write("[create-file]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.WriteLine("Cria um arquivo em um diretório especifico");
            Colors.WhiteText();   

            Console.Write("[delete-file]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.WriteLine("Deleta um arquivo em um diretório especifico");
            Colors.WhiteText();

            Console.Write("[create-folder]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.WriteLine("Cria uma pasta em um diretório específico");
            Colors.WhiteText();

            Console.Write("[delete-folder]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.WriteLine("Deleta uma pasta em um diretório especifico");
            Colors.WhiteText();

            Console.WriteLine();
        }

        public void CommandSintaxe()
        {
            Console.Write("[zip]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.Write("<directory>");

            colors.Blue();
            Console.Write(" <zip> ");

            colors.DarkGray();
            Console.WriteLine("<file/folder> to <zip archive>");
            Colors.WhiteText();

            Console.Write("[move]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.Write("<diretório>");

            colors.Blue();
            Console.Write(" <move> ");

            colors.DarkGray();
            Console.WriteLine("<arquivo/pasta> to <diretório de destino>");
            Colors.WhiteText();

            Console.Write("[list]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.Write("<diretório>");
            colors.Blue();

            Console.WriteLine(" <list>");
            Colors.WhiteText();

            Console.Write("[scan]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.Write("<diretório>");
            colors.Blue();

            Console.WriteLine(" <scan>");
            colors.DarkPurple();

            Colors.WhiteText();

            Console.Write("[open]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.Write("<diretório>");

            colors.Blue();
            Console.Write(" <open> ");

            colors.DarkGray();
            Console.WriteLine("<arquivo/pasta>");
            Colors.WhiteText();

            Console.Write("[exit]");

            colors.Purple();
            Console.Write(" - ");

            colors.Blue();
            Console.WriteLine("<exit>");
            Colors.WhiteText();

            Console.Write("[clear]");

            colors.Purple();
            Console.Write(" - ");

            colors.Blue();
            Console.WriteLine("<clear>");
            Colors.WhiteText();

            Console.Write("[rename]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.Write("<diretório>");

            colors.Blue();
            Console.Write(" <rename>");

            colors.DarkGray();
            Console.WriteLine(" <arquivo/pasta> to <nome final>");
            Colors.WhiteText();

            Console.Write("[extract]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.Write("<diretório>");

            colors.Blue();
            Console.Write(" <extract> ");

            colors.DarkGray();
            Console.WriteLine("<arquivo zip> to <diretório final>");
            Colors.WhiteText();

            Console.Write("[open-site]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.Blue();
            Console.Write("<open-site> ");

            colors.DarkGray();
            Console.WriteLine("<site/nome de domínio>");
            Colors.WhiteText();

            Console.Write("[create-file]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.Write("<diretório>");
            colors.Blue();

            Console.Write(" <create-file> ");
            colors.DarkGray();

            Console.WriteLine("<nome do arquivo>");
            Colors.WhiteText();

            Console.Write("[delete-file]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.Write("<diretório>");
            colors.Blue();

            Console.Write(" <delete-file> ");
            colors.DarkGray();

            Console.WriteLine("<nome do arquivo>");
            Colors.WhiteText();

            Console.Write("[create-folder]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.Write("<diretório>");

            colors.Blue();
            Console.Write(" <create-folder> ");
            colors.DarkGray();

            Console.WriteLine("<nome da pasta>");
            Colors.WhiteText();

            Console.Write("[delete-folder]");

            colors.DarkPurple();
            Console.Write(" - ");

            colors.DarkGray();
            Console.Write("<diretório>");

            colors.Blue();
            Console.Write(" <delete-folder> ");

            colors.DarkGray();
            Console.WriteLine("<nome da pasta>");
            Colors.WhiteText();

            Console.WriteLine();
        }
    }
}
