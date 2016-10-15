using System;

namespace SnippetSpeed.Interfaces
{
    internal interface INonBlockingConsole
    {
        ulong Iterations { set; }

        TimeSpan TimeElapsed { set; }
    }
}