namespace SnippetSpeed.Interfaces
{
    internal interface IFileWrapper
    {
        void WriteAllLines(string path, string[] lines);
    }
}
