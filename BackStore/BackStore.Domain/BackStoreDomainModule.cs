using System;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace BackStore.Domain
{
    [DependsOn(
        typeof(AbpDddDomainModule)
        )]
    public class BackStoreDomainModule : AbpModule
    {
    }
}
