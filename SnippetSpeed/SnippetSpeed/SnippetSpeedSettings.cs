using System;

namespace SnippetSpeed
{
    public class SnippetSpeedSettings
    {
        private string outputWritePath;

        public TimeSpan LengthOfOneTestRound { get; set; }

        public string OutputWritePath
        {
            get
            {
                if (outputWritePath != null)
                {
                    return outputWritePath;
                }
                else
                {
                    var time = StartOfExecution.ToLocalTime().ToString().Replace(' ', '-');
                    return $"result-{time}.csv";
                }
            }
            set { outputWritePath = value; }
        }

        internal DateTime StartOfExecution { get; set; }

        public SnippetSpeedSettings()
        {
            LengthOfOneTestRound = new TimeSpan(0, 5, 0);
            StartOfExecution = DateTime.Now;
        }
    }
}
