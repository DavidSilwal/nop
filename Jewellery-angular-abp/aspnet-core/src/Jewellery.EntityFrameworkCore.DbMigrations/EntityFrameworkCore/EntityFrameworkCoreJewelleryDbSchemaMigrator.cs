using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Jewellery.Data;
using Volo.Abp.DependencyInjection;

namespace Jewellery.EntityFrameworkCore
{
    public class EntityFrameworkCoreJewelleryDbSchemaMigrator
        : IJewelleryDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreJewelleryDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the JewelleryMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<JewelleryMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}