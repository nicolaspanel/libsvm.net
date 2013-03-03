using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using libsvm;
using System.Diagnostics;

namespace LIBSVM.NET.Tests
{
    /// <summary>
    /// Description résumée pour XorTest
    /// </summary>
    [TestClass]
    public class XorTest
    {

        const string XOR_TRAINING_FILE = "C:\\Users\\user\\Documents\\visual studio 2010\\Projects\\LIBSVM\\LIBSVM.NET.Tests\\DataSets\\XOR\\XOR_TRAINING.ds";

        [TestMethod]
        public void XORTest()
        {
            var range = Enumerable.Range(-10,16);
            var log2gammas = range.Select(i => Math.Pow(2, i));
            var log2Cs = range.Select(i => Math.Pow(2, i + 1));
            var log2Rs = range.Select(i => Math.Pow(2, i + 1));
            var prob = ProblemHelper.ReadProblem(XOR_TRAINING_FILE);
            //Assert.IsTrue(prob.l == 4);
            Tuple<double, double, double, int> best = Tuple.Create(0.0, 0.0 ,0.0, prob.l);
            foreach (var g in log2gammas)
            {
                foreach (var c in log2Cs)
                {
                    foreach (var r in log2Rs)
                    {
                        var svm = new C_SVC(prob, KernelHelper.SigmoidKernel(g,r), c);
                        var errorCout = 0;
                        for (int i = 0; i < prob.l; i++)
                        {
                            //var x = (prob.x[i].FirstOrDefault(xi => xi.index == 1) == null) ? 0.0 : prob.x[i].FirstOrDefault(xi => xi.index == 1).value;
                            //var y = (prob.x[i].FirstOrDefault(xi => xi.index == 2) == null) ? 0.0 : prob.x[i].FirstOrDefault(xi => xi.index == 2).value;
                            var z = svm.Predict(prob.x[i]);
                            var probabilities = svm.PredictProbabilities(prob.x[i]);
                            if (z != prob.y[i])
                                errorCout++;
                            //Debug.WriteLine(String.Format("x={0} & y={1} => z={2} -- {3}", x, y, z, z == prob.y[i]));
                        }
                        if (errorCout < best.Item4)
                            best = Tuple.Create(g, c, r, errorCout);
                        //Debug.WriteLine(String.Format("g={0} && C={1} && C={2} => Error rate = {3}%", g, c, r, (double)errorCout / prob.l * 100));
                    }
                }
            }
            Debug.WriteLine(String.Format("BEST :: g={0} && C={1} && R={2} => Error rate = {3}%", best.Item1, best.Item2, best.Item3, (double)best.Item4 / (double)prob.l * 100));
        }
    }
}
