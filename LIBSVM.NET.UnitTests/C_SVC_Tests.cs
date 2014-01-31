using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using libsvm;
using System.Collections.Generic;
using System.IO;

namespace LIBSVM.NET.UnitTests
{
    [TestClass]
    public class C_SVC_Tests
    {
        private const string XOR_DATASET = @"LIBSVM.NET.Examples\DataSets\xor\xor.ds.txt";
        private readonly string base_path ="";
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
            var svm = new C_SVC(xor_problem, KernelHelper.PolynomialKernel(2, 1 , 1), 1);

            checkXOR(svm);
            
        }

        [TestMethod]
        public void C_SVC_Should_enable_to_export_and_import_svm_models()
        {
            // note : K(u; v) = (u  v + 1)^2 kernel is able to feet exactly the xor function 
            // see http://www.doc.ic.ac.uk/~dfg/ProbabilisticInference/IDAPILecture18.pdf for more infos
            var svm = new C_SVC(xor_problem, KernelHelper.PolynomialKernel(2, 1, 1), 1);
            var file_name = System.IO.Path.Combine(base_path, "test_export_temp.txt");
            
            // make sure directory is clean
            if (File.Exists(file_name))
                File.Delete(file_name);
            
            svm.Export(file_name);

            Assert.IsTrue(File.Exists(file_name));

            var new_svm = new C_SVC(file_name);

            checkXOR(new_svm);
            
            File.Delete(file_name); // cleanup
        }

        private void checkXOR(SVM svm)
        {
            var predictions = new double[2, 2];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    var A = new svm_node() { index = 1, value = i == 0 ? -1 : 1 };
                    var B = new svm_node() { index = 2, value = j == 0 ? -1 : 1 };
                    predictions[i, j] = svm.Predict(new svm_node[] { A, B });
                }
            }
            Assert.AreEqual(predictions[0, 0], 0);
            Assert.AreEqual(predictions[0, 1], 1);
            Assert.AreEqual(predictions[1, 0], 1);
            Assert.AreEqual(predictions[1, 1], 0);
        }
    }
}
