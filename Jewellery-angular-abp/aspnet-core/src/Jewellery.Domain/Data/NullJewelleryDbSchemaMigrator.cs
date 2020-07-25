using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Jewellery.Data
{
    /* This is used if database provider does't define
     * IJewelleryDbSchemaMigrator implementation.
     */
    public class NullJewelleryDbSchemaMigrator : IJewelleryDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}