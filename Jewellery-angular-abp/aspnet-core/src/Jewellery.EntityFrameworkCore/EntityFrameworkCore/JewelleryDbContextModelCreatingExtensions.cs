using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace Jewellery.EntityFrameworkCore
{
    public static class JewelleryDbContextModelCreatingExtensions
    {
        public static void ConfigureJewellery(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(JewelleryConsts.DbTablePrefix + "YourEntities", JewelleryConsts.DbSchema);

            //    //...
            //});
        }
    }
}