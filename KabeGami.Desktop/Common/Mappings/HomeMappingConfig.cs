using KabeGami.Application.Homes.Results;
using KabeGami.Desktop.ViewModels.Common.Models;
using Mapster;

namespace KabeGami.Desktop.Common.Mappings;
internal class HomeMappingConfig
    : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<HomeResult, HomeDisplayModel>()
            .Map(dest => dest.HomeGuid, src => src.HomeGuid)
            .Map(dest => dest.DefaultKabe, src => src.DefaultKabe)
            .Map(dest => dest.Kabes, src => src.Kabes);

        config.NewConfig<KabeResult, KabeDisplayModel>()
            .Map(dest => dest.KabeGuid, src => src.KabeGuid)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Combination, src => src.Combination)
            .Map(dest => dest.CronSchedule, src => src.CronSchedule)
            .Map(dest => dest.GalleryGuid, src => src.GalleryGuid);
    }
}
