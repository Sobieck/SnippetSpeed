using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomTestValues;

namespace SnippetSpeed.Tests
{
    [TestClass]
    public class SnippetSpeedSettingsTests
    {
        private static SnippetSpeedSettings Result;
        private DateTime startDateTime;

        [TestInitialize]
        public void BeforeEach()
        {
            startDateTime = RandomValue.DateTime();

            Result = new SnippetSpeedSettings();
        }

        [TestMethod]
        public void TheDefaultLengthOfATestShouldBe5Minutes()
        {
            Result.LengthOfOneTestRound.TotalMinutes.Should().Be(5);
        }

        [TestMethod]
        public void ItShouldHaveTheRightFileName()
        {
            Result.StartOfExecution = startDateTime;
            var localTime = startDateTime.ToLocalTime().ToString().Replace(' ', '-').Replace('/','-').Replace(':','-');
            Result.OutputPathAndFileName.Should().Be($"result-{localTime}.csv");
        }

        [TestMethod]
        public void TheDefaultStartTimeShouldBeSetToSomethingOtherThanTheDefault()
        {
            Result.StartOfExecution.Should().NotBeSameDateAs(new DateTime());
        }

        [TestMethod]
        public void TheOutputWriteFileShouldReturnWhateverTheUserSetsItAs()
        {
            var path = RandomValue.String();
            Result.OutputPathAndFileName = path;

            Result.OutputPathAndFileName.ShouldAllBeEquivalentTo(path);
        }

        [TestMethod]
        public void TheDefaultLengthOfOneTestRoundShouldBeOverWrittenWhenAUserSetsIt()
        {
            var lengthOfMinutes = RandomValue.Int();

            var span = new TimeSpan(0, lengthOfMinutes, 0);

            Result.LengthOfOneTestRound = span;

            Result.LengthOfOneTestRound.TotalMinutes.Should().Be(lengthOfMinutes);
            Result.LengthOfOneTestRound.TotalMinutes.Should().NotBe(5);
        }
    }
}
