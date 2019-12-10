namespace UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Common utilities for test funcitons.
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// Zip two enums and verify that the corresponding elements are equal to each other.
        /// </summary>
        /// <typeparam name="T">The generic type of the enum.</typeparam>
        /// <param name="expected">The enumeration of expected values.</param>
        /// <param name="actual">The enumeration of actual values.</param>
        public static void CompareEnum<T>(IEnumerable<T> expected, IEnumerable<T> actual)
        {
            foreach (var (ex, ac) in expected.Zip(actual))
                Assert.AreEqual(ex, ac);
        }
    }
}
