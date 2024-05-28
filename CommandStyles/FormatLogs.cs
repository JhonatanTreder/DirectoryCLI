using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.CommandStyles
{
    internal class FormatLogs
    {
        readonly Colors colors = new Colors();
        public void UserAndMachineName()
        {
            Console.ResetColor();

            colors.Blue();

            Console.Write("#");

            colors.DarkPurple();

            Console.WriteLine(Environment.UserName + " - " + Environment.MachineName);
            Console.ResetColor();
        }

        public void DirectoryLog()
        {
            Console.ResetColor();
            Console.WriteLine();

            colors.DarkGray();

            Console.WriteLine("arguments used: <directory> <command> <conclusion>");
            Console.WriteLine();
        }

        public void SiteLog()
        {
            colors.DarkGray();

            Console.WriteLine("arguments used: <command> <site/domain name>");
            Console.ResetColor();
        }

        public void ScanAndListLogs()
        {
            colors.DarkGray();

            Console.WriteLine("arguments used: <directory> <command>");
            Console.WriteLine();
        }

        public void CommandLog()
        {
            colors.DarkGray();

            Console.WriteLine("argument used: <command>");
            Console.WriteLine();
        }
    }
}
