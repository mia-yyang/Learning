using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AopDemo
{
    public class ADispatchProxy : DispatchProxy
    {
        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            Console.WriteLine("Proxy A: " + targetMethod.Name);
            return null;
        }
    }
}
