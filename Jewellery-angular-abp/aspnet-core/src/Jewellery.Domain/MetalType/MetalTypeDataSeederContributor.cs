using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace Jewellery.MetalType
{
    public class MetalTypeDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<MetalType, Guid> _metalTypeRepository;
        private readonly IGuidGenerator _guidGenerator;

        public MetalTypeDataSeederContributor(IRepository<MetalType, Guid> metalTypeRepository, IGuidGenerator guidGenerator)
        {
            _metalTypeRepository = metalTypeRepository;
            _guidGenerator = guidGenerator;

        }
        public async Task SeedAsync(DataSeedContext context)
        {


            if (await _metalTypeRepository.GetCountAsync() > 0)
            {
                await _metalTypeRepository.InsertAsync(new MetalType
                {
                    Name = "Gold",
                    Cost = 57000
                });
            }

        }
    }
}
