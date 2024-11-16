using KabeGami.Application.Homes.Results;
using KabeGami.Domain.Homes;
using KabeGami.Domain.Homes.Entities;
using Mapster;

namespace KabeGami.Application.Common.Mappings;
internal class HomeMappingConfig 
    : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Home, HomeResult>()
            .Map(dest => dest.HomeGuid, src => src.Id.Value)
            .Map(dest => dest.DefaultKabe, src => src.DefaultKabe)
            .Map(dest => dest.Kabes, src => src.Kabes);

        config.NewConfig<Kabe, KabeResult>()
            .Map(dest => dest.KabeGuid, src => src.Id.Value)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Combination, src => src.Combination)
            .Map(dest => dest.CronSchedule, src => src.CronSchedule)
            .Map(dest => dest.GalleryGuid, src => src.GalleryId.Value);
    }
}
