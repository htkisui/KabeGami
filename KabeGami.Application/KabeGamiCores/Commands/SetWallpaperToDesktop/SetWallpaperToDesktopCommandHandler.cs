using ErrorOr;
using KabeGami.Application.Common.Interfaces.Services;
using MediatR;

namespace KabeGami.Application.KabeGamiCores.Commands.SetWallpaperToDesktop;
internal sealed class SetWallpaperToDesktopCommandHandler(
    IWallpaperSystemService wallpaperSystemService)
        : IRequestHandler<SetWallpaperToDesktopCommand, ErrorOr<Unit>>
{
    private readonly IWallpaperSystemService _wallpaperSystemService = wallpaperSystemService;

    public async Task<ErrorOr<Unit>> Handle(SetWallpaperToDesktopCommand request, CancellationToken cancellationToken)
    {
        await _wallpaperSystemService.SetWallpaperToDesktop();

        return Unit.Value;
    }
}
