using Spectre.Console;

namespace DirectoryCLI.Handlers
{
    internal class CommandsInfo
    {

        //Método para mostrar as informações de todos os comandos.
        public static void Commands()
        {
            //Uso do 'OutputEncoding' para garantir a renderização correta da tabela.
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var table = new Table();

            table.AddColumn(new TableColumn("Comando") { Width = 15 });
            table.AddColumn(new TableColumn("Uso") { Width = 97 });

            table.AddRow(Markup.Escape("[zip]"), "Zipa um arquivo ou pasta para um arquivo .zip de destino.");
            table.AddRow(Markup.Escape("[move]"), "Move um arquivo ou pasta para um diretório de destino.");
            table.AddRow(Markup.Escape("[list]"), Markup.Escape("Lista o conteúdo de um diretório/caminho especificado [ parâmetro opicional: <-e> ]."));
            table.AddRow(Markup.Escape("[scan]"), "Mostra o tamanho total de um caminho especificado.");
            table.AddRow(Markup.Escape("[open]"), Markup.Escape("Abre um arquivo ou pasta de um diretório especificado [ parâmetro opicional: <--this> ]."));
            table.AddRow(Markup.Escape("[exit]"), "Fecha o programa.");
            table.AddRow(Markup.Escape("[clear]"), "Limpa o terminal.");
            table.AddRow(Markup.Escape("[rename]"), "Renomeia um arquivo/pasta (podendo alterar sua extensão).");
            table.AddRow(Markup.Escape("[extract]"), "Extrai o conteúdo de um arquivo zipado para um diretório especificado.");
            table.AddRow(Markup.Escape("[open-site]"), "Abre um site através do seu DNS.");
            table.AddRow(Markup.Escape("[create-file]"), "Cria um ou mais arquivos em um diretório específico.");
            table.AddRow(Markup.Escape("[delete-file]"), "Deleta um ou mais arquivos de um diretório específico.");
            table.AddRow(Markup.Escape("[create-folder]"), "Cria uma pasta em um diretório específico.");
            table.AddRow(Markup.Escape("[delete-folder]"), "Deleta uma pasta de um diretório específico.");
            table.AddRow(Markup.Escape("[del-files]"), "Deleta todos os arquivos de um diretório específico (podendo especificar o tipo de ).");
            table.AddRow(Markup.Escape("[del-folders]"), "Deleta todas as pastas de um diretório específico.");
            table.AddRow(Markup.Escape("[system-info]"), "Fornece informações sobre o computador (Armazenamento, Processador, Placa de Vídeo, etc...).");

            AnsiConsole.Write(table);
            AnsiConsole.WriteLine();
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //Método para mostrar a sintaxe dos comandos.
        public static void CommandSintaxe()
        {
            //Uso do 'OutputEncoding' para garantir a renderização correta da tabela.
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var table = new Table();

            table.AddColumn(new TableColumn("Comando") { Width = 15 });
            table.AddColumn(new TableColumn("Uso") { Width = 97 });

            table.AddRow(Markup.Escape("[zip]"), "<diretório> <zip> <arquivo/pasta> <to> <diretório final>");
            table.AddRow(Markup.Escape("[move]"), "<diretório> <move> <arquivo/pasta> <to> <diretório final>");
            table.AddRow(Markup.Escape("[list]"), Markup.Escape("<diretório> <list> [ opicional: <-e> ]"));
            table.AddRow(Markup.Escape("[scan]"), "<diretório> <scan>");
            table.AddRow(Markup.Escape("[open]"), Markup.Escape("<diretório> <open> [ <arquivo/pasta> ou <--this> ]"));
            table.AddRow(Markup.Escape("[exit]"), "<exit>");
            table.AddRow(Markup.Escape("[clear]"), "<clear>");
            table.AddRow(Markup.Escape("[rename]"), "<diretório> <rename> <arquivo/pasta> <to> <nome final>");
            table.AddRow(Markup.Escape("[extract]"), "<diretório> <extract> <arquivo .zip> <to> <diretório final>");
            table.AddRow(Markup.Escape("[open-site]"), "<open-site> <nome exemplo de DNS>");
            table.AddRow(Markup.Escape("[create-file]"), "<diretório> <create-file> <arquivo> (pode especificar mais de um arquivo)");
            table.AddRow(Markup.Escape("[delete-file]"), "<diretório> <delete-file> <arquivo> (pode especificar mais de um arquivo)");
            table.AddRow(Markup.Escape("[create-folder]"), "<diretório> <create-folder> <pasta> (pode especificar mais de uma pasta)");
            table.AddRow(Markup.Escape("[delete-folder]"), "<diretório> <delete-folder> <pasta> (pode especificar mais de uma pasta)");
            table.AddRow(Markup.Escape("[del-files]"), Markup.Escape("<diretório> <del-files> [ opicional: <tipo de extensão>, <--null> ]"));
            table.AddRow(Markup.Escape("[del-folders]"), "<diretório> <del-folders>.");
            table.AddRow(Markup.Escape("[system-info]"), "<system-info>");

            AnsiConsole.Write(table);
            AnsiConsole.WriteLine();
        }

        public static void DirectoryCommands()
        {
            Table table = new Table();

            table.AddColumn("Comando");
            table.AddColumn("Sintaxe");

            table.AddRow(Markup.Escape("[zip]"), "<diretório> <zip> <arquivo/pasta> <to> <diretório final>");
            table.AddRow(Markup.Escape("[move]"), "<diretório> <move> <arquivo/pasta> <to> <diretório final>");
            table.AddRow(Markup.Escape("[list]"), Markup.Escape("<diretório> <list> [ opicional: <-e> ]"));
            table.AddRow(Markup.Escape("[scan]"), "<diretório> <scan>");
            table.AddRow(Markup.Escape("[open]"), Markup.Escape("<diretório> <open> [ <arquivo/pasta> ou <--this> ]"));
            table.AddRow(Markup.Escape("[rename]"), "<diretório> <rename> <arquivo/pasta> <to> <nome final>");
            table.AddRow(Markup.Escape("[extract]"), "<diretório> <extract> <arquivo .zip> <to> <diretório final>");
            table.AddRow(Markup.Escape("[create-file]"), "<diretório> <create-file> <arquivo> (pode especificar mais de um arquivo)");
            table.AddRow(Markup.Escape("[delete-file]"), "<diretório> <delete-file> <arquivo> (pode especificar mais de um arquivo)");
            table.AddRow(Markup.Escape("[create-folder]"), "<diretório> <create-folder> <pasta> (pode especificar mais de uma pasta)");
            table.AddRow(Markup.Escape("[delete-folder]"), "<diretório> <delete-folder> <pasta> (pode especificar mais de uma pasta)");
            table.AddRow(Markup.Escape("[del-files]"), Markup.Escape("<diretório> <del-files> [ opicional: <tipo de extensão>, <--null> ]"));
            table.AddRow(Markup.Escape("[del-folders]"), "<diretório> <del-folders>.");

            AnsiConsole.Write(table);
            AnsiConsole.WriteLine();

        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}
