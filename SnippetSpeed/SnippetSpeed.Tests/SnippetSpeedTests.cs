using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomTestValues;
using SnippetSpeed.Interfaces;
using System;
using System.Collections.Generic;

namespace SnippetSpeed.Tests
{
    [TestClass]
    public class SnippetSpeedTests
    {
        private IConsoleWrapper consoleWrapper;
        private string usersInput;
        private ISnippetIterator iterator;
        private List<SnippetSpeedTestResult> iteratorResults;
        private IResultWriter resultWriter;
        private string allTestsMessage;

        [TestInitialize]
        public void BeforeEach()
        {
            usersInput = RandomValue.String();

            consoleWrapper = A.Fake<IConsoleWrapper>();
            iterator = A.Fake<ISnippetIterator>();
            resultWriter = A.Fake<IResultWriter>();

            iteratorResults = RandomValue.List<SnippetSpeedTestResult>();
            var lengthOfTests = RandomValue.Int(50);

            allTestsMessage = $"(a) ALL TESTS. {lengthOfTests * 3} minute running time";

            SnippetSpeedConsoleInterface.Settings = new SnippetSpeedSettings { LengthOfOneTestRound = new TimeSpan(0, lengthOfTests, 0) };

            SnippetSpeedConsoleInterface.Console = consoleWrapper;
            SnippetSpeedConsoleInterface.Iterator = iterator;
            SnippetSpeedConsoleInterface.ResultWriter = resultWriter;

            A.CallTo(() => consoleWrapper.ReadLine()).Returns(usersInput);
            A.CallTo(() => iterator.Iterate(usersInput)).Returns(iteratorResults);

            SnippetSpeedConsoleInterface.Run();
        }

        [TestMethod]
        public void TheOrderOfTheCallsIsCorrect()
        {
            A.CallTo(() => consoleWrapper.WriteLine("Please select the number from the following list of possible tests:")).MustHaveHappened()
                .Then(A.CallTo(() => consoleWrapper.WriteLine(allTestsMessage)).MustHaveHappened())
                .Then(A.CallTo(() => consoleWrapper.WriteLine("(0) SpeedTest2")).MustHaveHappened())
                .Then(A.CallTo(() => consoleWrapper.WriteLine("(1) ASpeedTest3")).MustHaveHappened())
                .Then(A.CallTo(() => consoleWrapper.WriteLine("(2) SpeedBase1")).MustHaveHappened())
                .Then(A.CallTo(() => consoleWrapper.ReadLine()).MustHaveHappened())
                .Then(A.CallTo(() => iterator.Iterate(usersInput)).MustHaveHappened())
                .Then(A.CallTo(() => resultWriter.Write(iteratorResults)).MustHaveHappened());
        }

        [TestMethod]
        public void ShouldAskToSelectFromAListOfTests()
        {
            VerifyWriteLineIsCalledWithCorrectArgumentsOnce("Please select the number from the following list of possible tests:");
        }

        [TestMethod]
        public void ShouldHaveAnAllTestPrompt()
        {
            VerifyWriteLineIsCalledWithCorrectArgumentsOnce(allTestsMessage);
        }

        [TestMethod]
        public void ItShouldCorrectlyCallThePossibleTests()
        {
            VerifyWriteLineIsCalledWithCorrectArgumentsOnce("(0) SpeedTest2");
            VerifyWriteLineIsCalledWithCorrectArgumentsOnce("(1) ASpeedTest3");
            VerifyWriteLineIsCalledWithCorrectArgumentsOnce("(2) SpeedBase1");
        }

        [TestMethod]
        public void ACallToReadLineShouldHapped()
        {
            A.CallTo(() => consoleWrapper.ReadLine()).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        public void IteratorShouldBeCalledWithWhateverTheUserEnters()
        {
            A.CallTo(() => iterator.Iterate(usersInput)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        public void TheResultWriteShouldBeCalledWithResultOfTheIterator()
        {
            A.CallTo(() => resultWriter.Write(iteratorResults)).MustHaveHappened(Repeated.Exactly.Once);
        }


        private void VerifyWriteLineIsCalledWithCorrectArgumentsOnce(string message)
        {
            A.CallTo(() => consoleWrapper.WriteLine(message)).MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}
