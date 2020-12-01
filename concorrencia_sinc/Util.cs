using System;
using System.Collections.Generic;
using System.Text;

namespace concorrencia_sinc
{
    public class Util
    {
        //Exibe mensagem e retorna entrada digitada pelo usuario 
        public static string ConsoleInOut(string text)
        {
            Console.WriteLine(text);
            return Console.ReadLine();
        }

        //Mensagem na tela personalizada com cor
        public static void ConsoleMessage(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        //Gera valores double de forma randomica entre um min e max 
        public static double RandomVal(double min, double max)
        {
            double val = new Random().NextDouble() * (max - min) + min;
            return val;
        }
    }
}
