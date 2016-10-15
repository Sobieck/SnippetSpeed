using SnippetSpeed.Implementations;
using SnippetSpeed.Interfaces;

namespace SnippetSpeed.Tests.TypesForRegistryOfTypesTest
{
    class ASpeedTest3 : TestableSnippetSpeedBase<int>
    {
        public override string TypeOfTest => "x";

        public long Iterations = 0;

        internal IConsoleWrapper console = new ConsoleWrapper();

        public override int TestableAct()
        {
            console.WriteLine(TypeOfTest);
            Iterations++;
            return 1;
        }
    }
}
