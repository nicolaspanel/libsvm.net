using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using libsvm;

namespace LIBSVM.NET.Examples
{
    [TestClass()]
    public class C_SVC_Tests
    {
        // Full leukemia dataset
        private const string LEU_TEST_FILE = @"LIBSVM.NET.Examples\DataSets\leukemia\leu.ds.combined.txt";
        
        // Full svmguide1 dataset, see .\libsvm.net\LIBSVM.NET.Tests\DataSets\svmguide1 folder
        //private const string SVMGUIDE1_TEST_FILE = "svmguide1.ds.combined.txt";
        // Find more datasets in the libsvm official website : http://www.csie.ntu.edu.tw/~cjlin/libsvmtools/datasets/multiclass.html

        static double C = 0.8;
        static double gamma = 0.000030518125;

        svm_problem _prob; 

        [TestInitialize()]
        public void TestInitialize()
        {
            var path = Environment.CurrentDirectory;
            var pos = path.IndexOf("libsvm.net");
            var basePath = path.Substring(0, pos + 10);
            string fullPath = System.IO.Path.Combine(basePath, LEU_TEST_FILE);

            // get data from file
            // Note that you should always scale your data
            _prob = ProblemHelper.ReadAndScaleProblem(fullPath);
        }


        /// <summary>
        /// Show how to get the accuracy using cross validation method
        /// Assert accurancy is greater than zero
        ///</summary>
        [TestMethod()]
        public void DoCrossValidationTest()
        {
            var svm = new C_SVC(_prob, KernelHelper.RadialBasisFunctionKernel(gamma), C);
            var cva = svm.GetCrossValidationAccuracy(5); 

            Assert.IsTrue(cva > 0);
        }

        /// <summary>
        ///Show how to predict probabilities for classification problems
        ///Verify that the prediction is always the most probable class
        ///</summary>
        [TestMethod()]
        public void PredictTest()
        {
            var svm = new C_SVC(_prob, KernelHelper.RadialBasisFunctionKernel(gamma), C);
            var nb_class = _prob.y.Distinct().Count();
            for (int i = 0; i < _prob.l; i++)
            {
                var x = _prob.x[i];
                var y = _prob.y[i];
                var probabilities = svm.PredictProbabilities(x);
                var predict = svm.Predict(x);
                Assert.IsTrue(predict == probabilities.OrderByDescending(p => p.Value).First().Key);
                Assert.IsNotNull(probabilities);
                Assert.IsTrue(probabilities.Count == nb_class);
                var sum = probabilities.Sum(e => e.Value) ;
                
            }
        }
    }
}