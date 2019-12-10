namespace UnitTests.Grid
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BoardGames;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Contains the tests for constructors of the <see cref="Grid{T}"/> type.
    /// </summary>
    [TestClass]
    public class TestGridConstructors
    {
        /// <summary>
        /// Verify that default constructors result in empty values in every cell.
        /// </summary>
        [TestMethod]
        public void TestEmptyConstruction()
        {
            var bGrid = new Grid<bool>(5, 4);
            Assert.AreEqual(5, bGrid.Width);
            Assert.AreEqual(4, bGrid.Height);
            Assert.AreEqual(20, bGrid.All().Count());
            foreach (var t in bGrid.All())
            {
                Assert.AreEqual(false, t);
            }

            var iGrid = new Grid<int>(7, 6);
            Assert.AreEqual(7, iGrid.Width);
            Assert.AreEqual(6, iGrid.Height);
            Assert.AreEqual(42, iGrid.All().Count());
            foreach (var t in iGrid.All())
            {
                Assert.AreEqual(0, t);
            }
        }

        /// <summary>
        /// Test that constructing from an enumeration of values correctly places those values in the <see cref="Grid{T}"/>.
        /// </summary>
        [TestMethod]
        public void TestDataConstruction()
        {
            var source = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var work = new Grid<int>(4, 2, source);

            Util.CompareEnum(work.All(), source);
        }

        /// <summary>
        /// Test that copying a <see cref="Grid{T}"/> correctly initializes the deep copy.
        /// </summary>
        [TestMethod]
        public void TestCopyConstruction()
        {
            var source = Enumerable.Range(0, 100).ToArray();
            var first = new Grid<int>(10, 10, source);
            var second = new Grid<int>(first);
            Util.CompareEnum(first.All(), second.All());
        }

        /// <summary>
        /// Test that changing a <see cref="Grid{T}"/> results in the correct data.
        /// </summary>
        [TestMethod]
        public void TestChangeConstruction()
        {
            var source = Enumerable.Range(0, 100).ToArray();
            int[] changed = new int[100];
            Array.Copy(source, 0, changed, 0, 100);

            var changes = new List<(int Location, int Value)>();
            for (int i = 50; i < 60; i++)
            {
                changes.Add((i, -7));
                changed[i] = -7;
            }

            var first = new Grid<int>(10, 10, source);
            var second = new Grid<int>(first, changes);
            Util.CompareEnum(changed, second.All());
        }
    }
}
