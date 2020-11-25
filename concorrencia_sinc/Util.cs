using System;
using System.Collections.Generic;
using System.Text;

namespace concorrencia_sinc
{
    public class Util
    {
        public static string ConsoleInOut(string text)
        {
            Console.WriteLine(text);
            return Console.ReadLine();
        }

        public static void ConsoleMessage(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
