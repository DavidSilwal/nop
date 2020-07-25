using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Jewellery.HttpApi.Client.ConsoleTestApp
{
    [DependsOn(
        typeof(JewelleryHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class JewelleryConsoleApiClientModule : AbpModule
    {
        
    }
}
