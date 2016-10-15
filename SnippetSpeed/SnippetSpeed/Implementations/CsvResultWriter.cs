using System.Collections.Generic;
using SnippetSpeed.Interfaces;

namespace SnippetSpeed.Implementations
{
    internal class CsvResultWriter : IResultWriter
    {
        private IFileWrapper fileWrapper;

        public CsvResultWriter(IFileWrapper fileWrapper)
        {
            this.fileWrapper = fileWrapper;
        }

        public void Write(ICollection<SnippetSpeedTestResult> results)
        {
            var writeList = new List<string> { "ClassName,TypeOfTest,Iterations,NanosecondsPerIteration,LengthOfTest(ms)" };

            foreach (var result in results) 
            {
                writeList.Add($"{result.NameOfClass},{result.TypeOfTest},{result.Interations},{result.AverageTimeOfIterationInNanoseconds},{(int)result.LengthOfTest.TotalMilliseconds}");
            }
            
            fileWrapper.WriteAllLines(SnippetSpeedConsoleInterface.Settings.OutputWritePath, writeList.ToArray());
        }
    }
}
