using System;

namespace SnippetSpeed
{
    public class SnippetSpeedTestResult
    {
        public string TypeOfTest { get; set; }
        public string NameOfClass { get; set; }
        public ulong Interations { get; set; }
        public TimeSpan LengthOfTest { get; set; }

        public int AverageTimeOfIterationInNanoseconds
        {
            get
            {
                return (int)((LengthOfTest.Ticks / (decimal)Interations) * 100);
            }
        }
    }
}
