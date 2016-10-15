using SnippetSpeed.Interfaces;
using System;
using System.Threading.Tasks;

namespace SnippetSpeed
{
    internal class NonBlockingConsole : INonBlockingConsole
    {
        public ulong Iterations { private get; set; }

        public TimeSpan TimeElapsed { private get; set; }

        public NonBlockingConsole()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    var timeLeft = SnippetSpeedConsoleInterface.Settings.LengthOfOneTestRound - TimeElapsed;

                    if(Iterations != 0)
                    {
                        var output = string.Format("\rRun: {0:N0} times | Time Left {1:mm}:{1:ss}:{1:ff}", Iterations, timeLeft);
                        Console.Out.WriteAsync(output);
                    }
                }
            });
        }
    }
}
