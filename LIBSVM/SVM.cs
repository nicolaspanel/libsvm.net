using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace libsvm
{
    public abstract class SVM
    {
        protected svm_problem prob;
        protected svm_parameter param;
        protected svm_model model;
        

        /// <summary>
        /// Default SVM
        /// </summary>
        /// <remarks>The class store svm parameters and create the model. 
        /// This way, you can use it to predict</remarks>
        public SVM(svm_problem prob, int svm_type, int kernel_type, int degree, 
            double C, double gamma, double coef0, double nu, double cache_size,
            double eps, double p, int shrinking, int probability, int nr_weight, 
            int[] weight_label, double[] weight)
            :this(prob, new svm_parameter()
            {
                svm_type = svm_type,
                kernel_type = kernel_type,
                degree = degree,
                C = C,
                gamma = gamma,
                coef0 = coef0,
                nu = nu,
                cache_size = cache_size,
                eps = eps,
                p = p,
                shrinking = shrinking,
                probability = probability,
                nr_weight = nr_weight,
                weight_label = weight_label,
                weight = weight,
            })
        { }
        /// <summary>
        /// Default SVM
        /// </summary>
        /// <remarks>The class store svm parameters and create the model.
        /// This way, you can use it to predict</remarks>
        public SVM(svm_problem prob, int svm_type,
            Kernel kernel, double C, 
            double nu, double cache_size,
            double eps, double p, int shrinking, int probability, int nr_weight,
            int[] weight_label, double[] weight)
            : this(prob, new svm_parameter()
            {
                svm_type = svm_type,
                kernel_type = (int)kernel.KernelType,
                degree = kernel.Degree,
                C = C,
                gamma = kernel.Gamma,
                coef0 = kernel.R,
                nu = nu,
                cache_size = cache_size,
                eps = eps,
                p = p,
                shrinking = shrinking,
                probability = probability,
                nr_weight = nr_weight,
                weight_label = weight_label,
                weight = weight,
            })
        { }
        /// <summary>
        /// Default SVM
        /// </summary>
        /// <remarks>The class store svm parameters and create the model.
        /// This way, you can use it to predict</remarks>
        public SVM(svm_problem prob, svm_parameter param)
        {
            var error = svm.svm_check_parameter(prob, param);
            if (error != null)
            {
                throw new Exception(error);
            }

            this.prob = prob;
            this.param = param;

            this.Train();
        }
        /// <summary>
        /// Default SVM
        /// </summary>
        /// <remarks>The class store svm parameters and create the model.
        /// This way, you can use it to predict</remarks>
        public SVM(string input_file_name, svm_parameter param)
            : this(ProblemHelper.ReadProblem(input_file_name), param)
        {
        }
        
        
        /// <summary>
        /// Train the SVM and save the model
        /// </summary>
        public void Train()
        {
            this.model = svm.svm_train(prob, param);
        }
        
        /// <summary>
        /// Provides the prediction
        /// </summary>
        public abstract double Predict(svm_node[] x);
    }
}
