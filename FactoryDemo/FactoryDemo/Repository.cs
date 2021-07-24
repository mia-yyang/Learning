using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDemo
{
    /// <summary>
    /// 定义仓储类
    /// </summary>
    public abstract class Repository
    {
        // 可以进行各种操作，无论是EFCore，还是Sqlsugar的
        public abstract void GetData();
    }

   
}
