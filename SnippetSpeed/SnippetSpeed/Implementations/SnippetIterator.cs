using System.Collections.Generic;
using SnippetSpeed.Interfaces;
using System.Linq;
using System.Diagnostics;

namespace SnippetSpeed.Implementations
{
    internal class SnippetIterator : ISnippetIterator
    {
        private IConsoleWrapper console;
        private INonBlockingConsole nonBlockingConsole;

        public SnippetIterator(IConsoleWrapper console, INonBlockingConsole nonBlockingConsole)
        {
            this.console = console;
            this.nonBlockingConsole = nonBlockingConsole;
        }

        public ICollection<SnippetSpeedTestResult> Iterate(string usersInput)
        {
            var result = new List<SnippetSpeedTestResult>();
                                    
            if (usersInput.ToLower() == "a")
            {
                return RunAllSpeedTests(result);
            }
                        
            if (IsValidUserInput(usersInput))
            {
                console.WriteLine("There is no record of that test to run. Did you mistype?");
                return result;
            }
            else
            {
                RunTestAndAddResultToList(result, GetTestToRun(usersInput));
            }

            return result;
        }
        
        private ICollection<SnippetSpeedTestResult> RunAllSpeedTests(List<SnippetSpeedTestResult> result)
        {
            var testNumber = 0;
            while (testNumber < RegisterOfTypes.DictoraryOfTypes.Count())
            {
                var currentTestToRun = RegisterOfTypes.DictoraryOfTypes.ElementAt(testNumber);
                RunTestAndAddResultToList(result, currentTestToRun);
                testNumber++;
            }
            return result;
        }

        private void RunTestAndAddResultToList(List<SnippetSpeedTestResult> result, KeyValuePair<string, SnippetSpeedBase> testToRun)
        {
            var resultOfTestRun = RunTestScenario(testToRun);
            result.Add(resultOfTestRun);
        }

        private SnippetSpeedTestResult RunTestScenario(KeyValuePair<string, SnippetSpeedBase> testCase)
        {
            console.WriteLine("\nRunning: " + testCase.Key);

            var testObject = testCase.Value;

            var count = IterateOverTestObject(testObject);

            return new SnippetSpeedTestResult
            {
                Interations = count,
                NameOfClass = testObject.GetType().Name,
                TypeOfTest = testObject.TypeOfTest,
                LengthOfTest = SnippetSpeedConsoleInterface.Settings.LengthOfOneTestRound
            };
        }

        private ulong IterateOverTestObject(SnippetSpeedBase testObject)
        {
            var timeToRun = SnippetSpeedConsoleInterface.Settings.LengthOfOneTestRound;

            ulong count = 0;

            var sw = Stopwatch.StartNew();

            while (sw.Elapsed < timeToRun)
            {
                testObject.Act();
                count++;
                nonBlockingConsole.Iterations = count;
                nonBlockingConsole.TimeElapsed = sw.Elapsed;
            }

            return count;
        }

        private bool IsValidUserInput(string usersInput)
        {
            KeyValuePair<string, SnippetSpeedBase> testToRun = GetTestToRun(usersInput);

            int y;

            return testToRun.Key == null || int.TryParse(usersInput, out y) == false;
        }

        private static KeyValuePair<string, SnippetSpeedBase> GetTestToRun(string usersInput)
        {
            return RegisterOfTypes.DictoraryOfTypes.FirstOrDefault(x => x.Key.Contains(usersInput));
        }
    }
}
