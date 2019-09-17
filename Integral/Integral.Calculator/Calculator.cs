using System;
using CenterSpace.NMath.Core;

namespace Integral.Calculator
{
    public class Calculator
    {
        public double Integrate(Func<double, double> func, double from, double to)
        {
            OneVariableFunction.DefaultIntegrator = new GaussKronrodIntegrator();

            var f = new OneVariableFunction(func);
            double integral = f.Integrate(from, to);

            return integral;
        }
    }
}
