using Volo.Abp.Modularity;

namespace Jewellery
{
    [DependsOn(
        typeof(JewelleryApplicationModule),
        typeof(JewelleryDomainTestModule)
        )]
    public class JewelleryApplicationTestModule : AbpModule
    {

    }
}