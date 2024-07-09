using DirectoryCLI.CommandStyles;
using DirectoryCLI.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Xml.Linq;

namespace DirectoryCLI.Commands
{
    internal class Zip : CommandsConfig
    {
        public Zip()
        {

        }

        public static void Execute(FileInfo directoryPath, string command, string[] arguments)
        {
            List<string> elements = new List<string>();

            try
            {

                for (int i = 2; i  < arguments.Length - 2; i++)
                {
                    FileInfo item = new FileInfo(arguments[i]);
                    FileInfo destiny = new FileInfo(arguments[arguments.Length - 1]);

                    string itemPath = Path.Combine(directoryPath.FullName, item.Name);
                    string destinyPath = Path.Combine(directoryPath.FullName, destiny.Name);

                    //-------------------------------------------------------------------------------------------------------------------------------------------------
                    //Variável para identificar se o usuário digitou outra coisa além de 'to'.
                    bool userArgument = arguments[arguments.Length - 2] != "to";

                    if (userArgument && File.Exists(itemPath))
                    {
                        throw new ArgumentException($"Erro ao tentar zipar um arquivo, '{arguments[arguments.Length - 2]}' não é um identificador válido: ");
                    }

                    else if (userArgument && Directory.Exists(itemPath))
                    {
                        throw new ArgumentException($"Erro ao tentar zipar uma pasta, '{arguments[arguments.Length - 2]}' não é um identificador válido: ");
                    }
                    //-------------------------------------------------------------------------------------------------------------------------------------------------
                    //-----------------------------------------------------------------------
                    //Validação dos dados (item, destiny) - ZIP

                    DataValidation(command, directoryPath, item, destiny);

                    //------------------------------------------------------------------------

                    elements.Add(arguments[i]);

                    using (ZipArchive archive = ZipFile.Open(destinyPath, ZipArchiveMode.Update))
                    {
                        if (File.Exists(itemPath))
                        {
                            archive.CreateEntryFromFile(itemPath, item.Name, CompressionLevel.Optimal);
                        }

                        else if (Directory.Exists(itemPath))
                        {
                            AddDirectoryToZip(archive, itemPath, item.Name);
                        }
                    }
                }

                colors.Blue();

                foreach (string element in elements)
                {
                    Colors.WhiteText();
                    Console.Write("Item:");

                    colors.DarkGray();

                    Console.WriteLine($" [{element}] zipado para [{arguments[0]}\\{arguments[arguments.Length - 1]}]");
                }

            }
            catch (InvalidDestinationPathException ex)
            {
                colors.DarkRed();
                Console.Write("Erro ao zipar um item: ");

                Colors.WhiteText();
                Console.WriteLine(ex.Message);
            }

        }

        private static void AddDirectoryToZip(ZipArchive archive, string sourceDir, string entryName)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(sourceDir);

            if (dirInfo.GetFiles().Length == 0 && dirInfo.GetDirectories().Length == 0)
            {
                archive.CreateEntry($"{entryName}/");
            }

            foreach (FileInfo file in dirInfo.GetFiles())
            {
                string entryPath = Path.Combine(entryName, file.Name);
                archive.CreateEntryFromFile(file.FullName, entryPath);
            }

            foreach (DirectoryInfo subDir in dirInfo.GetDirectories())
            {
                string subDirEntryName = Path.Combine(entryName, subDir.Name);
                AddDirectoryToZip(archive, subDir.FullName, subDirEntryName);

                Console.WriteLine(subDir);
            }
        }
    }
}
