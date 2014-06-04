using System;
using System.IO; 
using libsvm;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LIBSVM.NET.UnitTests
{
    [TestClass]
    public class C_SVC_Tests
    {
        private const string XOR_DATASET = @"dataSets\xor\xor.ds";
        private readonly string base_path = "";
        private readonly svm_problem xor_problem;


        public C_SVC_Tests()
        {
            var current_path = Environment.CurrentDirectory;
            var pos = current_path.IndexOf("libsvm.net");
            base_path = current_path.Substring(0, pos + 10);
            string full_path = System.IO.Path.Combine(base_path, XOR_DATASET);
            xor_problem = ProblemHelper.ReadProblem(full_path);
        }

        [TestMethod]
        public void C_SVC_Should_predict_perfectly_XOR_dataset_with_polynomial_kernel()
        {
            // note : K(u; v) = (u  v + 1)^2 kernel is able to feet exactly the xor function 
            // see http://www.doc.ic.ac.uk/~dfg/ProbabilisticInference/IDAPILecture18.pdf for more infos
            var svm = new C_SVC(xor_problem, KernelHelper.PolynomialKernel(2, 1, 1), 1);

            checkXOR(svm);
        }

        [TestMethod]
        public void C_SVC_should_enable_to_export_and_import_svm_models()
        {
            // note : K(u; v) = (u  v + 1)^2 kernel is able to feet exactly the xor function 
            // see http://www.doc.ic.ac.uk/~dfg/ProbabilisticInference/IDAPILecture18.pdf for more infos
            var svm = new C_SVC(xor_problem, KernelHelper.PolynomialKernel(2, 1, 1), 1);
            var file_name = System.IO.Path.Combine(base_path, "test_export_temp.xml");

            // make sure directory is clean
            if (File.Exists(file_name))
                File.Delete(file_name);

            svm.Export(file_name);

            Assert.IsTrue(File.Exists(file_name));

            var new_svm = new C_SVC(file_name);

            checkXOR(new_svm);

            File.Delete(file_name); // cleanup
        }

        [TestMethod]
        public void C_SVC_should_always_return_the_same_cross_validation_accuracy_when_probability_is_false()
        {
            // Arrange
            var problem = CreateSimpleProblem();
            var model = new C_SVC(problem, KernelHelper.LinearKernel(), 1);

            // Act
            var results = new double[10];
            for (int i = 0; i < 10; i++)
            {
                results[i] = model.GetCrossValidationAccuracy(10);
            }
             
            //Assert 
            for (int i = 1; i < 10; i++)
            {
                Assert.AreEqual(0.90909090909090906, results[i]);
            } 
        }

        private void checkXOR(SVM svm)
        {
            var predictions = new double[2, 2];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    var A = new svm_node() {index = 1, value = i == 0 ? -1 : 1};
                    var B = new svm_node() {index = 2, value = j == 0 ? -1 : 1};
                    predictions[i, j] = svm.Predict(new svm_node[] {A, B});
                }
            }
            Assert.AreEqual(predictions[0, 0], 0);
            Assert.AreEqual(predictions[0, 1], 1);
            Assert.AreEqual(predictions[1, 0], 1);
            Assert.AreEqual(predictions[1, 1], 0);
        }

        private svm_problem CreateSimpleProblem()
        {
            var y = new double[] {1, -1, 1, -1, -1, -1, +1, +1, +1, +1, -1};
            #region x declaration 
            var x = new[]
            {
                new[]
                {
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
                    }
                },
                new[]
                {
                    new svm_node
                    {
                        index = 2,
                        value = 2
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
                },
                new[]
                {
                    new svm_node
                    {
                        index = 1,
                        value = 2
                    }
                },
                new[]
                {
                    new svm_node
                    {
                        index = 2,
                        value = 3
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
                        value = 2
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
                        value = 2
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
                        value = 2
                    }
                },
                new[]
                {
                    new svm_node
                    {
                        index = 1,
                        value = 2
                    },
                    new svm_node
                    {
                        index = 2,
                        value = 1
                    }
                }
            };
            #endregion

            return new svm_problem
            {
                y = y,
                x = x,
                l = y.Length
            };
        }
    }
}