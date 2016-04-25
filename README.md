# New owner required
Since I have not been developing in .NET for a while and I don't use Windows anymore, I cannot maintain this library any longer. Let me know if you want to take over.


# libsvm.net 


libsvm.net is an easy way to use [Support Vector Machines](https://en.wikipedia.org/wiki/Support_vector_machine) in your .NET projects.

Current version : 2.1.8

##How to use it
First of all, if you are not familiar with SVM, I highly recommend to read this [guide](http://www.csie.ntu.edu.tw/~cjlin/papers/guide/guide.pdf).
 
You can use libsvm.net right now, using the following [NuGet package](http://nuget.org/packages/libsvm.net/ ) : 
`PM> Install-Package libsvm.net`

Note : For now, C-SVC and epsilon-SVR are supported. nu-SVC and nu-SVR will be available in future versions.

### Classification (using C_SVC)
The code below describe the main methods :
```c#
var prob = ProblemHelper.ReadAndScaleProblem(TRAINING_FILE);
var test = ProblemHelper.ReadAndScaleProblem(TEST_FILE);

var svm = new C_SVC(prob, KernelHelper.RadialBasisFunctionKernel(gamma), C);
var accuracy = svm.GetCrossValidationAccuracy(nr_fold);// with nr_fold > 1
for (int i = 0 ; i < test.l ; i++)
{
    var x = test.x[i];
    var y = test.y[i];
    var predictedY = svm.Predict(x); // returns the predicted value for 'x' attributes
    var probabilities = svm.PredictProbabilities(x);  // returns the probabilities for each class
    // Note : in about accuracy% of cases, 'predictedY' should be equal to 'y'
}
```
Of course you can choose other kernels if you want (see `KernelHelper` class for more informations).

### Regression (using Epsilon_SVR)
Regression methods are a little different but the spirit remains the same :
```c#
var svm = new Epsilon_SVR(prob, KernelHelper.RadialBasisFunctionKernel(gamma), C, epsilon);
double mse = svm.GetMeanSquaredError(); 
//...
var prediction = svm.Predict(x);
```

### Advance options
Since version 2.1.0 (thanks to [ccerhan](https://github.com/ccerhan)), you can also save your models into xml files. This avoids having to train your model again and again when the dataset did'nt changed.

## License
libsvm.net is provided under MIT License.

## Contributions
Feel free to fork and improve/enhance libsvm.net in any way your want.

If you feel that the community will benefit from your changes, please send a pull request!

## Contributors
 * [Can Erhan](https://github.com/ccerhan)
 * [oloopy](https://github.com/oloopy)

## How it works
libsvm.net uses the official [libsvm Java library](http://www.csie.ntu.edu.tw/~cjlin/libsvm/#java), version *3.17* translated to .NET using [IKVM](http://www.ikvm.net/). It also provides helpers and classes to facilitate its use in real projects.

For more informations, see also :
 * [libsvm](http://www.csie.ntu.edu.tw/~cjlin/libsvm/)
 * [IKVM](http://www.ikvm.net/)
 * [Wikipedia article about SVM](https://en.wikipedia.org/wiki/Support_vector_machine)


[![githalytics.com alpha](https://cruel-carlota.pagodabox.com/abd9bce9df6164dedaa164cbf971ed21 "githalytics.com")](http://githalytics.com/nicolaspanel/libsvm.net)
