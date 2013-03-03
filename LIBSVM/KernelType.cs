using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libsvm
{
    public enum KernelType : int
    {
        LINEAR = 0,
        POLY = 1,
        RBF = 2,
        SIGMOID = 3,
    }
}
