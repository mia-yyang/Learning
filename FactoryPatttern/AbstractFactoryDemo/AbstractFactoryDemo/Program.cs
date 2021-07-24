using AbstractFactoryDemo.Core;
using AbstractFactoryDemo.Repository.EFCore;
using AbstractFactoryDemo.Repository.Sugar;
using System;

namespace AbstractFactoryDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // 实例化工厂，这里用来生产 efcore 这一系列的 产品
            AbstractFactory efcoreFactory = new EFCoreRepositoryFactory();
            efcoreFactory.UserRepository().Add();
            efcoreFactory.RoleRepository().Delete();
            efcoreFactory.PermissionRepository().Query();


            // 实例化工厂，这里用来生产 sugar 这一系列的 产品
            AbstractFactory sugarFactory = new SugarRepositoryFactory();
            sugarFactory.UserRepository().Add();
            sugarFactory.RoleRepository().Delete();
            sugarFactory.PermissionRepository().Query();

        }
    }
}
