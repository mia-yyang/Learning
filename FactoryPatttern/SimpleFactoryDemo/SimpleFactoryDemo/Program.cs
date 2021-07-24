using System;

namespace SimpleFactoryDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // 初始化创建Repository的两个仓储工厂
            RepositoryFactory efcoreRepositoryFactory = new RepositoryFactory_EFCore();
            RepositoryFactory sugarRepositoryFactory = new RepositoryFactory_SqlSugar();

            // 生产efcore仓储的实例
            var efcoreRepository = efcoreRepositoryFactory.CreateRepository();
            efcoreRepository.GetData();

            //生产sugar仓储的实体
            var sugarRepository = sugarRepositoryFactory.CreateRepository();
            sugarRepository.GetData();

        }
    }
}
