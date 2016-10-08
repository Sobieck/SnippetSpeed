namespace SnippetSpeed
{
    public abstract class SpeedTestBase
    {
        public abstract void Act();

        public abstract string TypeOfTest { get; }
    }
}
