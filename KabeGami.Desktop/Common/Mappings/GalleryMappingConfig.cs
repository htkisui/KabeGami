using KabeGami.Application.Galleries.Results;
using KabeGami.Desktop.ViewModels.Common.Models;
using Mapster;

namespace KabeGami.Desktop.Common.Mappings;
internal class GalleryMappingConfig 
    : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<GalleryResult, GalleryDisplayModel>()
            .Map(dest => dest.GalleryGuid, src => src.GalleryGuid)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.ImageGuids, src => src.ImageGuids);
    }
}
