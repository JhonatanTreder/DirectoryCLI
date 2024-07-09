using DirectoryCLI.CommandStyles;
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
        public static void Execute(string directory)
        {

            Console.Write("Tamanho do diretório: ");

            colors.DarkGray();
            Console.WriteLine(FormatBytes(Storage(directory)));
            Console.WriteLine();

            Colors.WhiteText();
        }

        private static long Storage(string directory)
        {
            long size = 0;

            string[] files = Directory.GetFiles(directory);

            foreach (string file in files)
            {
                size += new FileInfo(file).Length;
            }

            string[] directories = Directory.GetDirectories(directory);

            foreach (string folder in directories)
            {
                size += Storage(folder);
            }

            return size;
        }

        private static string FormatBytes(long bytes)
        {
            if (bytes >= 0x1000000000000000) { return ((double)(bytes >> 50) / 1024).ToString("0.### EB"); }
            if (bytes >= 0x4000000000000) { return ((double)(bytes >> 40) / 1024).ToString("0.### PB"); }
            if (bytes >= 0x10000000000) { return ((double)(bytes >> 30) / 1024).ToString("0.### TB"); }
            if (bytes >= 0x40000000) { return ((double)(bytes >> 20) / 1024).ToString("0.### GB"); }
            if (bytes >= 0x100000) { return ((double)(bytes >> 10) / 1024).ToString("0.### MB"); }
            if (bytes >= 0x400) { return ((double)(bytes) / 1024).ToString("0.###") + " KB"; }

            return bytes.ToString("0 Bytes");
        }
    }
}
