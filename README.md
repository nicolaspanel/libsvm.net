# libsvm.net
 
libsvm.net is an easy way to use [Support Vector Machines](https://en.wikipedia.org/wiki/Support_vector_machine) in your .NET projects.


##How to use it
First of all, if you are not familiar with SVM, I highly recommend to read this [guide](http://www.csie.ntu.edu.tw/~cjlin/papers/guide/guide.pdf).
 
You can use libsvm.net right now, using the following [NuGet package](http://nuget.org/packages/libsvm.net/ ) : 
PM> Install-Package libsvm.net

Note : For now, C-SVC and epsilon-SVR are supported. nu-SVC and nu-SVR will be available in future versions.

You can use it this way:
```c#
var prob = ProblemHelper.ReadAndScaleProblem(TRAINING_FILE);
var test = ProblemHelper.ReadAndScaleProblem(TEST_FILE);


var svm = new C_SVC(prob, KernelHelper.RadialBasisFunctionKernel(gamma), C);
var accuracy = svm.GetCrossValidationAccuracy(nr_fold);
for (int i = 0 ; i < test.l ; i++)
{
    var x = test.x[i];
    var y = test.y[i];
    var predictedY = svm.Predict(x); // returns the predicted value for 'x' attributes
    var probabilities = svm.PredictProbabilities(x);  // returns the probabilities for each class
    // Note : in about accuracy% of cases, 'predictedY' should be equal to 'y'
}
```
For more informations, take look to the [integration tests](https://github.com/nicolaspanel/libsvm.net/tree/master/LIBSVM.NET.Tests/LIBSVM.NET.Tests)

## How it works
libsvm.net uses the official [libsvm Java library](http://www.csie.ntu.edu.tw/~cjlin/libsvm/#java), version 3.16 translated to .NET using [IKVM](http://www.ikvm.net/). It also provides helpers and classes to facilitate its use in real projects.

For more informations, see also :
 * [libsvm](http://www.csie.ntu.edu.tw/~cjlin/libsvm/)
 * [IKVM](http://www.ikvm.net/)
 * [Wikipedia article about SVM](https://en.wikipedia.org/wiki/Support_vector_machine)

## License
libsvm.net is provided under MIT License

[![githalytics.com alpha](https://cruel-carlota.pagodabox.com/abd9bce9df6164dedaa164cbf971ed21 "githalytics.com")](http://githalytics.com/nicolaspanel/libsvm.net)
