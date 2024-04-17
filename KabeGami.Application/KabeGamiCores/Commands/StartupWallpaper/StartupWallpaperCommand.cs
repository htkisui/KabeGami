using ErrorOr;
using MediatR;

namespace KabeGami.Application.KabeGamiCores.Commands.StartupWallpaper;
public sealed record StartupWallpaperCommand(
    ) : IRequest<ErrorOr<Unit>>;
