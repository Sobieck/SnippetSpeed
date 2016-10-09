using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnippetSpeed.Implementations;

namespace SnippetSpeed.Tests
{
    [TestClass]
    public class SnippetSpeedTestConstructorTests
    {
        private SnippetSpeed snippetSpeed;

        [TestInitialize]
        public void BeforeEach()
        {
            SnippetSpeed.Settings = null;
            snippetSpeed = new SnippetSpeed();
        }

        [TestMethod]
        public void TheConsoleShouldBeAConsoleWrapper()
        {
            snippetSpeed.Console.Should().BeOfType<ConsoleWrapper>();
        }

        [TestMethod]
        public void TheIteratorShouldBeASnippetIterator()
        {
            snippetSpeed.Iterator.Should().BeOfType<SnippetIterator>();
        }

        [TestMethod]
        public void TheResultsWriterShouldBeASCsvResultsWriter()
        {
            snippetSpeed.ResultWriter.Should().BeOfType<CsvResultWriter>();
        }

        [TestMethod]
        public void TheSettingsShouldBeASettings()
        {
            SnippetSpeed.Settings.LengthOfOneTestRound.TotalMinutes.ShouldBeEquivalentTo(5);
        }
    }
}
