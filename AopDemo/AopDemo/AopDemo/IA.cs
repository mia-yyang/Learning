using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopDemo
{
    public interface IA
    {
        void Foo(string a);
    }
    public class A : IA
    {
        public void Foo(string a)
        {
            Console.WriteLine(a);
        }
    }
}
