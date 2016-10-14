using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnippetSpeed.Implementations;

namespace SnippetSpeed.Tests
{
    [TestClass]
    public class SnippetIteratorTests
    {
        internal SnippetIterator Sut { get; private set; }

        [TestInitialize]
        public void BeforeEach()
        {
            Sut = new SnippetIterator();
        }

        [TestMethod]
        public void MyTestMethod()
        {

        }
    }
}
