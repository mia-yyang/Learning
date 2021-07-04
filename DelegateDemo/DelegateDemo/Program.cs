using System;

namespace DelegateDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            A a = new A(); // 定义首领A
            B b = new B(a); // 定义部下B
            C c = new C(a); // 定义部下C

            // 首领A左手举杯
            a.Raise("左");

            ////// 首领A右手举杯
            ////a.Raise("右");

            ////// 首领A摔杯
            ////a.Fall();

            /*
            var subject = new Subject();
            var observerA = new ConcreteObserverA();
            subject.Attach(observerA);

            var observerB = new ConcreteObserverB();
            subject.Attach(observerB);

            subject.SomeBusinessLogic();
            subject.SomeBusinessLogic();

            subject.Detach(observerB);

            subject.SomeBusinessLogic();
            */

            Console.ReadLine();
        }
    }
}
