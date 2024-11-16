using KabeGami.Application.Images.Results;
using KabeGami.Desktop.ViewModels.Common.Models;
using Mapster;
using System.IO;

namespace KabeGami.Desktop.Common.Mappings;
internal class ImageMapperConfig
    : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ImageResult, ImageDisplayModel>()
            .Map(dest => dest.ImageGuid, src => src.ImageGuid)
            .Map(dest => dest.ImagePath, src => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Wallpapers/{src.ImageNameWithExtension}"));
    }
}
