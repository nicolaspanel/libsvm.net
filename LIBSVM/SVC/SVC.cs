using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace libsvm
{
    public abstract class SVC : SVM
    {
        public SVC(SvmType svm_type, svm_problem prob, Kernel kernel, double C, double cache_size = 100, int probability = 0)
            : base(prob, (int)svm_type, kernel, C, 0.0, cache_size, 1e-3, 0.1, 1, probability, 0, new int[0], new double[0])
        {
        }
          
        public SVC(string model_file_name)
            : base(model_file_name)
        {
        }

        public override double Predict(svm_node[] x)
        {
            if (model == null)
                throw new Exception("No trained svm model");

            return svm.svm_predict(model, x);
        }

        public Dictionary<int, double> PredictProbabilities(svm_node[] x)
        {
            if (this.model == null)
                throw new Exception("No trained svm model");

            var probabilities = new Dictionary<int, double>();
            int nr_class = model.nr_class;

            double[] prob_estimates = new double[nr_class];
            int[] labels = new int[nr_class];
            svm.svm_get_labels(model, labels);

            svm.svm_predict_probability(this.model, x, prob_estimates);
            for (int i = 0; i < nr_class; i++)
                probabilities.Add(labels[i], prob_estimates[i]);

            return probabilities;
        }

        public double GetCrossValidationAccuracy(int nr_fold)
        {
            int i;
            int total_correct = 0;
            double[] target = new double[prob.l];

            svm.svm_cross_validation(prob, param, nr_fold, target);

            for (i = 0; i < prob.l; i++)
                if (Math.Abs(target[i] - prob.y[i]) < double.Epsilon)
                    ++total_correct;
            var CVA = total_correct / (double) prob.l;
            Debug.WriteLine("Cross Validation Accuracy = {0:P} ({1}/{2})", CVA, total_correct, prob.l);
            return CVA;
        }
    }
}
