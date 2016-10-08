using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace SnippetSpeed.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            1.Should().Be(1);
        }
    }
}
