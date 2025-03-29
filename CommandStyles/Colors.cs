//using Spectre.Console;
using System.Drawing;
using Console = Colorful.Console;

namespace DirectoryCLI.CommandStyles
{
    //Classe para facilitar o acessar as cores.
    internal class Colors
    {
        public static void DarkPurple()
        {
            Console.ForegroundColor = Color.Magenta;
        }

        public static void Blue()
        {
            Console.ForegroundColor = Color.RoyalBlue;
        }

        public static void WhiteText()
        {
            Console.ForegroundColor = Color.White;
        }

        public static void BlackBG()
        {
            Console.BackgroundColor = Color.Black;
        }
    }
}
