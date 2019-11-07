using System;
using System.Threading.Tasks;

namespace Integral.Calculator
{
    public class RectangleRuleIntegral : IIntegral
    {
        public Task<double> Integrate(Func<double, double> f, double lowerBound, double upperBound, int n)
        {
            return Task.Run(() =>
            {
                double result = 0;
                var dx = (upperBound - lowerBound) / n;

                for (var i = lowerBound; i < upperBound; i += dx)
                {
                    result += dx * f(i);
                }

                return result;
            });
        }
    }
}
