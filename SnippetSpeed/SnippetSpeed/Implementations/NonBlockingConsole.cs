using SnippetSpeed.Interfaces;
using System;
using System.Threading.Tasks;

namespace SnippetSpeed
{
    internal class NonBlockingConsole : INonBlockingConsole
    {
        public ulong Iterations { private get; set; }

        public int TimeInMillisecondsToRun { private get; set; }

        public double MillisecondsElapsed { private get; set; }

        public NonBlockingConsole()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    var output = string.Format("\rRun: {0:N0} times | MS Left {1:N0}", Iterations, TimeInMillisecondsToRun - MillisecondsElapsed);
                    Console.Out.WriteAsync(output);
                }
            });
        }
    }
}
