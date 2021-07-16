using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateDemo
{
    public interface ILeader
    {
        void Raise(string hand);
        void Fall();
    }

    public interface ISubordinate
    {
        void Attack();
    }

    /// <summary>
    /// 首领A
    /// </summary>
    public class AA : ILeader
    {
        public int State { get; set; } = 0;
        private List<ISubordinate> _subordinates = new List<ISubordinate>();

        public void Attach(ISubordinate subordinate)
        {
            _subordinates.Add(subordinate);
        }

        public void Detach(ISubordinate subordinate)
        {
            _subordinates.Remove(subordinate);
        }

        public void Raise(string hand)
        {

            this.Notify();
        }

        public void Fall()
        {

            this.Notify();
        }

        public void Notify()
        {
            Console.WriteLine("Leader: Notifying subordinates...");

            foreach (var subordinate in _subordinates)
            {
                subordinate.Attack();
            }
        }
    }

    /// <summary>
    /// 部下B
    /// 若首领A左手举杯，则B攻击
    /// 若首领A摔杯，则AC攻击
    /// </summary>
    public class BB : ISubordinate
    {
        private readonly ILeader _leader;

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
    /// 若首领A右手举杯，则C攻击
    /// 若首领A摔杯，则AC攻击
    /// </summary>
    public class CC : ISubordinate
    {
        private readonly ILeader _leader;

        /// <summary>
        /// 攻击
        /// </summary>
        public void Attack()
        {
            Console.WriteLine("部下C发送攻击");
        }
    }
}
