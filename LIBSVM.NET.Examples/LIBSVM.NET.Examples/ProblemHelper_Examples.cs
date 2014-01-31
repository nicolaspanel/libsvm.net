using libsvm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace LIBSVM.NET.Examples
{
    
    
    /// <summary>
    ///Classe de test pour ProblemHelperTests, destinée à contenir tous
    ///les tests unitaires ProblemHelperTests
    ///</summary>
    [TestClass()]
    public class ProblemHelper_Examples
    {
        private const string TEST_FILE  = @"datasets\leukemia\leu.ds";
        private const string WRITE_FILE = @"datasets\leukemia\leu.ds-copy";
        private string base_path = "";
        
        //[TestInitialize()]
        public void TestInitialize()
        {
            var current_path = Environment.CurrentDirectory;
            var pos = current_path.IndexOf("libsvm.net");
            base_path = current_path.Substring(0, pos + 10);
        }
        
        /// <summary>
        ///Test pour ReadAndScaleProblem
        ///</summary>
        //[TestMethod()]
        public void ReadAndScaleProblemTest()
        {
            string full_path = System.IO.Path.Combine(base_path, TEST_FILE);
            var prob = ProblemHelper.ReadAndScaleProblem(full_path);
            Assert.IsNotNull(prob);
            Assert.IsTrue(prob.x.Max(v => v.Max(n => n.value)) == 1.0);
            Assert.IsTrue(prob.x.Min(v => v.Min(n => n.value)) == -1.0);
        }

        /// <summary>
        ///Test pour ReadProblem
        ///</summary>
        //[TestMethod()]
        public void ReadProblemTest()
        {
            string full_path = System.IO.Path.Combine(base_path, TEST_FILE);
            var prob = ProblemHelper.ReadProblem(full_path);
            Assert.IsNotNull(prob);
        }

        /// <summary>
        ///Test pour ScaleProblem
        ///</summary>
        //[TestMethod()]
        public void ScaleProblemTest()
        {
            string full_path = System.IO.Path.Combine(base_path, TEST_FILE);
            var prob = ProblemHelper.ScaleProblem(ProblemHelper.ReadProblem(full_path));
            Assert.IsNotNull(prob);
            Assert.IsTrue(prob.x.Max(v => v.Max(n => n.value)) == 1.0);
            Assert.IsTrue(prob.x.Min(v => v.Min(n => n.value)) == -1.0);
        }

        /// <summary>
        ///Test pour WriteProblem
        ///</summary>
        //[TestMethod()]
        public void WriteProblemTest()
        {
            
            string full_test_path = System.IO.Path.Combine(base_path, TEST_FILE);
            var prob = ProblemHelper.ReadProblem(full_test_path);

            string full_write_path = System.IO.Path.Combine(base_path, WRITE_FILE);
            if (File.Exists(full_write_path))
            {
                File.Delete(full_write_path);
            }

            ProblemHelper.WriteProblem(full_write_path, prob);
            Assert.IsTrue(File.Exists(full_write_path));
            File.Delete(full_write_path); // cleaunp after test succeeded
        }


    }
}
