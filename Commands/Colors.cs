using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Commands
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
    }
}
