using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTests
{
    public static class Util
    {
        public static void CompareEnum<T>(IEnumerable<T> Expected, IEnumerable<T> Actual)
        {
            foreach (var test in Expected.Zip(Actual))
                Assert.AreEqual(test.First, test.Second);
        }
    }
}
