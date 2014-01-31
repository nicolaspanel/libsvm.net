using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libsvm
{
    public class C_SVC : SVC
    {
        /// <summary>
        /// Classification SVM
        /// Supports multi-class classification
        /// </summary>
        /// <param name="prob">Training Data Set</param>
        /// <param name="kernel">Selected Kernel</param>
        /// <param name="C">Cost parameter </param>
        /// <param name="cache_size">Indicates the maximum memory that can use the program</param>
        public C_SVC(svm_problem prob, Kernel kernel, double C, double cache_size = 100)
            : base(SvmType.C_SVC, prob, kernel, C, cache_size)
        {
        }

        /// <summary>
        /// Classification SVM
        /// Supports multi-class classification
        /// </summary>
        /// <param name="input_file_name">Path to the training data set file. Has respect the libsvm format</param>
        /// <param name="kernel">Selected Kernel</param>
        /// <param name="C">Cost parameter </param>
        /// <param name="cache_size">Indicates the maximum memory that can use the program</param>
        public C_SVC(string input_file_name, Kernel kernel, double C, double cache_size = 100)
            : this(ProblemHelper.ReadProblem(input_file_name), kernel, C, cache_size)
        {
        }

        /// <summary>
        /// Classification SVM
        /// Supports multi-class classification
        /// </summary>
        /// <param name="model_file_name">Path to the SVM model file.</param>
        public C_SVC(string model_file_name)
            : base(model_file_name)
        {
        }
    }
}
