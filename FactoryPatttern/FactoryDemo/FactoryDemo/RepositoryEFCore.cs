using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDemo
{
    public class RepositoryEFCore : Repository
    {
        public override void GetData()
        {
            Console.WriteLine("获取 EFCore 全部数据！");
        }
    }
}
