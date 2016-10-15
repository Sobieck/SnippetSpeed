using System;

namespace SnippetSpeed
{
    public class SnippetSpeedSettings
    {
        private string outputPathAndFileName;

        public TimeSpan LengthOfOneTestRound { get; set; }

        public string OutputPathAndFileName
        {
            get
            {
                if (outputPathAndFileName != null)
                {
                    return outputPathAndFileName;
                }
                else
                {
                    var time = StartOfExecution
                        .ToLocalTime()
                        .ToString()
                        .Replace(' ', '-')
                        .Replace('/', '-')
                        .Replace(':','-');

                    return $"result-{time}.csv";
                }
            }
            set { outputPathAndFileName = value; }
        }

        internal DateTime StartOfExecution { get; set; }

        public SnippetSpeedSettings()
        {
            LengthOfOneTestRound = new TimeSpan(0, 5, 0);
            StartOfExecution = DateTime.Now;
        }
    }
}
