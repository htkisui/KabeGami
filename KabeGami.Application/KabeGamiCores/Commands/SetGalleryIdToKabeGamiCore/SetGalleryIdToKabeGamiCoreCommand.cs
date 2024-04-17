using ErrorOr;
using MediatR;

namespace KabeGami.Application.KabeGamiCores.Commands.SetGalleryIdToKabeGamiCore;
public sealed record SetGalleryIdToKabeGamiCoreCommand(
    Guid GalleryGuid) : IRequest<ErrorOr<Unit>>;
