using System;
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace BackStore.Application
{
    [DependsOn(
        typeof(AbpDddApplicationModule)
        )]
    public class BackStoreApplicationModule : AbpModule
    {
    }
}
