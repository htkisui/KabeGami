using KabeGami.Application.Images.Common;
using KabeGami.Domain.Images;
using Mapster;

namespace KabeGami.Application.Common.Mappings;
internal class ImageMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Image, ImageResult>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Extension, src => src.ImageExtension.Extension)
            .Map(dest => dest.CategoryDirectoryPath, src => src.ImageCategory.DirectoryPath);
    }
}
