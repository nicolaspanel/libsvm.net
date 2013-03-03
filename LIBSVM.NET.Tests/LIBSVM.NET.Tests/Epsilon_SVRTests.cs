using libsvm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LIBSVM.NET.Tests
{
    
    
    /// <summary>
    ///Classe de test pour Epsilon_SVRTests, destinée à contenir tous
    ///les tests unitaires Epsilon_SVRTests
    ///</summary>
    [TestClass()]
    public class Epsilon_SVRTests
    {
        double epsilon = 1.0;
        private const string TRAINING_FILE = "C:\\Users\\user\\Documents\\visual studio 2010\\Projects\\LIBSVM\\LIBSVM.NET.Tests\\DataSets\\mpg\\mpg.ds";
        svm_problem prob = ProblemHelper.ReadAndScaleProblem(TRAINING_FILE);
        svm_problem test = ProblemHelper.ReadAndScaleProblem(TRAINING_FILE);
        Kernel kernel = KernelHelper.RadialBasisFunctionKernel(2.0);
        double C = 38;

        /// <summary>
        ///Test pour Constructeur Epsilon_SVR
        ///</summary>
        [TestMethod()]
        public void Epsilon_SVRConstructorTest()
        {
            var svm = new Epsilon_SVR(prob, kernel, C, epsilon);
            Assert.IsNotNull(svm);
        }

        /// <summary>
        ///Test pour Constructeur Epsilon_SVR
        ///</summary>
        [TestMethod()]
        public void Epsilon_SVRConstructorTest1()
        {
            var svm = new Epsilon_SVR(TRAINING_FILE, kernel, C, epsilon);
            Assert.IsNotNull(svm);
        }

        /// <summary>
        ///Test pour GetCrossValidationSqsuaredCorrelationCoefficient
        ///</summary>
        [TestMethod()]
        public void GetCrossValidationSqsuaredCorrelationCoefficientTest()
        {
            var svm = new Epsilon_SVR(prob, kernel, C, epsilon);
            double CVS = svm.GetCrossValidationSqsuaredCorrelationCoefficient();

        }

        /// <summary>
        ///Test pour GetMeanSquaredError
        ///</summary>
        [TestMethod()]
        public void GetMeanSquaredErrorTest()
        {
            var svm = new Epsilon_SVR(prob, kernel, C, epsilon);
            double cms = svm.GetMeanSquaredError();
            Assert.IsTrue(cms > 0);
        }

        /// <summary>
        ///Test pour Predict
        ///</summary>
        [TestMethod()]
        public void PredictTest()
        {
            var svm = new Epsilon_SVR(prob, kernel, C, epsilon);
            var predictions = new double[prob.l];
            for (int i = 0; i < prob.l; i++)
            {
                var x = prob.x[i];
                var y = prob.y[i];
                predictions[i] = svm.Predict(x);

            }
        }
    }
}
