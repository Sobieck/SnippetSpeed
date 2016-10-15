using System;
using System.IO;
using System.Reflection;

namespace SnippetSpeed
{
    public class SnippetSpeedSettings
    {
        private string outputWritePath;
        private string outputFileName;

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
                    var pathOfProject = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                    return pathOfProject;
                }
            }
            set { outputWritePath = value; }
        }

        public string OutputFileName
        {
            get
            {
                if (outputFileName != null)
                {
                    return outputFileName;
                }
                else
                {
                    var time = StartOfExecution.ToLocalTime().ToString().Replace(' ', '-');
                    return $"result-{time}.csv";
                }
            }
            set { outputFileName = value; }
        }

        internal DateTime StartOfExecution { get; set; }

        public SnippetSpeedSettings()
        {
            LengthOfOneTestRound = new TimeSpan(0, 5, 0);
            StartOfExecution = DateTime.Now;
        }
    }
}
