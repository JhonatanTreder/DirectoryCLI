using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Commands
{
    internal class Scan : CommandsConfig
    {
        public long Execute(string directory)
        {
            long size = 0;

            string[] files = Directory.GetFiles(directory);

            foreach(string file in files)
            {
                size += new FileInfo(file).Length;
            }

            string[] directories = Directory.GetDirectories(directory);

            foreach (string folder in directories)
            {
                size += Execute(folder);
            }

            return size;
        }

        public string FormatBytes(long size)
        {
            string[] suffixes = { "B", "KB", "MB", "GB", "TB" };

            int index = 0;

            long bytes = size;

            for (; bytes >= 1024 && index < suffixes.Length - 1; index++)
            {
                bytes /= 1024;
            }

            return $" {suffixes[index]}";
        }
    }
}
