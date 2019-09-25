using System;
using System.Threading.Tasks;
using CenterSpace.NMath.Core;

namespace Integral.Calculator
{
    public class Calculator
    {
        public Task<double> Integrate(Func<double, double> func, double from, double to)
        {
            return Task.Run(() =>
            {
                OneVariableFunction.DefaultIntegrator = new GaussKronrodIntegrator();
                var f = new OneVariableFunction(func);
                double integral = f.Integrate(from, to);

                return integral;
            });
        }
    }
}
