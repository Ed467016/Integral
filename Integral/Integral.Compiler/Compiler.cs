using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.CSharp;

namespace Integral.Compiler
{
    public class Compiler
    {
        private static readonly Dictionary<string, string> _keys = new Dictionary<string, string>()
        {
            { "pow", "Math.Pow" },
            { "sqrt", "Math.Sqrt" },
            { "e", Math.E.ToString() },
            { "pi", Math.PI.ToString() }
        };

        private readonly CSharpCodeProvider m_provider = new CSharpCodeProvider();
        private readonly CompilerParameters m_parameters = new CompilerParameters();

        public Compiler()
        {
            m_parameters.GenerateInMemory = true;
            m_parameters.ReferencedAssemblies.Add("System.dll");
        }

        public Task<CompilerResults> Compile(string code)
        {
            return Task.Run(() =>
            {
                CompilerResults results = m_provider.CompileAssemblyFromSource(m_parameters, GetCode(code));
                return results;
            });
        }

        public Func<double, double> GetLambda(CompilerResults cr)
        {
            var asm = cr.CompiledAssembly.GetType("Dynamic.DynamicCode");
            var method = asm.GetMethod("DynamicMethod", BindingFlags.Static | BindingFlags.Public);
            var res = method.Invoke(null, null) as Func<double, double>;

            return res;
        }

        private static string[] GetCode(string code)
        {
            foreach (var key in _keys)
                code = code.Replace(key.Key, key.Value);

            return new string[]
            {
                @"using System;
 
                namespace Dynamic
                {
                    public static class DynamicCode
                    {
                        public static Func<double, double> DynamicMethod()
                        {
                            return x => " + code + @";
                        }
                    }
                }"
            };
        }
    }
}
