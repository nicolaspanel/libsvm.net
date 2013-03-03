using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using libsvm;

namespace LIBSVM.NET.Tests
{
    /// <summary>
    ///Classe de test pour SVMTests, destinée à contenir tous
    ///les tests unitaires SVMTests
    ///</summary>
    [TestClass()]
    public class C_SVCTests
    {
        private const string LEU_TEST_FILE = "C:\\Users\\user\\Documents\\visual studio 2010\\Projects\\LIBSVM\\LIBSVM.NET.Tests\\DataSets\\leukemia\\leu.ds.combined";
        private const string SVMGUIDE1_TEST_FILE = "C:\\Users\\user\\Documents\\visual studio 2010\\Projects\\LIBSVM\\LIBSVM.NET.Tests\\DataSets\\svmguide1\\svmguide1.ds.combined";
        double C = 0.8;
        Kernel kernel = KernelHelper.RadialBasisFunctionKernel(0.000030518125);

        svm_problem _prob; 

        [TestInitialize()]
        public void TestInitialize()
        {
            _prob = ProblemHelper.ReadAndScaleProblem(LEU_TEST_FILE);
        }

        /// <summary>
        ///Test pour Constructeur SVM
        ///</summary>
        [TestMethod()]
        public void SVMConstructorTest()
        {
            var svm = new C_SVC(_prob, kernel, C);
        }

        /// <summary>
        ///Test pour Constructeur SVM
        ///</summary>
        [TestMethod()]
        public void SVMConstructorTest1()
        {
            var svm = new C_SVC(LEU_TEST_FILE, kernel, C);
        }

        /// <summary>
        ///Test pour DoCrossValidation
        ///</summary>
        [TestMethod()]
        public void DoCrossValidationTest()
        {
            var svm = new C_SVC(_prob, kernel, C);
            var cva = svm.GetCrossValidationAccuracy(5);

            Assert.IsTrue(cva > 0);
        }

        /// <summary>
        ///Test pour Predict
        ///</summary>
        [TestMethod()]
        public void PredictTest()
        {
            var svm = new C_SVC(_prob, kernel, C);
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
        /// <summary>
        ///Test pour Predict
        ///</summary>
        [TestMethod()]
        public void DoCrossValidationTest2()
        {
            var prob2 = ProblemHelper.ReadAndScaleProblem(SVMGUIDE1_TEST_FILE);
            var svm = new C_SVC(prob2, KernelHelper.RadialBasisFunctionKernel(3.0), 2.0);
            var cva = svm.GetCrossValidationAccuracy(5);
        }
    }
}