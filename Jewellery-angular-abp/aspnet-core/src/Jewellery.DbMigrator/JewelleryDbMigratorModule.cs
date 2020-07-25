using Jewellery.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Jewellery.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(JewelleryEntityFrameworkCoreDbMigrationsModule),
        typeof(JewelleryApplicationContractsModule)
        )]
    public class JewelleryDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
