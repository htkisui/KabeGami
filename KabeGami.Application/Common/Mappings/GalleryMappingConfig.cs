using KabeGami.Application.Galleries.Common.Results;
using KabeGami.Domain.Galleries;
using KabeGami.Domain.Galleries.Entities;
using KabeGami.Domain.Galleries.ValueObjects;
using Mapster;

namespace KabeGami.Application.Common.Mappings;
internal sealed class GalleryMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Gallery, GalleryResult>()
            .Map(dest => dest.GalleryGuid, src => src.Id.Value)
            .Map(dest => dest.SubGalleryGuid, src => src.SubGalleryId.Value)
            .Map(dest => dest.SubGalleriesResult, src => src.SubGalleries)
            .Map(dest => dest, src => src);

        config.NewConfig<SubGallery, SubGalleryResult>()
            .Map(dest => dest.SubGalleryGuid, src => src.Id.Value)
            .Map(dest => dest.ImageIdsResult, src => src.ImageIds)
            .Map(dest => dest, src => src);

        config.NewConfig<GalleryId, GalleryResult>()
            .Map(dest => dest.GalleryGuid, src => src.Value);
    }
}
