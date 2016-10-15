using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnippetSpeed.Implementations;

namespace SnippetSpeed.Tests
{
    [TestClass]
    public class SnippetSpeedTestConstructionTests
    {
        [TestInitialize]
        public void BeforeEach()
        {
            SnippetSpeedConsoleInterface.Settings = null;
            SnippetSpeedConsoleInterface.Console = null;
            SnippetSpeedConsoleInterface.Iterator = null;
            SnippetSpeedConsoleInterface.ResultWriter = null;
        }

        [TestMethod]
        public void TheConsoleShouldBeAConsoleWrapper()
        {
            SnippetSpeedConsoleInterface.Console.Should().BeOfType<ConsoleWrapper>();
        }

        [TestMethod]
        public void TheIteratorShouldBeASnippetIterator()
        {
            SnippetSpeedConsoleInterface.Iterator.Should().BeOfType<SnippetIterator>();
        }

        [TestMethod]
        public void TheResultsWriterShouldBeASCsvResultsWriter()
        {
            SnippetSpeedConsoleInterface.ResultWriter.Should().BeOfType<CsvResultWriter>();
        }

        [TestMethod]
        public void TheSettingsShouldBeASettings()
        {
            SnippetSpeedConsoleInterface.Settings.LengthOfOneTestRound.TotalMinutes.ShouldBeEquivalentTo(5);
        }
    }
}
