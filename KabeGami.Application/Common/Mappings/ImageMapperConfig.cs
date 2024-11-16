using KabeGami.Application.Images.Results;
using KabeGami.Domain.Images;
using Mapster;

namespace KabeGami.Application.Common.Mappings;
internal class ImageMapperConfig
    : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Image, ImageResult>()
            .Map(dest => dest.ImageGuid, src => src.Id.Value)
            .Map(dest => dest.ImageNameWithExtension, src => $"{src.Id.Value}{src.ImageExtension.Extension}");
    }
}
