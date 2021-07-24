using AbstractFactoryDemo.Core;
using System;

namespace AbstractFactoryDemo.Repository.Sugar
{
    public class SugarRepositoryFactory : AbstractFactory
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
