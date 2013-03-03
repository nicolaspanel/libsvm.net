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

For more informations about how to use it, see : 
* [C_SVC unit tests](https://github.com/nicolaspanel/libsvm.net/blob/master/LIBSVM.NET.Tests/LIBSVM.NET.Tests/C_SVCTests.cs) (Classification)
* [epsilon_SVR unit tests](https://github.com/nicolaspanel/libsvm.net/blob/master/LIBSVM.NET.Tests/LIBSVM.NET.Tests/Epsilon_SVRTests.cs) (Regression)

Feel free to send me your feedback.

Enjoy !
