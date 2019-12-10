namespace UnitTests.Grid
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BoardGames;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test anything that causes the grid to transform itself without changing individual cells.
    /// </summary>
    [TestClass]
    public class TestGridTransformation
    {
        /// <summary>
        /// Rotate the grid up to 7 times each for clockwise / counter-clockwise and verify the result.
        /// </summary>
        [TestMethod]
        public void TestRotation()
        {
            var rot0 = new[]
            {
                0, 1, 2, 3,
                4, 5, 6, 7,
            };
            var rot1 = new[]
            {
                4, 0,
                5, 1,
                6, 2,
                7, 3,
            };
            var rot2 = new[]
            {
                7, 6, 5, 4,
                3, 2, 1, 0,
            };
            var rot3 = new[]
            {
                3, 7,
                2, 6,
                1, 5,
                0, 4,
            };

            var source = new Grid<int>(4, 2, rot0);

            Util.CompareEnum(rot0, source.Rotate(0).All());
            Util.CompareEnum(rot1, source.Rotate(1).All());
            Util.CompareEnum(rot2, source.Rotate(2).All());
            Util.CompareEnum(rot3, source.Rotate(3).All());
            Util.CompareEnum(rot0, source.Rotate(4).All());
            Util.CompareEnum(rot1, source.Rotate(5).All());
            Util.CompareEnum(rot2, source.Rotate(6).All());
            Util.CompareEnum(rot3, source.Rotate(7).All());
            Util.CompareEnum(rot3, source.Rotate(-1).All());
            Util.CompareEnum(rot2, source.Rotate(-2).All());
            Util.CompareEnum(rot1, source.Rotate(-3).All());
            Util.CompareEnum(rot0, source.Rotate(-4).All());
            Util.CompareEnum(rot3, source.Rotate(-5).All());
            Util.CompareEnum(rot2, source.Rotate(-6).All());
            Util.CompareEnum(rot1, source.Rotate(-7).All());
        }

        /// <summary>
        /// Swap the X and Y axes.
        /// </summary>
        [TestMethod]
        public void TestTransposition()
        {
            var source = new[]
            {
                1, 1, 1, 1, 1, 1, 1,
                2, 2, 2, 2, 2, 2, 2,
                3, 3, 3, 3, 3, 3, 3,
                4, 4, 4, 4, 4, 4, 4,
            };
            var transposed = new[]
            {
                1, 2, 3, 4,
                1, 2, 3, 4,
                1, 2, 3, 4,
                1, 2, 3, 4,
                1, 2, 3, 4,
                1, 2, 3, 4,
                1, 2, 3, 4,
            };

            Util.CompareEnum(transposed, new Grid<int>(7, 4, source).Transpose().All());
        }

        /// <summary>
        /// Flip horizontally, vertically, or both.
        /// </summary>
        [TestMethod]
        public void TestFlipping()
        {
            var original = new[]
            {
                1,  2,  3,  4,  5,
                6,  7,  8,  9, 10,
                11, 12, 13, 14, 15,
                16, 17, 18, 19, 20,
            };
            var horizontal = new[]
            {
                5,  4,  3,  2,  1,
                10,  9,  8,  7,  6,
                15, 14, 13, 12, 11,
                20, 19, 18, 17, 16,
            };
            var vertical = new[]
            {
                16, 17, 18, 19, 20,
                11, 12, 13, 14, 15,
                6,  7,  8,  9,  10,
                1,  2,  3,  4,  5,
            };
            var both = new[]
            {
                20, 19, 18, 17, 16,
                15, 14, 13, 12, 11,
                10, 9,  8,  7,  6,
                5,  4,  3,  2,  1,
            };

            Util.CompareEnum(original, new Grid<int>(5, 4, original).Flip(false, false).All());
            Util.CompareEnum(horizontal, new Grid<int>(5, 4, original).Flip(true, false).All());
            Util.CompareEnum(vertical, new Grid<int>(5, 4, original).Flip(false, true).All());
            Util.CompareEnum(both, new Grid<int>(5, 4, original).Flip(true, true).All());
        }
    }
}
