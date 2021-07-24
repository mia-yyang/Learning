using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactoryDemo
{
    public abstract class Repository
    {
        /// <summary>
        /// 抽象方法，获取数据
        /// </summary>
        public abstract void GetData();
    }

    /// <summary>
    /// 定义一个 EFCore 仓储
    /// 继承仓储父类
    /// </summary>
    public class RepositoryEFCore : Repository
    {
        /// <summary>
        /// 实现父类抽象方法
        /// </summary>
        public override void GetData()
        {
            Console.WriteLine("获取 EFCore 全部数据！");
        }
    }

    /// <summary>
    /// 定义一个 Sqlsugar 仓储
    /// </summary>
    public class RepositorySqlsugar : Repository
    {
        public override void GetData()
        {
            Console.WriteLine("获取 Sqlsugar 全部数据！");
        }
    }
}
