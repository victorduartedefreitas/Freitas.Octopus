using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit;

namespace Freitas.Octopus.xUnitTests
{
    public class TextOrdinationTest
    {
        private IList<Item> GetCleanItems()
        {
            return new List<Item>
            {
                new Item("Item 001"),
                new Item("New item"),
                new Item("Cellphone"),
                new Item("Notebook"),
                new Item("Mouse"),
                new Item("Tablet"),
                new Item("Photo"),
                new Item("Water bottle"),
                new Item("Bible"),
                new Item("Book"),
                new Item("Macbook"),
                new Item("iMac"),
                new Item("Trash"),
                new Item("Paper"),
                new Item("Chair"),
                new Item("Table"),
                new Item("TV"),
                new Item("Cup"),
                new Item("Trash"),
                new Item("F1"),
                new Item("F2"),
                new Item("F3"),
                new Item("F4"),
                new Item("F5"),
                new Item("F6"),
                new Item("F7"),
                new Item("F8"),
                new Item("F9"),
                new Item("F10"),
                new Item("F11"),
                new Item("F12"),
                new Item("LetterA"),
                new Item("LetterB"),
                new Item("LetterC"),
                new Item("LetterD"),
                new Item("LetterE"),
                new Item("LetterF"),
                new Item("LetterG"),
                new Item("LetterH"),
                new Item("LetterI"),
                new Item("LetterJ"),
                new Item("LetterK"),
                new Item("LetterL"),
                new Item("LetterM"),
                new Item("LetterN"),
                new Item("LetterO"),
                new Item("LetterP"),
                new Item("LetterQ"),
                new Item("LetterR"),
                new Item("LetterS"),
                new Item("LetterT"),
                new Item("LetterU"),
                new Item("LetterV"),
                new Item("LetterW"),
                new Item("LetterX"),
                new Item("LetterY"),
                new Item("LetterZ")
            };
        }

        private IList<Item> GetCleanItems2()
        {
            var items = new List<Item>();
            for (long i = 0; i < 500_000; i++)
                items.Add(new Item($"Item{i}"));

            return items;
        }

        [Fact]
        public void GetFirstPositionTest()
        {
            var firstPosition = OctopusPositionGenerator.Instance.GeneratePositionValue();
            Assert.True(firstPosition == "AAAA");
        }

        [Theory]
        [InlineData("AAAA")]
        [InlineData("AAAE")]
        [InlineData("AAAY")]
        [InlineData("AAAZ")]
        [InlineData("AAAAE")]
        [InlineData("AAAAY")]
        public void GetLastPositionTest(string previous)
        {
            var last = OctopusPositionGenerator.Instance.GeneratePositionValue(previous);

            if (previous == "AAAA")
                Assert.True(last == "AAAE");
            else if (previous == "AAAE")
                Assert.True(last == "AAAI");
            else if (previous == "AAAY")
                Assert.True(last == "AAAZ");
            else if (previous == "AAAAE")
                Assert.True(last == "AAAAI");
            else if (previous == "AAAAY")
                Assert.True(last == "AAAAZ");
            else if (previous == "AAAZ")
                Assert.True(last == "AAEA");
        }

        [Theory]
        [InlineData("AZAX", "AZZZ")]
        [InlineData("AAAA", "AAAE")]
        [InlineData("AAAA", "AAAC")]
        [InlineData("AAAA", "AAAB")]
        [InlineData("AAAA", "AAAAE")]
        [InlineData("AAAE", "AAAI")]
        [InlineData("AAAAC", "AAAAE")]
        [InlineData("AAAAC", "AAAAD")]
        [InlineData("AAAAZ", "AAAC")]
        [InlineData("AAAAZ", "AAAB")]
        [InlineData("AAAAY", "AAAB")]
        [InlineData("AAAAW", "AAAB")]
        [InlineData("AAAAE", "AAAB")]
        [InlineData("AABCZ", "AABEE")]
        [InlineData("AZZZ", null)]
        [InlineData("AAZY", "AAZZ")]
        [InlineData("AZZW", "AZZZ")]
        [InlineData("ZZZZ", null)]
        [InlineData("AZZZ", "AZZZZ")]
        public void GetInnerPositionTest(string previous, string next)
        {
            var inner = OctopusPositionGenerator.Instance.GeneratePositionValue(previous, next);
            Debug.WriteLine($"Previous: {previous}; Next: {next}; NewPosition: {inner}");

            //if (previous == "AAAA" && next == "AAAE")
            //    Assert.True(inner == "AAAC");
            //else if (previous == "AAAA" && next == "AAAC")
            //    Assert.True(inner == "AAAB");
            //else if (previous == "AAAA" && next == "AAAB")
            //    Assert.True(inner == "AAAAE");
            //else if (previous == "AAAA" && next == "AAAAE")
            //    Assert.True(inner == "AAAAC");
            //else if (previous == "AAAE" && next == "AAAI")
            //    Assert.True(inner == "AAAG");
            //else if (previous == "AAAAC" && next == "AAAAE")
            //    Assert.True(inner == "AAAAD");
            //else if (previous == "AAAAC" && next == "AAAAD")
            //    Assert.True(inner == "AAAACE");
            //else if (previous == "AAAAZ" && next == "AAAC")
            //    Assert.True(inner == "AAAB");
            //else if (previous == "AAAAZ" && next == "AAAB")
            //    Assert.True(inner == "AAAAZA");
            //else if (previous == "AAAAY" && next == "AAAB")
            //    Assert.True(inner == "AAAAZ");
            //else if (previous == "AAAAW" && next == "AAAB")
            //    Assert.True(inner == "AAAAY");
            //else if (previous == "AAAAE" && next == "AAAB")
            //    Assert.True(inner == "AAAAI");
            //else if (previous == "AABCZ" && next == "AABEE")
            //    Assert.True(inner == "AABCZE");
        }

        [Fact]
        public void OrderListTest()
        {
            IList<Item> items = GetCleanItems();
            items.InitializeOctopusOrdination();

            var letterA = items.FirstOrDefault(f => f.Name == "LetterA");
            var f11 = items.FirstOrDefault(f => f.Name == "F11");

            items = items.MoveUp(f11);
            items = items.MoveUp(items[0]);
            items = items.MoveUp(items[1]);
            items = items.MoveUp(items[2]);
            items = items.MoveUp(letterA);

            items = items.MoveDown(items[0]);
            items = items.MoveDown(items[1]);
            items = items.MoveDown(items[2]);
            items = items.MoveDown(items[items.Count - 1]);
            items = items.MoveDown(items[items.Count - 2]);
            items = items.MoveDown(items[items.Count - 3]);

        }

        [Fact]
        public void InitializeOrderTest()
        {
            var gc0 = GC.CollectionCount(0);
            var gc1 = GC.CollectionCount(1);

            var sw = new Stopwatch();
            sw.Start();

            var items = GetCleanItems2();
            items.InitializeOctopusOrdination();

            sw.Stop();
            var time = sw.ElapsedMilliseconds;

            var gc0_pos = GC.CollectionCount(0) - gc0;
            var gc1_pos = GC.CollectionCount(1) - gc1;

            string ret = string.Empty;

            foreach (var i in items)
                ret += $"{i}\r\n";

            ret += $"GC_0:{gc0_pos}; GC_1:{gc1_pos}; Time: {time}ms";
        }
    }
}
