using SnippetSpeed.Implementations;
using SnippetSpeed.Interfaces;

using System.Linq;

namespace SnippetSpeed
{
    public class SnippetSpeed
    {
        private static SnippetSpeedSettings settings;

        internal IConsoleWrapper Console { get; set; }
        internal ISnippetIterator Iterator { get; set; }

        public IResultWriter ResultWriter { get; set; }

        public static SnippetSpeedSettings Settings
        {
            get
            {
                if (settings == null)
                {
                    return new SnippetSpeedSettings();
                }
                else
                {
                    return settings;
                }
            }
            set { settings = value; }
        }
                
        public SnippetSpeed()
        {
            Console = new ConsoleWrapper();
            Iterator = new SnippetIterator();

            ResultWriter = new CsvResultWriter();
        }

        public void Run()
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
