using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libsvm
{
    public class Kernel
    {
        /// <summary>
        /// Specify the kernel and associated parameters
        /// </summary>
        /// <param name="kernelType">Set type of kernel function</param>
        /// <param name="gamma">Set gamma in kernel function</param>
        /// <param name="r">Set coef0 in kernel function, used for polynomial kernel Only</param>
        /// <param name="degree">Set degree in kernel function, used in polynomial kernel only</param>
        /// <remarks>The class is used to facilitate the management of parameters</remarks>
        /// <seealso cref="KernelHelper"/>
        public Kernel(KernelType kernelType, double gamma, double r, int degree)
        {
            this.KernelType = kernelType;
            this.Gamma = gamma;
            this.R = r;
            this.Degree = degree;
        }
        public KernelType KernelType { get; private set; }
        /// <summary>
        /// Gamma parameter, used in polynomial, RBF and sigmoid kernel
        /// </summary>
        public double Gamma { get; set; }
        /// <summary>
        /// R parameter (coef0), used for polynomial and sigmoid kernels
        /// </summary>
        public double R { get; private set; }
        /// <summary>
        /// Degree of the polynom, used for polynomial kernel only
        /// </summary>
        public int Degree { get; private set; }
    }
}
