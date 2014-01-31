using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace libsvm
{
    public abstract class SVR : SVM
    {

        public SVR(SvmType svm_type, svm_problem prob, Kernel kernel, double C, double eps, bool probability, double cache_size)
            : base(prob, (int) svm_type, kernel, C, 0.0, cache_size, 1e-3, 0.1, 1, probability ? 1 : 0, 0, new int[0], new double[0])
        {

        }

        public double GetMeanSquaredError(int nr_fold = 5)
        {
            double[] target = new double[prob.l];

            svm.svm_cross_validation(prob, param, nr_fold, target);

            double total_error = Enumerable.Range(0, prob.l).Sum(i => Math.Pow(target[i] - prob.y[i], 2));

            return (double) total_error / (double) prob.l;
        }

        public double GetCrossValidationSqsuaredCorrelationCoefficient(int nr_fold = 5)
        {
            double sumv = 0, sumy = 0, sumvv = 0, sumyy = 0, sumvy = 0;
            double[] target = new double[prob.l];

            svm.svm_cross_validation(prob, param, nr_fold, target);

            for (int i = 0; i < prob.l; i++)
            {
                double y = prob.y[i];
                double v = target[i];
                sumv += v;
                sumy += y;
                sumvv += v * v;
                sumyy += y * y;
                sumvy += v * y;
            }

            return ((prob.l * sumvy - sumv * sumy) * (prob.l * sumvy - sumv * sumy)) / ((prob.l * sumvv - sumv * sumv) * (prob.l * sumyy - sumy * sumy));

        }

        public override double Predict(svm_node[] x)
        {
            if (this.model == null)
                throw new Exception("No trained svm model");

            return svm.svm_predict(this.model, x);
        }
    }
}
