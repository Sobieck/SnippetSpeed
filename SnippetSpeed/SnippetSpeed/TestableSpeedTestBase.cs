namespace SnippetSpeed
{
    public abstract class TestableSpeedTestBase<T> : SpeedTestBase where T : class
    {
        public abstract T TestableAct();

        public override void Act()
        {
            TestableAct();
        }
    }
}
