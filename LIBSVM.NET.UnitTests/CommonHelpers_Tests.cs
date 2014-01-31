using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LIBSVM.NET.UnitTests
{
    [TestClass]
    public class CommonHelpers_Tests
    {
        /// <summary>
        ///Test pour atof
        ///</summary>
        [TestMethod()]
        public void should_convert_string_to_double()
        {
            string s = "1.0";
            double expected = 1.0;
            double actual;
            actual = s.ToDouble();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Test pour atof
        ///</summary>
        [TestMethod()]
        public void should_convert_string_to_double_2()
        {
            string s = "1 ";
            double expected = 1.0;
            double actual;
            actual = s.ToDouble();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Test pour atoi
        ///</summary>
        [TestMethod()]
        public void should_convert_string_to_interger()
        {
            string s = "2"; 
            int expected = 2; 
            int actual;
            actual = s.ToInteger();
            Assert.AreEqual(expected, actual);
        }
    }
}
