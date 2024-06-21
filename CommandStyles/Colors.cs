using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace DirectoryCLI.CommandStyles
{
    internal class Colors
    {
        public Colors()
        {

        }

        public void Purple()
        {
            Console.ForegroundColor = Color.Magenta;
        }

        public void DarkPurple()
        {
            Console.ForegroundColor = Color.DarkMagenta;
        }

        public void Blue()
        {
            Console.ForegroundColor = Color.RoyalBlue;
        }

        public void Red()
        {
            Console.ForegroundColor = Color.IndianRed;
        }

        public void DarkRed()
        {
            Console.ForegroundColor = Color.DarkRed;
        }

        public void Green()
        {
            Console.ForegroundColor = Color.LightGreen;
        }

        public void Yellow()
        {
            Console.ForegroundColor = Color.Yellow;
        }

        public void Orange()
        {
            Console.ForegroundColor = Color.Orange;
        }
        public void DarkGray()
        {
            Console.ForegroundColor = Color.DimGray;
        }

        public void Gray()
        {
            Console.ForegroundColor = Color.Gray;
        }

        public static void WhiteText()//Texto branco
        {
            Console.ForegroundColor = Color.White;
        }

        public static void BlackText()//Texto escuro
        {
            Console.ForegroundColor = Color.Black;
        }

        public static void WhiteBG()//Cor de fundo branca
        {
            Console.BackgroundColor = Color.White;
        }

        public static void BlackBG()//Cor de fundo escuro
        {
            Console.BackgroundColor = Color.Black;
        }

        public static void LightBlue()
        {
            Console.ForegroundColor = Color.LightBlue;
        }
    }
}
