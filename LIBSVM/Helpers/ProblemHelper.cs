using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace libsvm
{
    public static class ProblemHelper
    {
        public static svm_problem ReadProblem(string inputFileName)
        {
            var y = new List<double>();
            var x = new List<svm_node[]>(); 
            var lines = File.ReadAllLines(inputFileName);

            foreach (var tokens in lines.Select(line => line.Split(" \t\n\r\f".ToCharArray())
                                                            .Where(c => c != String.Empty).ToArray()))
            {
                y.Add(tokens[0].ToDouble()); 
                x.Add(GetNodes(tokens).ToArray());
            }

            return new svm_problem { l = y.Count, x = x.ToArray(), y = y.ToArray() }; 
        }

        private static IEnumerable<svm_node> GetNodes(IList<string> tokens)
        {
            for (var i = 1; i <= (tokens.Count() - 1); i++)
            {
                var token = tokens[i].Trim().Split(':');

                yield return new svm_node
                {
                    index = token[0].ToInteger(),
                    value = token[1].ToDouble(),
                };
            }
        }

        

        public static svm_problem ReadProblem(List<List<double>> dataset)
        {
            if (dataset == null)
            {
                throw new ArgumentNullException("dataset", "dataset passed in could not be null.");
            }

            if (dataset.Count == 0)
            {
                throw new ArgumentException("dataset should contain at least one field"); 
            }

            var vy = new List<double>();
            var vx = new List<svm_node[]>();
            int featureCount = dataset.First().Count - 1;

            for (int i = 0; i < dataset.Count(); i++)
            {
                vy.Add(dataset[i][0]);

                if (!((dataset[i].Count - 1).Equals(featureCount)))
                {
                    throw new InvalidFeatureException(string.Format("The features extracted from the {0} row of dataset does not equal to {1}. Missing one or more feature columns?", i, featureCount));
                }
                var x = new List<svm_node>();
                for (int j = 1; j < dataset[i].Count; j++)
                {
                    x.Add(new svm_node
                    {
                        index = j,
                        value = dataset[i][j],
                    });
                }
                vx.Add(x.ToArray());
            }

            return new svm_problem {l = dataset.Count(), x = vx.ToArray(), y = vy.ToArray()};
        }

        public static svm_problem ScaleProblem(svm_problem prob, double lower = -1.0, double upper = 1.0)
        {
            int indexMax = prob.x.Max(X => X.Max(e => e.index));
            var featureMax = new double[(indexMax + 1)];
            var featureMin = new double[(indexMax + 1)];
            int n = prob.l;

            for (int i = 0; i <= indexMax; i++)
            {
                featureMax[i] = -Double.MaxValue;
                featureMin[i] = Double.MaxValue;
            }

            for (int i = 0; i < n; i++)
            {
                int m = prob.x[i].Count();
                for (int j = 0; j < m; j++)
                {
                    int index = prob.x[i][j].index;
                    featureMax[index - 1] = Math.Max(featureMax[index - 1], prob.x[i][j].value);
                    featureMin[index - 1] = Math.Min(featureMin[index - 1], prob.x[i][j].value);
                }
            }

            var scaledProb = new svm_problem {l = n, y = prob.y.ToArray(), x = new svm_node[n][]};

            for (int i = 0; i < n; i++)
            {
                int m = prob.x[i].Count();
                scaledProb.x[i] = new svm_node[m];
                for (int j = 0; j < m; j++)
                {
                    int index = prob.x[i][j].index;
                    double value = prob.x[i][j].value;
                    double max = featureMax[index - 1];
                    double min = featureMin[index - 1];

                    scaledProb.x[i][j] = new svm_node {index = index};

                    if (Math.Abs(min - max) < double.Epsilon)
                        scaledProb.x[i][j].value = 0;
                    else
                        scaledProb.x[i][j].value = lower + (upper - lower)*(value - min)/(max - min);
                }
            }
            return scaledProb;
        }

        public static svm_problem Scale(this svm_problem prob, double lower = -1.0, double upper = 1.0)
        {
            return ScaleProblem(prob, lower, upper);
        }

        public static svm_problem ReadAndScaleProblem(string inputFileName, double lower = -1.0, double upper = 1.0)
        {
            return ScaleProblem(ReadProblem(inputFileName), lower, upper);
        }

        public static svm_problem ReadAndScaleProblem(List<List<double>> dataset, double lower = -1.0, double upper = 1.0)
        {
            return ScaleProblem(ReadProblem(dataset), lower, upper);
        }

        public static void WriteProblem(string outputFileName, svm_problem prob)
        {
            using (var sw = new StreamWriter(outputFileName))
            {
                for (int i = 0; i < prob.l; i++)
                {
                    var sb = new StringBuilder();
                    sb.AppendFormat("{0} ", prob.y[i]);
                    for (int j = 0; j < prob.x[i].Count(); j++)
                    {
                        svm_node node = prob.x[i][j];
                        sb.AppendFormat("{0}:{1} ", node.index, node.value);
                    }
                    sw.WriteLine(sb.ToString().Trim());
                }
                sw.Close();
            }
        }
    }
}