using AutoMapper;

namespace Jewellery
{
    public class JewelleryApplicationAutoMapperProfile : Profile
    {
        public JewelleryApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */


            CreateMap<Jewellery.MetalType.MetalType, MetalTypeDto>();
        }
    }
}
