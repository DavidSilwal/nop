using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Jewellery.EntityFrameworkCore
{
    [DependsOn(
        typeof(JewelleryEntityFrameworkCoreModule)
        )]
    public class JewelleryEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<JewelleryMigrationsDbContext>();
        }
    }
}
