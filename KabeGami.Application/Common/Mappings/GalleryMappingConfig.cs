using KabeGami.Application.Galleries.Results;
using KabeGami.Domain.Galleries;
using Mapster;

namespace KabeGami.Application.Common.Mappings;
internal class GalleryMappingConfig 
    : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Gallery, GalleryResult>()
            .Map(dest => dest.GalleryGuid, src => src.Id.Value)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.ImageGuids, src => src.ImageIds.Select(i => i.Value).ToList());

        config.NewConfig<Gallery, GalleryNameResult>()
            .Map(dest => dest.Name, src => src.Name);
    }
}
