using System;
using System.IO;
using System.IO.Compression;

namespace DirectoryCLI.Commands
{
    internal class Zip : CommandsConfig
    {
        public Zip()
        {

        }

        public void Execute(FileInfo directoryPath, FileInfo item, FileInfo destiny)
        {
            string itemPath = Path.Combine(directoryPath.FullName, item.Name);
            string destinyPath = Path.Combine(directoryPath.FullName, destiny.Name);

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

        private void AddDirectoryToZip(ZipArchive archive, string sourceDir, string entryName)
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
