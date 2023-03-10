using System;
using System.Collections.Generic;
using System.Text;

namespace LogServices
{
    public class ConsoleLogProvider : ILogProvider
    {
        public void LogError(string message)
        {
            Console.WriteLine($"ERROR: {message}");
        }

        public void LogWarning(string message)
        {
            Console.WriteLine($"WARNING: {message}");
        }
    }
}
