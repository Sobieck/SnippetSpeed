using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SnippetSpeed
{
    public class SnippetSpeed
    {
        public static void Run()
        {
            Console.WriteLine("Please select the number from the following list of possible tests:");
            Console.WriteLine("(a) ALL TESTS. " + RegisterOfTypes.DictoraryOfTypes.Count() * 5 + " minute running time");

            foreach (var item in RegisterOfTypes.DictoraryOfTypes)
            {
                Console.WriteLine(item.Key);
            }

            var selection = Console.ReadLine();

            if (selection == "a")
            {
                var testNumber = 0;
                while (testNumber < RegisterOfTypes.DictoraryOfTypes.Count())
                {
                    RunTestScenario(testNumber.ToString());
                    testNumber++;
                }
            }
            else
            {
                RunTestScenario(selection);
            }
        }

        private static void RunTestScenario(string selection)
        {
            var dictonaryItemToActOn = RegisterOfTypes.DictoraryOfTypes.First(x => x.Key.Contains(selection));

            Console.WriteLine("\nRunning:" + dictonaryItemToActOn.Key);

            ulong count = 0;

            var timeInMillisecondsToRun = 300000; //300000;

            NonBlockingConsole.TimeInMillisecondsToRun = timeInMillisecondsToRun;
            
            var sw = Stopwatch.StartNew();

            while (sw.Elapsed.TotalMilliseconds < timeInMillisecondsToRun)
            {
                dictonaryItemToActOn.Value.Act();
                count++;
                NonBlockingConsole.Iterations = count;
                NonBlockingConsole.MillisecondsElapsed = sw.Elapsed.TotalMilliseconds;
            }

#if (!DEBUG)
            var averageTime = timeInMillisecondsToRun / (float)count;

            var binaryVersion = File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString("yyyy.MM.dd.HHmm");

            var lineToWrite = string.Format("|{0}|{1}|{2}|{3:N0}|{4:N5}|", binaryVersion, dictonaryItemToActOn.Value.TypeOfTest, dictonaryItemToActOn.Key, count, averageTime);

            using (var file = new StreamWriter(@"C:\GitHub\SpeedTest\README.md", true))
            {
                file.WriteLine(lineToWrite);
            }
#endif
        }
    }
}
