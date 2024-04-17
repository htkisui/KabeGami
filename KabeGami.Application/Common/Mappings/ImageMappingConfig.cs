using KabeGami.Application.Images.Common.Results;
using KabeGami.Domain.Images;
using KabeGami.Domain.Images.ValueObjects;
using Mapster;

namespace KabeGami.Application.Common.Mappings;
internal sealed class ImageMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Image, ImageResult>()
            .Map(dest => dest.ImageGuidResult, src => src.Id.Value)
            .Map(dest => dest.Extension, src => src.ImageExtension.Extension)
            .Map(dest => dest.CategoryDirectoryPath, src => src.ImageCategory.DirectoryPath);

        config.NewConfig<ImageId, ImageGuidResult>()
            .Map(dest => dest.ImageGuid, src => src.Value);
    }
}
