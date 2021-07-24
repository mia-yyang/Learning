using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactoryDemo
{
    public abstract class RepositoryFactory
    {
        public abstract Repository CreateRepository();
    }

    public class RepositoryFactory_EFCore : RepositoryFactory
    {
        /// <summary>
        /// 重写，生成EFCore 仓储的实例
        /// </summary>
        /// <returns></returns>
        public override Repository CreateRepository()
        {
            return new RepositoryEFCore();
        }
    }

    public class RepositoryFactory_SqlSugar : RepositoryFactory
    {
        /// <summary>
        /// 重写，生成 SqlSugar 仓储的实例
        /// </summary>
        /// <returns></returns>
        public override Repository CreateRepository()
        {
            return new RepositorySqlsugar();
        }
    }
}
