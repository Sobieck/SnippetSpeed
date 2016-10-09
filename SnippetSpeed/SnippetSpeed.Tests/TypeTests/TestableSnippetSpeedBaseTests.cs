using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnippetSpeed.Interfaces;
using SnippetSpeed.Tests.TypesForRegistryOfTypesTest;

namespace SnippetSpeed.Tests
{
    [TestClass]
    public class TestableSnippetSpeedBaseTests
    {
        private IConsoleWrapper console;

        [TestMethod]
        public void TheActShouldCallTheTestableAct()
        {
            var instanceOfAClassThatImplementsTestableBase = new ASpeedTest3();

            console = A.Fake<IConsoleWrapper>();

            instanceOfAClassThatImplementsTestableBase.console = console;

            instanceOfAClassThatImplementsTestableBase.Act();

            A.CallTo(() => console.WriteLine(instanceOfAClassThatImplementsTestableBase.TypeOfTest)).MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}
