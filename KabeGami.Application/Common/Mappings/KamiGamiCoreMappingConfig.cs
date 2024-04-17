using KabeGami.Application.KabeGamiCores.Common.Results;
using KabeGami.Domain.KabeGamis;
using Mapster;

namespace KabeGami.Application.Common.Mappings;
internal sealed class KamiGamiCoreMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<KabeGamiCore, KabeGamiCoreResult>()
            .Map(dest => dest.GalleryGuidResult, src => src.GalleryId)
            .Map(dest => dest, src => src);
    }
}
