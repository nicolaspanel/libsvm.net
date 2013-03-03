using libsvm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace LIBSVM.NET.Tests
{
    
    
    /// <summary>
    ///Classe de test pour ProblemHelperTests, destinée à contenir tous
    ///les tests unitaires ProblemHelperTests
    ///</summary>
    [TestClass()]
    public class ProblemHelperTests
    {

        private const string TEST_FILE = "C:\\Users\\user\\Documents\\visual studio 2010\\Projects\\LIBSVM\\LIBSVM.NET.Tests\\DataSets\\leukemia\\leu.ds.combined";
        private const string WRITE_FILE = "C:\\Users\\user\\Documents\\visual studio 2010\\Projects\\LIBSVM\\LIBSVM.NET.Tests\\DataSets\\leukemia\\leu.ds.writed";
        /// <summary>
        ///Test pour ReadAndScaleProblem
        ///</summary>
        [TestMethod()]
        public void ReadAndScaleProblemTest()
        {
            var prob = ProblemHelper.ReadAndScaleProblem(TEST_FILE);
            Assert.IsNotNull(prob);
            Assert.IsTrue(prob.x.Max(v => v.Max(n => n.value)) == 1.0);
            Assert.IsTrue(prob.x.Min(v => v.Min(n => n.value)) == -1.0);
        }

        /// <summary>
        ///Test pour ReadProblem
        ///</summary>
        [TestMethod()]
        public void ReadProblemTest()
        {
            var prob = ProblemHelper.ReadProblem(TEST_FILE);
            Assert.IsNotNull(prob);
        }

        /// <summary>
        ///Test pour ScaleProblem
        ///</summary>
        [TestMethod()]
        public void ScaleProblemTest()
        {
            var prob = ProblemHelper.ScaleProblem(ProblemHelper.ReadProblem(TEST_FILE));
            Assert.IsNotNull(prob);
            Assert.IsTrue(prob.x.Max(v => v.Max(n => n.value)) == 1.0);
            Assert.IsTrue(prob.x.Min(v => v.Min(n => n.value)) == -1.0);
        }

        /// <summary>
        ///Test pour WriteProblem
        ///</summary>
        [TestMethod()]
        public void WriteProblemTest()
        {
            var prob = ProblemHelper.ReadProblem(TEST_FILE);
            ProblemHelper.WriteProblem(WRITE_FILE, prob);
        }

        /// <summary>
        ///Test pour atof
        ///</summary>
        [TestMethod()]
        public void atofTest()
        {
            string s = "1.0"; 
            double expected = 1.0; 
            double actual;
            actual = ProblemHelper.atof(s);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Test pour atof
        ///</summary>
        [TestMethod()]
        public void atofTest2()
        {
            string s = "1";
            double expected = 1.0;
            double actual;
            actual = ProblemHelper.atof(s);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Test pour atoi
        ///</summary>
        [TestMethod()]
        public void atoiTest()
        {
            string s = "2"; // TODO: initialisez à une valeur appropriée
            int expected = 2; // TODO: initialisez à une valeur appropriée
            int actual;
            actual = ProblemHelper.atoi(s);
            Assert.AreEqual(expected, actual);
        }
    }
}
