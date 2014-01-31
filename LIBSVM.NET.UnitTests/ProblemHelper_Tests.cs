using libsvm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBSVM.NET.UnitTests
{
    [TestClass()]
    public class ProblemHelper_Tests
    {
        private const string XOR_FILE = @"datasets\xor\xor.ds";
        private readonly string path_to_xor_dataset;
        public ProblemHelper_Tests()
        {
            var current_path = Environment.CurrentDirectory;
            var pos = current_path.IndexOf("libsvm.net"); 
            var base_path  = current_path.Substring(0, pos + 10);
            path_to_xor_dataset = System.IO.Path.Combine(base_path, XOR_FILE);
        }

        [TestMethod()]
        public void Should_be_able_to_read_files()
        {
            var prob = ProblemHelper.ReadProblem(path_to_xor_dataset);
            Assert.IsNotNull(prob);

            var lineCount = File.ReadLines(path_to_xor_dataset).Count();
            Assert.IsTrue(prob.l == lineCount);
            Assert.IsTrue(prob.x.Count() == lineCount);
            Assert.IsTrue(prob.y.Count() == lineCount);

            Assert.IsTrue(prob.x.Max(v => v.Max(n => n.value)) == 1.0);
            Assert.IsTrue(prob.x.Min(v => v.Min(n => n.value)) == -1.0);

            Assert.IsTrue(prob.y.Max() == 1.0);
            Assert.IsTrue(prob.y.Min() == 0);
        }

        [TestMethod()]
        public void Should_be_able_to_scale_problems()
        {
            var prob = new svm_problem() {
                l = 2,
                x = new svm_node[][]
                {
                   new svm_node[]
                   {
                        new svm_node(){ index= 1, value=1},
                        new svm_node(){ index= 2, value=2}
                   },
                   new svm_node[]
                   {
                        new svm_node(){ index= 1, value=3},
                        new svm_node(){ index= 2, value=4}
                   }
                },
                y = new double[] { 0, 0 }
            };

            prob = prob.Scale(0, 1);
            Assert.IsTrue(prob.x[0].Single(x => x.index == 1).value == 0);
            Assert.IsTrue(prob.x[1].Single(x => x.index == 1).value == 1);

            Assert.IsTrue(prob.x[0].Single(x => x.index == 2).value == 0);
            Assert.IsTrue(prob.x[1].Single(x => x.index == 2).value == 1);
        }
    }
}
