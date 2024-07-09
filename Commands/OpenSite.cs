using DirectoryCLI.CommandStyles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Commands
{
    internal class OpenSite : CommandsConfig
    {

        public static void Execute(string domain)
        {
            string url = "https://www." + domain + ".com";
            Process.Start( new ProcessStartInfo(url) { UseShellExecute = true });

            colors.Blue();
            Console.Write("Abrindo: ");

            Colors.WhiteText();
            Console.WriteLine($"{FormatWord(domain)}");
            Console.WriteLine();
        }

        static string FormatWord(string word)
        {
            string formatedWord = "";
            formatedWord += char.ToUpper(word[0]);

            for (int i = 1; i < word.Length; i++)
            {
                formatedWord += word[i];
            }

            return formatedWord;
        }
    }
}
