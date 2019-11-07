using System;
using System.Threading.Tasks;

namespace Integral.Calculator
{
    public class SimpsonsRuleIntegral : IIntegral
    {
        public Task<double> Integrate(Func<double, double> f, double lowerBound, double upperBound, int n)
        {
            return Task.Run(() =>
            {
                var dx = (upperBound - lowerBound) / n;

                double result = f(lowerBound) + f(upperBound);
                for (int i = 1; i < n - 1; i++)
                    result += i % 2 == 1 ? 4 : 2 * f(lowerBound + i * dx);

                result *= dx / 3;
                return result;
            });
        }
    }
}
