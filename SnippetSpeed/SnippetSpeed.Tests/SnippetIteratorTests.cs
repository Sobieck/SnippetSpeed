using FakeItEasy;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomTestValues;
using SnippetSpeed.Implementations;
using SnippetSpeed.Interfaces;
using SnippetSpeed.Tests.TypesForRegistryOfTypesTest;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SnippetSpeed.Tests
{
    [TestClass]
    public class SnippetIteratorTests
    {
        internal SnippetIterator Sut { get; private set; }
        internal IConsoleWrapper console { get; private set; }

        private const string KeyOfItem1 = "(1) ASpeedTest3";
        private const string MissTypeMessage = "There is no record of that test to run. Did you mistype?";
        private FakeNonBlockingConsole nonBlockingConsole;

        [ClassInitialize]
        public static void BeforeAll(TestContext testContext)
        {
            RegisterOfTypesTests.BeforeAll(testContext);
            
        }

        [TestInitialize]
        public void BeforeEach()
        {
            console = A.Fake<IConsoleWrapper>();
            nonBlockingConsole = new FakeNonBlockingConsole();
            Sut = new SnippetIterator(console, nonBlockingConsole);
            SnippetSpeedConsoleInterface.Settings.LengthOfOneTestRound = new TimeSpan(0, 0, 0, 0, 5);
        }

        [TestMethod]
        public void AValueIsPassedInThatIsntInTheRegistery()
        {
            Sut.Iterate("100");
            ConsoleShouldBeOnceCalledWith(MissTypeMessage);
        }

        [TestMethod]
        public void TheConsoleShouldTellAUserWhatIsRunning()
        {
            RunIterateWithOnlyOneTest();
            ConsoleShouldBeOnceCalledWith($"\nRunning: {KeyOfItem1}");
        }

        [TestMethod]
        public void TheTestShouldLastMoreThanASetAmountOfSeconds()
        {
            var lengthOfTestInMilliseconds = RandomValue.Byte();

            SnippetSpeedConsoleInterface.Settings.LengthOfOneTestRound = new TimeSpan(0, 0, 0, 0, lengthOfTestInMilliseconds);
            var sw = Stopwatch.StartNew();
            RunIterateWithOnlyOneTest();
            sw.Stop();
            sw.ElapsedMilliseconds.Should().BeGreaterOrEqualTo(lengthOfTestInMilliseconds);
            sw.ElapsedMilliseconds.Should().BeLessOrEqualTo(lengthOfTestInMilliseconds + 10);
        }

        [TestMethod]
        public void TheConsoleInTheActionShouldMatchTheNumberOfTimesRunInTheOutput()
        {
            RegisterOfTypes.DictoraryOfTypes[KeyOfItem1] = new ASpeedTest3();
            var result = RunIterateWithOnlyOneTest();

            var resultItemActedOn = (ASpeedTest3)RegisterOfTypes.DictoraryOfTypes[KeyOfItem1];

            result.Interations.ShouldBeEquivalentTo(resultItemActedOn.Iterations);
        }


        [TestMethod]
        public void TheResultShouldHaveTheNameOfTheClassTested()
        {
            var result = RunIterateWithOnlyOneTest();

            result.NameOfClass.ShouldBeEquivalentTo(typeof(ASpeedTest3).Name);
        }

        [TestMethod]
        public void ItShouldSetTheTypeOfTest()
        {
            var result = RunIterateWithOnlyOneTest();

            result.TypeOfTest.ShouldAllBeEquivalentTo(new ASpeedTest3().TypeOfTest);
        }

        [TestMethod]
        public void ItShouldSetTheTimeSpanToWhatWeHaveOnSettings()
        {
            var result = RunIterateWithOnlyOneTest();

            result.LengthOfTest.ShouldBeEquivalentTo(new TimeSpan(0, 0, 0, 0, 5));
        }

        [TestMethod]
        public void ItShouldCallTheNumberOfIterationsOnTheNonblockingConsole()
        {
            var result = RunIterateWithOnlyOneTest();

            var expectedSum = Enumerable.Range(1, (int)result.Interations).Sum();

            nonBlockingConsole.SumOfIterations.ShouldBeEquivalentTo(expectedSum);
        }

        [TestMethod]
        public void ItShouldCallHowLongHasElapsedToTheNonblockingConsole()
        {
            var result = RunIterateWithOnlyOneTest();

            nonBlockingConsole.TimesElapsed.All(x => x < result.LengthOfTest);

            for (ulong i = 0; i < result.Interations - 1; i++)
            {
                nonBlockingConsole.TimesElapsed.ElementAt((int)i).Should().BeLessOrEqualTo(nonBlockingConsole.TimesElapsed.ElementAt((int)i + 1));
            }
        }

        [TestMethod]
        public void AllItemsShouldBeRunIfaIsInput()
        {
            var result = Sut.Iterate("a");

            result.Count(x => x.NameOfClass == typeof(ASpeedTest3).Name).ShouldBeEquivalentTo(1);
            result.Count(x => x.NameOfClass == typeof(SpeedTest2).Name).ShouldBeEquivalentTo(1);
            result.Count(x => x.NameOfClass == typeof(SpeedBase1).Name).ShouldBeEquivalentTo(1);
        }

        [TestMethod]
        public void AllItemsShouldBeRunIfAIsInput()
        {
            var result = Sut.Iterate("A");

            result.Count(x => x.NameOfClass == typeof(ASpeedTest3).Name).ShouldBeEquivalentTo(1);
            result.Count(x => x.NameOfClass == typeof(SpeedTest2).Name).ShouldBeEquivalentTo(1);
            result.Count(x => x.NameOfClass == typeof(SpeedBase1).Name).ShouldBeEquivalentTo(1);
        }

        [TestMethod]
        public void TheUserTypesInACharInsteadOfANumber()
        {
            var result = Sut.Iterate("p");

            ConsoleShouldBeOnceCalledWith(MissTypeMessage);
        }

        private void ConsoleShouldBeOnceCalledWith(string message)
        {
            A.CallTo(() => console.WriteLine(message)).MustHaveHappened(Repeated.Exactly.Once);
        }

        private SnippetSpeedTestResult RunIterateWithOnlyOneTest()
        {
            return Sut.Iterate("1").First();
        }

        private class FakeNonBlockingConsole : INonBlockingConsole
        {
            public ulong SumOfIterations { get; set; }
            public List<TimeSpan> TimesElapsed = new List<TimeSpan>();
            public bool CalledRun = false;

            public ulong Iterations
            {
                set
                {
                    SumOfIterations += value;
                }
            }

            public TimeSpan TimeElapsed
            {
                set
                {
                    TimesElapsed.Add(value);
                }
            }

            public void Run()
            {
                CalledRun = true;
            }
        }
    }
}
