using System;

namespace FactoryDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //// 实例化仓储，然后调用
            //Repository repository = new Repository();
            //repository.GetData();

            //// 实例化仓储，然后调用
            //RepositorySqlsugar repositorySqlsugar = new RepositorySqlsugar();
            //repositorySqlsugar.GetData();
            //RepositoryEFCore repositoryEFCore = new RepositoryEFCore();
            //repositoryEFCore.GetData();

            // 实例化仓储，然后调用
            Repository sugar = RepositorySimpleFactory.GetRepository("sugar");
            sugar.GetData();

            Repository efcore = RepositorySimpleFactory.GetRepository("efcore");
            efcore.GetData();


            Console.ReadLine();
        }
      
    }
}
