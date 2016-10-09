namespace SnippetSpeed
{
    public abstract class TestableSnippetSpeedBase<T> : SnippetSpeedBase
    {
        public abstract T TestableAct();

        public override void Act()
        {
            TestableAct();
        }
    }
}
