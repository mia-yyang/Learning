using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDemo
{
    ///// <summary>
    ///// 定义仓储类
    ///// </summary>
    //public abstract class Repository
    //{
    //    // 可以进行各种操作，无论是EFCore，还是Sqlsugar的
    //    public abstract void GetData();
    //}


    /// <summary>
    /// 抽象工厂类
    /// </summary>
    public abstract class Repository
    {
        /// <summary>
        /// 抽象方法，用来返回仓储对象
        /// </summary>
        public abstract Repository GetData();
    }

}
