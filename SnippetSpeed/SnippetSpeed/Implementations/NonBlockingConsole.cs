using System;
using System.Threading.Tasks;

namespace SnippetSpeed
{
    internal static class NonBlockingConsole
    {
        public static ulong Iterations { private get; set; }

        public static int TimeInMillisecondsToRun { private get; set; }

        public static double MillisecondsElapsed { private get; set; }

        static NonBlockingConsole()
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
