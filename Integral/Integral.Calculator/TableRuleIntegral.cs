using System;
using System.Threading.Tasks;

namespace Integral.Calculator
{
    public class TableRuleIntegral : IIntegral
    {
        public Task<double> Integrate(Func<double, double> f, double lowerBound, double upperBound, int n)
        {
            return Task.Run(() =>
            {
                var dx = (upperBound - lowerBound) / n;
                var result = f(lowerBound) + f(upperBound);

                for (var i = 1; i < n; i++)
                {
                    result += 2 * f(lowerBound + i * dx);
                }

                result *= dx / 2;
                return result;
            });
        }
    }
}
