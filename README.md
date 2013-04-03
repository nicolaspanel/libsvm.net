libsvm.net
==========

An easy and quick way to use the last libsvm library (3.16) in your .NET projects.

First of all, if you are not familiar with SVM, I highly recommend to read this [guide](http://www.csie.ntu.edu.tw/~cjlin/papers/guide/guide.pdf).

For more informations, see also : 
 * [libsvm](http://www.csie.ntu.edu.tw/~cjlin/libsvm/)
 * [IKVM](http://www.ikvm.net/)

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
    var predict = svm.Predict(x); // returns the predicted value 'y'
    var probabilities = svm.PredictProbabilities(x);  // returns the probabilities for each 'y' value
}
```
Look at the code for more informations.

Enjoy !
