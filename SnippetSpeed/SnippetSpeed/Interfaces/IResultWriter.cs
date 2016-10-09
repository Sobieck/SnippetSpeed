using System.Collections.Generic;

namespace SnippetSpeed.Interfaces
{
    public interface IResultWriter
    {
        void Write(ICollection<SnippetSpeedTestResult> results);
    }
}
