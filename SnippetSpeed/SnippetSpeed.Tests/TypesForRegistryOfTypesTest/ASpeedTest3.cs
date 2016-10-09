using SnippetSpeed;
using System;

namespace SnippetSpeed.Tests.TypesForRegistryOfTypesTest
{
    class ASpeedTest3 : TestableSnippetSpeedBase<int>
    {
        public override string TypeOfTest => "x";

        public override int TestableAct()
        {
            throw new NotImplementedException();
        }
    }
}
