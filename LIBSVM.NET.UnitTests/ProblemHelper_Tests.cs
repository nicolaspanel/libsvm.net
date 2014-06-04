using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using libsvm;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LIBSVM.NET.UnitTests
{
    [TestClass]
    public class ProblemHelper_Tests
    {
        private const string XOR_FILE = @"datasets\xor\xor.ds";
        private readonly string path_to_xor_dataset;

        public ProblemHelper_Tests()
        {
            string current_path = Environment.CurrentDirectory;
            int pos = current_path.IndexOf("libsvm.net");
            string base_path = current_path.Substring(0, pos + 10);
            path_to_xor_dataset = Path.Combine(base_path, XOR_FILE);
        }



        [TestMethod]
        public void Should_be_able_to_read_files()
        {
            svm_problem problem = ProblemHelper.ReadProblem(path_to_xor_dataset);

            var expectedProblem = GetExpectedXorProblem();

            Assert.AreEqual(expectedProblem.l, problem.l);
            CollectionAssert.AreEqual(expectedProblem.y, problem.y);

            // We cannot use CollectionAssert so we use this ugly method
            for (var i = 0; i < problem.x.Length; i++)
            {
                for (var j = 0; j < problem.x[i].Length; j++)
                {
                    Assert.AreEqual(expectedProblem.x[i][j].index, problem.x[i][j].index, string.Format("Index value are different on line {0} element {1}", i + 1, j + 1));
                    Assert.AreEqual(expectedProblem.x[i][j].value, problem.x[i][j].value, string.Format("Value are different on line {0} element {1} ", i + 1, j + 1));
                }
            }
        }

        [TestMethod]
        public void Should_be_able_to_scale_problems()
        {
            var prob = new svm_problem
            {
                l = 2,
                x = new[]
                {
                    new[]
                    {
                        new svm_node {index = 1, value = 1},
                        new svm_node {index = 2, value = 2}
                    },
                    new[]
                    {
                        new svm_node {index = 1, value = 3},
                        new svm_node {index = 2, value = 4}
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

        [TestMethod]
        public void Should_be_able_to_read_valid_dataset()
        {
            // Dataset extracted from first five rows of mpg.ds file.
            var dataset = new List<List<double>>
            {
                new List<double> {18, 8, 307.0, 130.0, 3504.0, 12.0, 70, 1},
                new List<double> {15, 8, 350.0, 165.0, 3693.0, 11.5, 70, 1},
                new List<double> {18, 8, 318.0, 150.0, 3436.0, 11.0, 70, 1},
                new List<double> {16, 8, 304.0, 150.0, 3433.0, 12.0, 70, 1},
                new List<double> {17, 8, 302.0, 140.0, 3449.0, 10.5, 70, 1}
            };

            svm_problem prob = ProblemHelper.ReadAndScaleProblem(dataset);

            // Data count
            int dataCount = dataset.Count();
            Assert.AreEqual(prob.l, dataCount, 0.01);
            Assert.AreEqual(prob.x.Count(), dataCount, 0.01);
            Assert.AreEqual(prob.y.Count(), dataCount, 0.01);

            // Scale
            Assert.AreEqual(prob.x.Max(v => v.Max(n => n.value)), 1.0, 0.1);
            Assert.AreEqual(prob.x.Min(v => v.Max(n => n.value)), 0.0, 0.1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Should_be_able_to_detect_null_dataset()
        {
            ProblemHelper.ReadAndScaleProblem((List<List<double>>)null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_be_able_to_detect_void_dataset()
        {
            ProblemHelper.ReadAndScaleProblem(new List<List<double>>());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidFeatureException))]
        public void Should_be_able_to_detect_invalid_feature_count_in_dataset()
        {
            var dataset = new List<List<double>>
            {
                new List<double> {18, 8, 307.0, 130.0, 3504.0, 12.0, 70, 1},
                new List<double> {15, 8, 350.0, 165.0, 3693.0, 11.5, 70, 1},
                new List<double> {18, 8, 318.0, 150.0, 11.0, 70, 1}, // miss the 4th feature column.
                new List<double> {16, 8, 304.0, 150.0, 3433.0, 12.0, 70, 1},
                new List<double> {17, 8, 302.0, 140.0, 3449.0, 10.5, 70, 1}
            };

            ProblemHelper.ReadAndScaleProblem(dataset);
        }


        private List<svm_node[]> GetXorNode()
        {
            return new List<svm_node[]>
            {
                new[]
                {
                    new svm_node
                    {
                        index = 1,
                        value = -1
                    },
                    new svm_node
                    {
                        index = 2,
                        value = -1
                    }
                },
                new[]
                {
                    new svm_node
                    {
                        index = 1,
                        value = -1
                    },
                    new svm_node
                    {
                        index = 2,
                        value = 1
                    }
                },
                new[]
                {
                    new svm_node
                    {
                        index = 1,
                        value = 1
                    },
                    new svm_node
                    {
                        index = 2,
                        value = -1
                    }
                },
                new[]
                {
                    new svm_node
                    {
                        index = 1,
                        value = 1
                    },
                    new svm_node
                    {
                        index = 2,
                        value = 1
                    }
                }
            };
        }

        private svm_problem GetExpectedXorProblem()
        {
            var y = new double[] { 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0 };


            #region x declaration

            var xList = new List<svm_node[]>(4);
            for (int i = 0; i < 4; i++)
            {
                xList.AddRange(GetXorNode());
            }

            #endregion

            return new svm_problem
            {
                y = y,
                x = xList.ToArray(),
                l = y.Length
            };
        }
    }
}