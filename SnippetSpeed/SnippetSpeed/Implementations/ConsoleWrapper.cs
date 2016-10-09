using System;
using SnippetSpeed.Interfaces;

namespace SnippetSpeed.Implementations
{
    internal class ConsoleWrapper : IConsoleWrapper
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}