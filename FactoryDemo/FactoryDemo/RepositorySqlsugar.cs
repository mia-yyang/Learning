using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDemo
{
    public class RepositorySqlsugar : Repository
    {
        public override void GetData()
        {
            Console.WriteLine("获取 Sqlsugar 全部数据！");
        }
    }
}
