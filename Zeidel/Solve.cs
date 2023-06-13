using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zeidel
{
    internal class Solve
    {
        public double[] Funk(double[,] A, double[] b, int maxIterations, double epsilon)
        {
            int n = b.Length;
            double[] x = new double[n];
            double[] xPrev = new double[n];

            int iterations = 0;
            double error = double.MaxValue;

            while (iterations < maxIterations && error > epsilon)
            {
                Array.Copy(x, xPrev, n);

                for (int i = 0; i < n; i++)
                {
                    double sum = 0.0;
                    for (int j = 0; j < n; j++)
                    {
                        if (j != i)
                        {
                            sum += A[i, j] * x[j];
                        }
                    }

                    x[i] = (b[i] - sum) / A[i, i];
                }

                error = CalculateError(x, xPrev);
                iterations++;
            }

            Console.WriteLine("Converged after " + iterations + " iterations.");

            return x;
        }
        static double CalculateError(double[] x, double[] xPrev)
        {
            double error = 0.0;
            for (int i = 0; i < x.Length; i++)
            {
                error = Math.Max(error, Math.Abs(x[i] - xPrev[i]));
            }

            return error;
        }
    }
}
