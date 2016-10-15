using System.Collections.Generic;

namespace SnippetSpeed.Interfaces
{
    internal interface ISnippetIterator
    {
        ICollection<SnippetSpeedTestResult> Iterate(string usersInput);
    }
}
