using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace libsvm
{
    public static class ProblemHelper
    {
        public static svm_problem ReadProblem(string input_file_name)
        {
            var vy = new List<double>();
            var vx = new List<svm_node[]>();
            using (var sr = new StreamReader(input_file_name))
            {
                while (true)
                {
                    string line = sr.ReadLine();
                    if (line == null) break;

                    string[] st = line.Split(" \t\n\r\f".ToCharArray()).Where(c => c != String.Empty).ToArray();

                    vy.Add(st[0].ToDouble());

                    int m = (st.Count() - 1);
                    var x = new List<svm_node>();
                    for (int i = 0; i < m; i++)
                    {
                        string[] values = st[i + 1].Trim().Split(':');
                        double value = values[1].ToDouble();
                        x.Add(new svm_node
                        {
                            index = values[0].ToInteger(),
                            value = value,
                        });
                    }
                    vx.Add(x.ToArray());
                }
            }
            var prob = new svm_problem {l = vy.Count, x = vx.ToArray(), y = vy.ToArray()};

            return prob;
        }

        public static svm_problem ReadProblem(List<List<double>> dataset)
        {
            if (dataset == null)
            {
                throw new ArgumentNullException("dataset", "dataset passed in could not be null.");
            }

            if (dataset.Count == 0)
            {
                throw new Exception("dataset should contain at least one field");
            }

            var vy = new List<double>();
            var vx = new List<svm_node[]>();
            int featureCount = dataset.First().Count - 1;

            for (int i = 0; i < dataset.Count(); i++)
            {
                vy.Add(dataset[i][0]);

                if (!((dataset[i].Count - 1).Equals(featureCount)))
                {
                    throw new Exception(string.Format("The features extracted from the {0} row of dataset does not equal to {1}. Missing one or more feature columns?", i, featureCount));
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
            int index_max = prob.x.Max(X => X.Max(e => e.index));
            var feature_max = new double[(index_max + 1)];
            var feature_min = new double[(index_max + 1)];
            int n = prob.l;

            for (int i = 0; i <= index_max; i++)
            {
                feature_max[i] = -Double.MaxValue;
                feature_min[i] = Double.MaxValue;
            }

            for (int i = 0; i < n; i++)
            {
                int m = prob.x[i].Count();
                for (int j = 0; j < m; j++)
                {
                    int index = prob.x[i][j].index;
                    feature_max[index - 1] = Math.Max(feature_max[index - 1], prob.x[i][j].value);
                    feature_min[index - 1] = Math.Min(feature_min[index - 1], prob.x[i][j].value);
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
                    double max = feature_max[index - 1];
                    double min = feature_min[index - 1];

                    scaledProb.x[i][j] = new svm_node {index = index};

                    if (min == max)
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

        public static svm_problem ReadAndScaleProblem(string input_file_name, double lower = -1.0, double upper = 1.0)
        {
            return ScaleProblem(ReadProblem(input_file_name), lower, upper);
        }

        public static svm_problem ReadAndScaleProblem(List<List<double>> dataset, double lower = -1.0, double upper = 1.0)
        {
            return ScaleProblem(ReadProblem(dataset), lower, upper);
        }

        public static void WriteProblem(string output_file_name, svm_problem prob)
        {
            using (var sw = new StreamWriter(output_file_name))
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