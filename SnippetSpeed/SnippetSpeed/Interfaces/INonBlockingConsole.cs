namespace SnippetSpeed.Interfaces
{
    internal interface INonBlockingConsole
    {
        ulong Iterations { set; }

        int TimeInMillisecondsToRun { set; }

        double MillisecondsElapsed { set; }
    }
}