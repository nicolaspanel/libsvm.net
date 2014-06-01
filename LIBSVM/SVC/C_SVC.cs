namespace libsvm
{
    public class C_SVC : SVC
    {
        /// <summary>
        ///     Classification SVM
        ///     Supports multi-class classification
        /// </summary>
        /// <param name="prob">Training Data Set</param>
        /// <param name="kernel">Selected Kernel</param>
        /// <param name="C">Cost parameter </param>
        /// <param name="cache_size">Indicates the maximum memory that the program can use </param>
        /// <param name="probability">Set this parameter to true if you want to use the PredictProbabilities function</param>
        public C_SVC(svm_problem prob, Kernel kernel, double C, double cache_size = 100, bool probability = false)
            : base(SvmType.C_SVC, prob, kernel, C, cache_size, probability ? 1 : 0)
        {
        }

        /// <summary>
        ///     Classification SVM
        ///     Supports multi-class classification
        /// </summary>
        /// <param name="input_file_name">Path to the training data set file. Has respect the libsvm format</param>
        /// <param name="kernel">Selected Kernel</param>
        /// <param name="C">Cost parameter </param>
        /// <param name="cache_size">Indicates the maximum memory that the program can use </param>
        /// <param name="probability">Set this parameter to true if you want to use the PredictProbabilities function</param>
        public C_SVC(string input_file_name, Kernel kernel, double C, double cache_size = 100, bool probability = false)
            : this(ProblemHelper.ReadProblem(input_file_name), kernel, C, cache_size, probability)
        {
        }

        /// <summary>
        ///     Classification SVM
        ///     Supports multi-class classification
        /// </summary>
        /// <param name="model_file_name">Path to the SVM model file.</param>
        public C_SVC(string model_file_name)
            : base(model_file_name)
        {
        }
    }
}