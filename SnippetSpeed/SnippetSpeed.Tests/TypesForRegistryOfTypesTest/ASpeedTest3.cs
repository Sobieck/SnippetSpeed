using SnippetSpeed.Interfaces;

namespace SnippetSpeed.Tests.TypesForRegistryOfTypesTest
{
    class ASpeedTest3 : TestableSnippetSpeedBase<int>
    {
        public override string TypeOfTest => "x";

        internal IConsoleWrapper console;

        public override int TestableAct()
        {
            console.WriteLine(TypeOfTest);
            return 1;
        }
    }
}
