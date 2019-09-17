using System;
using System.CodeDom.Compiler;
using System.Reflection;
using Microsoft.CSharp;

namespace Integral.Compiler
{
    public class Compiler
    {
        private readonly CSharpCodeProvider m_provider = new CSharpCodeProvider();
        private readonly CompilerParameters m_parameters = new CompilerParameters();

        public Compiler()
        {
            m_parameters.GenerateInMemory = true;
            m_parameters.ReferencedAssemblies.Add("System.dll");
        }

        public CompilerResults Compile(string code)
        {
            CompilerResults results = m_provider.CompileAssemblyFromSource(m_parameters, GetCode(code));
            return results;
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
