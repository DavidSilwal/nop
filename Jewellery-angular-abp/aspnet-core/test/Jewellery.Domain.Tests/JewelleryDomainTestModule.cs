using Jewellery.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Jewellery
{
    [DependsOn(
        typeof(JewelleryEntityFrameworkCoreTestModule)
        )]
    public class JewelleryDomainTestModule : AbpModule
    {

    }
}