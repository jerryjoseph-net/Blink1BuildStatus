using System;
using Blink1BuildStatus.Core.Interfaces;

namespace Blink1BuildStatus.UI
{
    public class ConsoleLog : ILog
    {
        public void Info(string message)
        {
            CreateStandardLog(message);
        }

        public void Heading(string message)
        {
            Console.WriteLine();
            Console.WriteLine("".PadRight(80, '*'));
            CreateStandardLog(message);
            Console.WriteLine("".PadRight(80, '*'));
            Console.WriteLine();
        }

        public void Error(string message)
        {
            LogWithColour(message, ConsoleColor.Red);
        }

        public void Success(string message)
        {
            LogWithColour(message, ConsoleColor.Green);
        }

        public void Warning(string message)
        {
            LogWithColour(message, ConsoleColor.Yellow);
        }

        private void LogWithColour(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            CreateStandardLog(message);
            Console.ResetColor();
        }

        private void CreateStandardLog(string message)
        {
            Console.WriteLine($"[{DateTime.Now:u}] {message}");
        }
    }
}
