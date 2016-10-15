using System.Collections.Generic;
using SnippetSpeed.Interfaces;

namespace SnippetSpeed.Implementations
{
    internal class CsvResultWriter : IResultWriter
    {
        public void Write(ICollection<SnippetSpeedTestResult> results)
        {

            //create public interface

            //var averageTime = timeInMillisecondsToRun / (float)count;

            //var binaryVersion = File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString("yyyy.MM.dd.HHmm");

            //var lineToWrite = string.Format("|{0}|{1}|{2}|{3:N0}|{4:N5}|", binaryVersion, dictonaryItemToActOn.Value.TypeOfTest, dictonaryItemToActOn.Key, count, averageTime);

            //using (var file = new StreamWriter(@"C:\GitHub\SpeedTest\README.md", true))
            //{
            //    file.WriteLine(lineToWrite);
            //}
        }
    }
}
