using System;

namespace SnippetSpeed.Tests.TypesForRegistryOfTypesTest
{
    public class SpeedTest2 : AbstractSpeedBase1
    {
        public override string TypeOfTest => "a";

        public override void Act()
        {
            var asdf = "asd";
        }
    }
}
