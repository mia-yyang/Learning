using AbstractFactoryDemo.Core;
using System;

namespace AbstractFactoryDemo.Repository.EFCore
{
    public class EFCoreRepositoryFactory : AbstractFactory
    {
        public override PermissionRepository PermissionRepository()
        {
            throw new NotImplementedException();
        }

        public override RoleRepository RoleRepository()
        {
            throw new NotImplementedException();
        }

        public override UserRepository UserRepository()
        {
            throw new NotImplementedException();
        }
    }
}
