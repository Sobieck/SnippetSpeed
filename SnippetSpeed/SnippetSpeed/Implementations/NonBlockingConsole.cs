using SnippetSpeed.Interfaces;
using System;
using System.Threading.Tasks;

namespace SnippetSpeed
{
    internal class NonBlockingConsole : INonBlockingConsole
    {
        public ulong Iterations { private get; set; }

        public TimeSpan TimeElapsed { private get; set; }

        public void Run()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    var timeLeftInMs = SnippetSpeed.Settings.LengthOfOneTestRound - TimeElapsed;

                    var output = string.Format("\rRun: {0:N0} times | MS Left {1:N0}", Iterations, timeLeftInMs);
                    Console.Out.WriteAsync(output);
                }
            });
        }
    }
}
