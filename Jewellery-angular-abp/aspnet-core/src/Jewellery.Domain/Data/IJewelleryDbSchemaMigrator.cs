using System.Threading.Tasks;

namespace Jewellery.Data
{
    public interface IJewelleryDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
