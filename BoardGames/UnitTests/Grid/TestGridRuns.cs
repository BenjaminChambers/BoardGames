namespace UnitTests.Grid
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BoardGames;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test searching for runs.
    /// </summary>
    [TestClass]
    public class TestGridRuns
    {
        /// <summary>
        /// Test that a 1x1 grid is properly recognized as having no runs of length 2 or longer.
        /// </summary>
        [TestMethod]
        public void SingleCell()
        {
            var grid = new Grid<int>(1, 1);
            Assert.AreEqual(0, grid.Runs(2).Count());
        }

        /// <summary>
        /// Verify that a single row (5x1) contains a single run.
        /// </summary>
        [TestMethod]
        public void SingleRow()
        {
            var grid = new Grid<int>(5, 1);
            var runs = grid.Runs(2);

            Assert.AreEqual(1, runs.Count());
        }

        /// <summary>
        /// Verify that runs in a 3x3 grid show up correctly.
        /// </summary>
        [TestMethod]
        public void Test3x3()
        {
            var grid = new Grid<int>(3, 3, new[] { 1, 1, 0, 1, 1, 0, 0, 0, 0 });

            var runs = grid.Runs(3);
            Assert.AreEqual(2, runs.Count());

            runs = grid.Runs(2);
            Assert.AreEqual(6, runs.Where(x => x.Value == 1).Count());
            Assert.AreEqual(3, runs.Where(x => x.Value == 0).Count());
        }
    }
}
