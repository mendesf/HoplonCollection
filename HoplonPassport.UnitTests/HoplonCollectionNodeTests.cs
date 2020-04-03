using Hoplon.Collections;
using NUnit.Framework;

namespace HoplonPassport.UnitTests
{
    [TestFixture]
    public class HoplonCollectionNodeTests
    {
        private HoplonCollectionNode node;

        [SetUp]
        public void SetUp()
        {
            node = new HoplonCollectionNode();
            node.Add(1990, "mayconn");
            node.Add(1997, "amanda");
            node.Add(1998, "ana");
            node.Add(1990, "felipe");
            node.Add(1970, "marcia");
        }

        [Test]
        [TestCase(0, 2, new[] { "marcia", "felipe", "mayconn" })]
        [TestCase(3, 3, new[] { "amanda" })]
        [TestCase(0, 10, new[] { "marcia", "felipe", "mayconn", "amanda", "ana" })]
        [TestCase(1, 0, new[] { "felipe" })]
        [TestCase(0, -2, new[] { "marcia", "felipe", "mayconn", "amanda" })]
        [TestCase(0, -10, new[] { "marcia" })]
        [TestCase(-2, 1, new[] { "marcia", "felipe" })]
        [TestCase(-3, -1, new[] { "marcia", "felipe", "mayconn", "amanda", "ana" })]
        public void Get_WhenCalled_ShouldReturnListRespectingIndexRange(int start, int end, string[] expectedValues)
        {
            var result = node.Get(start, end);
            Assert.That(result, Is.EquivalentTo(expectedValues));
        }

        [Test]
        [TestCase("amanda", 3)]
        [TestCase("filipe", -1)]
        public void IndexOf_WhenCalled_ShouldReturnCorrectIndex(string value, long expectedIndex)
        {
            var result = node.IndexOf(value);
            Assert.That(result, Is.EqualTo(expectedIndex));
        }

        [Test]
        [TestCase(2020, "felipe")]
        public void Add_RepeatedValue_ShouldReorderItAndReturnTrue(int subIndex, string value)
        {
            var currentIndex = node.IndexOf(value);
            var result = node.Add(subIndex, value);
            var newIndex = node.IndexOf(value);
            Assert.That(result, Is.True);
            Assert.That(newIndex, Is.Not.EqualTo(currentIndex));
        }
    }
}
