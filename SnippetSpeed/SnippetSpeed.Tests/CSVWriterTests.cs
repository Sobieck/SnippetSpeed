using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomTestValues;
using SnippetSpeed.Implementations;
using SnippetSpeed.Interfaces;
using FluentAssertions;
using System.Linq;

namespace SnippetSpeed.Tests
{

    [TestClass]
    public class CSVWriterTests
    {
        private FakeFileWrap fileWrapper;
        private ICollection<SnippetSpeedTestResult> collectionOfSnippetSpeedTestResults;

        internal CsvResultWriter Sut { get; private set; }

        [ClassInitialize]
        public static void BeforeAll(TestContext testContext)
        {
            RegisterOfTypesTests.BeforeAll(testContext);

        }

        [TestInitialize]
        public void BeforeEach()
        {
            fileWrapper = new FakeFileWrap();
            Sut = new CsvResultWriter(fileWrapper);

            collectionOfSnippetSpeedTestResults = RandomValue.ICollection<SnippetSpeedTestResult>();

            foreach (var item in collectionOfSnippetSpeedTestResults)
            {
                var randomInt = RandomValue.Long();
                item.LengthOfTest = new TimeSpan(randomInt);
            }

            Sut.Write(collectionOfSnippetSpeedTestResults);
        }

        [TestMethod]
        public void TheTitlesOfTheFieldsShouldBeTheFirstItemPassedInTheArrayToTheWriter()
        {
            fileWrapper.Lines[0].ShouldBeEquivalentTo("ClassName,TypeOfTest,Iterations,NanosecondsPerIteration,LengthOfTest(ms)");
        }

        [TestMethod]
        public void ThePathShouldBeThePathSetOnTheSettings()
        {
            fileWrapper.Path.Should().Contain(SnippetSpeedConsoleInterface.Settings.OutputWritePath+"\\"+SnippetSpeedConsoleInterface.Settings.OutputFileName);
        }

        [TestMethod]
        public void NanosecondShouldBeComputerCorrectly()
        {
            var nanosecondsString = GetNanosecondString(1);

            var firstInput = collectionOfSnippetSpeedTestResults.ElementAt(0);

            int numberOfNanoSeconds = NanosecondsPerOperation(firstInput);

            nanosecondsString.ShouldBeEquivalentTo(numberOfNanoSeconds);
        }

        [TestMethod]
        public void FirstClassNameShouldBeRight()
        {
            var itemNumber = 1;
            GetClassName(itemNumber).ShouldBeEquivalentTo(collectionOfSnippetSpeedTestResults.ElementAt(0).NameOfClass);
        }

        [TestMethod]
        public void FirstTypeOfTestShouldBeRight()
        {
            var itemNumber = 1;
            GetTypeOfTest(itemNumber).ShouldBeEquivalentTo(collectionOfSnippetSpeedTestResults.ElementAt(0).TypeOfTest);
        }

        [TestMethod]
        public void FirstIterationsShouldBeRight()
        {
            var itemNumber = 1;
            GetIterations(itemNumber).ShouldBeEquivalentTo(collectionOfSnippetSpeedTestResults.ElementAt(0).Interations.ToString());
        }
        
        [TestMethod]
        public void FirstLengthOfTestInMillisecondsShouldBeRight()
        {
            var itemNumber = 1;
            GetLengthOfTestInMilliseconds(itemNumber).ShouldBeEquivalentTo((int)collectionOfSnippetSpeedTestResults.ElementAt(0).LengthOfTest.TotalMilliseconds);
        }

        [TestMethod]
        public void AllTheInputsShouldBeCorrect()
        {
            for (int i = 0; i < collectionOfSnippetSpeedTestResults.Count; i++)
            {
                var currentItem = collectionOfSnippetSpeedTestResults.ElementAt(i);
                var indexOfItemInWriter = i + 1;

                GetLengthOfTestInMilliseconds(indexOfItemInWriter).Should().Be(((int)currentItem.LengthOfTest.TotalMilliseconds).ToString());
                GetIterations(indexOfItemInWriter).Should().Be(currentItem.Interations.ToString());
                GetTypeOfTest(indexOfItemInWriter).Should().Be(currentItem.TypeOfTest);
                GetClassName(indexOfItemInWriter).Should().Be(currentItem.NameOfClass);
            }
        }

        private static int NanosecondsPerOperation(SnippetSpeedTestResult input)
        {
            var numberOfTicksPerCalculation = input.LengthOfTest.Ticks / (decimal)input.Interations;

            var numberOfNanoSeconds = numberOfTicksPerCalculation * 100;
            return (int)numberOfNanoSeconds;
        }

        private string GetLengthOfTestInMilliseconds(int itemNumber)
        {
            return GetCsvValue(itemNumber, 4);
        }

        private string GetIterations(int itemNumber)
        {
            return GetCsvValue(itemNumber, 2);
        }

        private string GetTypeOfTest(int itemNumber)
        {
            return GetCsvValue(itemNumber, 1);
        }

        private string GetClassName(int itemNumber)
        {
            return GetCsvValue(itemNumber, 0);
        }

        private string GetNanosecondString(int itemNumber)
        {
            var columnNumber = 3;
            var nanosecondsString = GetCsvValue(itemNumber, columnNumber);
            return nanosecondsString;
        }

        private string GetCsvValue(int itemNumber, int columnNumber)
        {
            return fileWrapper.Lines[itemNumber].Split(',')[columnNumber];
        }

        private class FakeFileWrap : IFileWrapper
        {
            public string Path { get; set; }

            public string[] Lines { get; set; }

            public void WriteAllLines(string path, string[] lines)
            {
                Path = path;
                Lines = lines;
            }
        }
    }
}
