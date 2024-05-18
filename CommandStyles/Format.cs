using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.CommandStyles
{
    internal class Format
    {
        readonly Colors colors = new Colors();
        public void UserAndMachineName()
        {
            Console.ResetColor();
            colors.Blue();
            Console.Write("#");
            colors.DarkPurple();
            Console.WriteLine(System.Environment.UserName + " - " + System.Environment.MachineName);
            Console.ResetColor();
        }

        public void DirectoryLog()
        {
            Console.ResetColor();
            Console.WriteLine();
            colors.Yellow();
            Console.WriteLine("arguments used: <directory> <command> <conclusion>");
        }

        public void SiteLog()
        {
            colors.Yellow();
            Console.WriteLine("arguments used: <command> <site/domain name>");
            Console.ResetColor();
        }

        public void ScanAndListLogs()
        {
            colors.Yellow();
            Console.WriteLine("arguments used: <directory> <command>");
        }

        public void CommandLog()
        {
            colors.Yellow();
            Console.WriteLine("argument used: <command>");
        }
    }
}
