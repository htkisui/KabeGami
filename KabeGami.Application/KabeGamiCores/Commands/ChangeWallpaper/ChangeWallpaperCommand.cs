using ErrorOr;
using MediatR;

namespace KabeGami.Application.KabeGamiCores.Commands.SetWallpaper;
public sealed record ChangeWallpaperCommand(
    Guid SubGalleryGuid) : IRequest<ErrorOr<Unit>>;
