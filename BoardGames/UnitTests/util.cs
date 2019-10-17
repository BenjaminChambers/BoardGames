using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTests
{
    public static class Util
    {
        public static void CompareEnum<T>(IEnumerable<T> First, IEnumerable<T> Second)
        {
            foreach (var test in First.Zip(Second))
                Assert.AreEqual(test.First, test.Second);
        }
    }
}
