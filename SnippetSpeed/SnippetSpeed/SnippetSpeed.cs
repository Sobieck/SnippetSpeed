using SnippetSpeed.Implementations;
using SnippetSpeed.Interfaces;

using System.Linq;

namespace SnippetSpeed
{
    public static class SnippetSpeed
    {
        private static SnippetSpeedSettings settings;
        private static IConsoleWrapper console;
        private static ISnippetIterator iterator;
        private static IResultWriter resultWriter;

        public static IResultWriter ResultWriter
        {
            get
            {
                if(resultWriter == null)
                {
                    resultWriter = new CsvResultWriter();
                }
                return resultWriter;
            }
            set { resultWriter = value; }
        }

        internal static IConsoleWrapper Console
        {
            get
            {
                if(console == null)
                {
                    console = new ConsoleWrapper();
                }
                return console;
            }
            set { console = value; }
        }

        internal static ISnippetIterator Iterator
        {
            get
            {
                if(iterator == null)
                {
                    iterator = new SnippetIterator(Console, new NonBlockingConsole());
                }
                return iterator;
            }
            set { iterator = value; }
        }

        public static SnippetSpeedSettings Settings
        {
            get
            {
                if (settings == null)
                {
                    settings = new SnippetSpeedSettings();
                }

                return settings;
            }
            set { settings = value; }
        }

        public static void Run()
        {
            Console.WriteLine("Please select the number from the following list of possible tests:");
            Console.WriteLine($"(a) ALL TESTS. {RegisterOfTypes.DictoraryOfTypes.Count() * Settings.LengthOfOneTestRound.TotalMinutes} minute running time");

            foreach (var item in RegisterOfTypes.DictoraryOfTypes)
            {
                Console.WriteLine(item.Key);
            }

            var selection = Console.ReadLine();

            var result = Iterator.Iterate(selection);

            ResultWriter.Write(result);
        }
    }
}
