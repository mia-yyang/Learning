using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AopDemo
{
    /// <summary>
    /// 性能很渣
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LoggingAdvice<T> : DispatchProxy
    {
        private T Object { set; get; }

        public static T CreateLogging(Func<T> creator)
        {
            object proxy = DispatchProxy.Create<T, LoggingAdvice<T>>();
            ((LoggingAdvice<T>)proxy).Object = creator();
            return (T)proxy;
        }
        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            Console.WriteLine($"开始执行 {targetMethod.Name}");

            var result = targetMethod.Invoke(Object, args);

            Console.WriteLine($"执行完成 {targetMethod.Name}");

            return result;
        }
    }
}
