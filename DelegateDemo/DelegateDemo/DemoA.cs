using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateDemo
{
    /*----------------------------------------------------------------------------
        C#中的委托和事件
    场景：首领A要搞一场鸿门宴，吩咐部下B和C各自带队埋伏在屏风两侧，约定以杯为令：
    左手举杯，则B带队杀出；若右手举杯，则C带队杀出；若直接摔杯，则B和C同时杀出。
    B和C袭击的具体方法，首领A并不关心。

    用一个IF条件判断语句不就搞定了吗?
    =>首领A会什么时候发出信号呢？估计连他自己都不知道。
      难道B和C就一直不停的用While循环做判断吗？

    正确的逻辑应该是，
    B和C不管宴席上发生的任何其他事情，只等首领发出举杯或者摔杯的信号，
    一旦首领A发出信号，相当于通知了所有部下，凡是约定好的部下，都立马各自行动！
    -------------------------------------------------------------------------------*/

    public delegate void RaiseEventHandler(string hand);
    public delegate void FallEventHandler();

    /// <summary>
    /// 首领A
    /// </summary>
    public class A
    {
        /// <summary>
        /// 首领A举杯事件
        /// </summary>
        public event RaiseEventHandler RaiseEvent;

        /// <summary>
        /// 首领A摔杯事件
        /// </summary>
        public event FallEventHandler FallEvent;

        /// <summary>
        /// 举杯
        /// </summary>
        /// <param name="hand"></param>
        public void Raise(string hand)
        {
            Console.WriteLine($"领导A{hand}手举杯");

            // 调用举杯事件，传入左或右手作为参数
            if (RaiseEvent != null)
            {
                RaiseEvent(hand);
            }
        }

        public void Fall()
        {
            Console.WriteLine("首领A摔杯");
            // 调用摔杯事件
            if (FallEvent != null)
            {
                FallEvent();
            }
        }
    }

    /// <summary>
    /// 部下B
    /// </summary>
    public class B
    {
        A a;
        public B(A a)
        {
            this.a = a;
            a.RaiseEvent += new RaiseEventHandler(A_RaiseEvent); // 订阅举杯事件
            a.FallEvent += new FallEventHandler(A_FallEvent); // 订阅摔杯事件
        }

        /// <summary>
        /// 首领举杯时的动作
        /// </summary>
        /// <param name="hand">若首领A左手举杯，则B攻击</param>
        void A_RaiseEvent(string hand)
        {
            if (hand.Equals("左"))
            {
                Attack();
            }
        }

        /// <summary>
        /// 首领摔杯时的动作
        /// </summary>
        void A_FallEvent()
        {
            Attack();
        }

        /// <summary>
        /// 攻击
        /// </summary>
        public void Attack()
        {
            Console.WriteLine("部下B发送攻击");
        }
    }

    /// <summary>
    /// 部下C
    /// </summary>
    public class C
    {
        A a;

        public C(A a)
        {
            this.a = a;
            a.RaiseEvent += new RaiseEventHandler(A_RaiseEvent); // 订阅举杯事件
            a.FallEvent += new FallEventHandler(A_FallEvent); // 订阅摔杯事件
        }

        /// <summary>
        /// 首领举杯时的动作
        /// </summary>
        /// <param name="hand">若首领A右手举杯，则C攻击</param>
        void A_RaiseEvent(string hand)
        {
            if (hand.Equals("右"))
            {
                Attack();
            }
        }

        /// <summary>
        /// 首领摔杯时的动作
        /// </summary>
        void A_FallEvent()
        {
            Attack();
        }

        /// <summary>
        /// 攻击
        /// </summary>
        public void Attack()
        {
            Console.WriteLine("部下C发送攻击");
        }
    }


}
