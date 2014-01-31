using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

namespace libsvm
{
    public static class ProblemHelper
    {
        public static svm_problem ReadProblem(string input_file_name)
        {
            
            var vy = new List<double>();
            var vx = new List<svm_node[]>();
            using (StreamReader sr = new StreamReader(input_file_name))
            {
                while (true)
                {
                    var line = sr.ReadLine();
                    if (line == null) break;

                    var st = line.Split(" \t\n\r\f".ToCharArray()).Where(c=>c!=String.Empty).ToArray();

                    vy.Add(st[0].ToDouble());

                    int m = (st.Count()-1);
                    List<svm_node> x = new List<svm_node>();
                    for (int i = 0; i < m; i++)
                    {
                        var values = st[i + 1].Trim().Split(':');
                        var value = values[1].ToDouble();
                        x.Add( new svm_node()
                        {
                            index = values[0].ToInteger(),
                            value = value,
                        });
                        
                    }
                    vx.Add(x.ToArray());
                }

            }
            var prob = new svm_problem();
            prob.l = vy.Count;
            prob.x = vx.ToArray();
            prob.y = vy.ToArray();

            return prob;
        }

        public static svm_problem ScaleProblem(svm_problem prob, double lower = -1.0, double upper = 1.0)
        {
            var index_max = prob.x.Max(X => X.Max(e=>e.index));
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
                var m = prob.x[i].Count();
                for (int j = 0; j< m; j++)
                {
                    var index = prob.x[i][j].index;
                    feature_max[index - 1] = Math.Max(feature_max[index - 1], prob.x[i][j].value);
                    feature_min[index - 1] = Math.Min(feature_min[index - 1], prob.x[i][j].value);
                }
            }
            
            var scaledProb = new svm_problem();
            scaledProb.l = n;
            scaledProb.y = prob.y.ToArray();
            scaledProb.x = new svm_node[n][];
            for (int i = 0; i < n; i++)
            {
                var m = prob.x[i].Count();
                scaledProb.x[i] = new svm_node[m];
                for (int j = 0; j < m; j++)
                {
                    var index = prob.x[i][j].index;
                    var value = prob.x[i][j].value;
                    var max = feature_max[index - 1];
                    var min = feature_min[index - 1];

                    scaledProb.x[i][j] = new svm_node() { index = index };

                    if (min == max)
                        scaledProb.x[i][j].value = 0;
                    else
                        scaledProb.x[i][j].value = lower + (upper - lower) * (value - min) / (max - min);
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



        public static void WriteProblem(string output_file_name, svm_problem problem)
        {
            using (StreamWriter sw = new StreamWriter(output_file_name))
            {
                for (int i =0 ; i< problem.l;i++)
                {
                    var sb = new StringBuilder();
                    sb.AppendFormat("{0} ", problem.y[i] );
                    for (int j = 0; j < problem.x[i].Count(); j++)
                    {
                        var node = problem.x[i][j];
                        sb.AppendFormat("{0}:{1} ", node.index, node.value);
                    }
                    sw.WriteLine(sb.ToString().Trim());
                }
                sw.Close();
            }
        }
        
    }
}
