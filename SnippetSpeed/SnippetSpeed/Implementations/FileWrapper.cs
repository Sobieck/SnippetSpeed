using SnippetSpeed.Interfaces;
using System.IO;

namespace SnippetSpeed.Implementations
{
    internal class FileWrapper : IFileWrapper
    {
        public void WriteAllLines(string path, string[] lines)
        {
            File.WriteAllLines(path, lines);
        }
    }
}
