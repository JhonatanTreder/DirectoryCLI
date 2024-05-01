using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.CommandStyles
{
    internal class Colors
    {
        public Colors()
        {

        }

        public void Purple()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
        }

        public void DarkPurple()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
        }

        public void Blue()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        public void Red()
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }

        public void Green()
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }

        public void Yellow()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        public void Orange()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
        }
        public void DarkGray()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
        }

        public void Gray()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
