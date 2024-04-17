using ErrorOr;
using MediatR;

namespace KabeGami.Application.KabeGamiCores.Commands.SetWallpaperToDesktop;
public sealed record SetWallpaperToDesktopCommand(
    ) : IRequest<ErrorOr<Unit>>;
