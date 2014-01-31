using libsvm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LIBSVM.NET.Examples
{
    [TestClass()]
    public class Epsilon_SVR_Examples
    {
        double epsilon = 1.0;
        private const string TRAINING_FILE = @"dataSets\mpg\mpg.ds"; //File containing training samples
        private const string TEST_FILE = @"dataSets\mpg\mpg.ds.t"; //File containing test samples
        // Find more datasets in the libsvm official website : http://www.csie.ntu.edu.tw/~cjlin/libsvmtools/datasets/regression.html
        svm_problem training_prob;
        svm_problem test_prob;
        static double C = 38;
        static double gamma = 2.0;

        [TestInitialize()]
        public void TestInitialize()
        {
            var path = Environment.CurrentDirectory;
            var pos = path.IndexOf("libsvm.net");
            var basePath = path.Substring(0, pos + 10);
            training_prob = ProblemHelper.ReadAndScaleProblem(System.IO.Path.Combine(basePath, TRAINING_FILE));
            test_prob = ProblemHelper.ReadAndScaleProblem(System.IO.Path.Combine(basePath,TEST_FILE));
        }

        /// <summary>
        ///Show how to get the sqsuared correlation coefficient using cross validation method
        ///Note : cros validation use the full dataset to increase the accuracy
        ///</summary>
        //[TestMethod()]
        public void GetCrossValidationSqsuaredCorrelationCoefficientTest()
        {
            var svm = new Epsilon_SVR(training_prob, KernelHelper.RadialBasisFunctionKernel(gamma), C, epsilon);
            double CVS = svm.GetCrossValidationSqsuaredCorrelationCoefficient();
        }

        /// <summary>
        ///Show how to get the Mean Squared Error
        ///</summary>
        //[TestMethod()]
        public void GetMeanSquaredErrorTest()
        {
            var svm = new Epsilon_SVR(training_prob, KernelHelper.RadialBasisFunctionKernel(gamma), C, epsilon);
            double cms = svm.GetMeanSquaredError();
            Assert.IsTrue(cms > 0);
        }

        /// <summary>
        ///Show how predict values for regression problems
        ///</summary>
        //[TestMethod()]
        public void PredictTest()
        {
            //Train the svm with the training datatset
            var svm = new Epsilon_SVR(training_prob, KernelHelper.RadialBasisFunctionKernel(gamma), C, epsilon);
    
            for (int i = 0; i < test_prob.l; i++)
            {
                var x = test_prob.x[i];
                var expectedValue = test_prob.y[i];
                var predictedValue = svm.Predict(x);
                Console.WriteLine(
                    String.Format(
                        "Predicted value = {0} || Expected value = {1} || Error = {2}", 
                        predictedValue, 
                        expectedValue, 
                        Math.Abs(predictedValue - expectedValue)));
            }
        }
    }
}
