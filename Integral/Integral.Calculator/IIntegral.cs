using System;
using System.Threading.Tasks;

namespace Integral.Calculator
{
    public interface IIntegral
    {
        /// <summary>
        /// Strategy pattern. Integrates given function within given limits.
        /// </summary>
        /// <param name="f">Function to integrate.</param>
        /// <param name="lowerBound">Lower bound of function.</param>
        /// <param name="upperBound">Upper bound of focuntio</param>
        /// <param name="n">Number of divisions.</param>
        /// <returns>Integral approximation.</returns>
        Task<double> Integrate(Func<double, double> f, double lowerBound, double upperBound, int n);
    }
}
