using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnippetSpeed.Implementations;

namespace SnippetSpeed.Tests
{
    [TestClass]
    public class SnippetSpeedTestConstructorTests
    {
        [TestInitialize]
        public void BeforeEach()
        {
            SnippetSpeed.Settings = null;
            SnippetSpeed.Console = null;
            SnippetSpeed.Iterator = null;
            SnippetSpeed.ResultWriter = null;
        }

        [TestMethod]
        public void TheConsoleShouldBeAConsoleWrapper()
        {
            SnippetSpeed.Console.Should().BeOfType<ConsoleWrapper>();
        }

        [TestMethod]
        public void TheIteratorShouldBeASnippetIterator()
        {
            SnippetSpeed.Iterator.Should().BeOfType<SnippetIterator>();
        }

        [TestMethod]
        public void TheResultsWriterShouldBeASCsvResultsWriter()
        {
            SnippetSpeed.ResultWriter.Should().BeOfType<CsvResultWriter>();
        }

        [TestMethod]
        public void TheSettingsShouldBeASettings()
        {
            SnippetSpeed.Settings.LengthOfOneTestRound.TotalMinutes.ShouldBeEquivalentTo(5);
        }
    }
}
