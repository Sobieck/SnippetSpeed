using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SnippetSpeed.Tests.TypeTests
{
    [TestClass]
    public class SnippetSpeedTestResultTests
    {
        public SnippetSpeedTestResult Sut { get; private set; }

        [TestInitialize]
        public void BeforeEach()
        {
            Sut = new SnippetSpeedTestResult();
            Sut.Interations = 1000;
            Sut.LengthOfTest = new TimeSpan(100);
        }

        [TestMethod]
        public void OneHundredTicks()
        {
            Sut.AverageTimeOfIterationInNanoseconds.Should().Be(10);
        }

        [TestMethod]
        public void OneThousandTicks()
        {
            Sut.LengthOfTest = new TimeSpan(1000);
            Sut.AverageTimeOfIterationInNanoseconds.Should().Be(100);
        }
        [TestMethod]
        public void FiftyIterations()
        {
            Sut.Interations = 50;
            Sut.AverageTimeOfIterationInNanoseconds.ShouldBeEquivalentTo(200);
        }
    }
}
