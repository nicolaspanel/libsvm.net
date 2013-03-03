using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libsvm
{
    public class Epsilon_SVR: SVR
    {
        /// <summary>
        /// Create and train the Epsilon_SV
        /// </summary>
        /// <param name="prob">Training Data Set</param>
        /// <param name="kernel">Selected Kernel</param>
        /// <param name="probability">Specify if probability are needed</param>
        /// <param name="cache_size">Indicates the maximum memory that can use the program</param>
        public Epsilon_SVR(svm_problem prob, Kernel kernel, double C, double epsilon, bool probability = true, double cache_size = 100)
            : base(SvmType.EPSILON_SVR, prob, kernel, C, epsilon, probability, cache_size)
        {
        }

        /// <summary>
        /// Read and scale inputs, create and train the Epsilon_SVR
        /// </summary>
        /// <param name="input_file_name">Training file path</param>
        /// <param name="kernel">Selected Kernel</param>
        /// <param name="probability">Specify if probability are needed</param>
        /// <param name="cache_size">Indicates the maximum memory that can use the program</param>
        public Epsilon_SVR(string input_file_name, Kernel kernel, double C, double epsilon, bool probability = true, double cache_size = 100)
            : this(ProblemHelper.ReadAndScaleProblem(input_file_name), kernel, C, epsilon, probability, cache_size) 
        {
        }
    }
}
