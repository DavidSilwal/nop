﻿using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Jewellery.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class JewelleryMigrationsDbContextFactory : IDesignTimeDbContextFactory<JewelleryMigrationsDbContext>
    {
        public JewelleryMigrationsDbContext CreateDbContext(string[] args)
        {
            JewelleryEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<JewelleryMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new JewelleryMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
