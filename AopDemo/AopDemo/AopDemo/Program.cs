using System;
using System.Reflection;

namespace AopDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            IA a = DispatchProxy.Create<IA, ADispatchProxy>();
            a.Foo("123");

            BDispatchProxy.GetObject<IA>().Foo("456");

            var b = LoggingAdvice<IA>.CreateLogging(() => new A());
            b.Foo("ABC");


            Console.ReadLine();
        }
    }
}
