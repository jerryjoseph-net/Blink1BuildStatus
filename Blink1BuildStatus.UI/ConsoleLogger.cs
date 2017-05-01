using System;
using Blink1BuildStatus.Core.Interfaces;

namespace Blink1BuildStatus.UI
{
    public class ConsoleLogger : ILogger
    {
        public void LogInfo(string message)
        {
            Console.WriteLine($"[{DateTime.Now:u}] {message}");
        }
    }
}
