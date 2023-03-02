using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Core.Helpers
{
    public static class ConsoleHelper
    {
        public static void WriteWithColor(string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (text.Length / 2)) + "}", text)); 
            Console.ResetColor();
        }
        public static void WriteList(List<ConsoleWriter> contents)
        {
            foreach (ConsoleWriter content in contents)
            {
                Console.ForegroundColor = content.Color;
                Console.WriteLine(content.Text);
                Console.ResetColor();
            }
        }
        public static void WriteContinuosly(string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.Write(String.Format("{0," + ((Console.WindowWidth / 2) + (text.Length / 2)) + "}", text));
            Console.ResetColor();
        }
    }
    public class ConsoleWriter
    {
        public ConsoleColor Color { get; set; }
        public string Text { get; set; }
    }
}