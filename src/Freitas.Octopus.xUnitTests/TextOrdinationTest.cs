using Freitas.Octopus.Ordination;
using System;
using Xunit;

namespace Freitas.Octopus.xUnitTests
{
    public class TextOrdinationTest
    {
        [Fact]
        public void GetFirstPositionTest()
        {
            var firstPosition = TextOrdination.Instance.GenerateOrdination();
            Assert.True(firstPosition == "AAAA");
        }

        [Theory]
        [InlineData("AAAA")]
        [InlineData("AAAE")]
        [InlineData("AAAY")]
        [InlineData("AAAAE")]
        [InlineData("AAAAY")]
        public void GetLastPositionTest(string previous)
        {
            var last = TextOrdination.Instance.GenerateOrdination(previous);

            if (previous == "AAAA")
                Assert.True(last == "AAAE");
            else if (previous == "AAAE")
                Assert.True(last == "AAAI");
            else if (previous == "AAAY")
                Assert.True(last == "AAEA");
            else if (previous == "AAAAE")
                Assert.True(last == "AAAAI");
            else if (previous == "AAAAY")
                Assert.True(last == "AAAAI");
        }

        [Theory]
        [InlineData("AZAX", "AZZZ")]
        //[InlineData("AAAA", "AAAE")]
        //[InlineData("AAAA", "AAAC")]
        //[InlineData("AAAA", "AAAB")]
        //[InlineData("AAAA", "AAAAE")]
        //[InlineData("AAAE", "AAAI")]
        //[InlineData("AAAAC", "AAAAE")]
        //[InlineData("AAAAC", "AAAAD")]
        public void GetInnerPositionTest(string previous, string next)
        {
            var inner = TextOrdination.Instance.GenerateOrdination(previous, next);

            if (previous == "AAAA" && next == "AAAE")
                Assert.True(inner == "AAAC");
            else if (previous == "AAAA" && next == "AAAC")
                Assert.True(inner == "AAAB");
            else if (previous == "AAAA" && next == "AAAB")
                Assert.True(inner == "AAAAE");
            else if (previous == "AAAA" && next == "AAAAE")
                Assert.True(inner == "AAAAC");
            else if (previous == "AAAE" && next == "AAAI")
                Assert.True(inner == "AAAG");
            else if (previous == "AAAAC" && next == "AAAAE")
                Assert.True(inner == "AAAAD");
            else if (previous == "AAAAC" && next == "AAAAD")
                Assert.True(inner == "AAAACE");
        }
    }
}
