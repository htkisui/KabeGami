using ErrorOr;
using MediatR;

namespace KabeGami.Application.KabeGamiCores.Commands.ResetGalleryIdToKabeGamiCore;
public sealed record ResetGalleryIdToKabeGamiCoreCommand(
    ) : IRequest<ErrorOr<Unit>>;
