using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AopDemo
{
    public class BDispatchProxy : DispatchProxy
    {
        public static T GetObject<T>()
        {
            return DispatchProxy.Create<T, BDispatchProxy>();
        }

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            Console.WriteLine("Proxy B: " + targetMethod.Name);
            return null;
        }
    }
}
