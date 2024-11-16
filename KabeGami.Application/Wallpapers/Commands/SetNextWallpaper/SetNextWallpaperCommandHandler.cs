using ErrorOr;
using KabeGami.Application.Common.Interfaces.Services;
using MediatR;

namespace KabeGami.Application.Wallpapers.Commands.SetNextWallpaper;
internal sealed class SetNextWallpaperCommandHandler(
    IWallpaperSystemService wallpaperSystemService)
        : IRequestHandler<SetNextWallpaperCommand, ErrorOr<Unit>>
{
    private readonly IWallpaperSystemService _wallpaperSystemService = wallpaperSystemService;

    public async Task<ErrorOr<Unit>> Handle(SetNextWallpaperCommand request, CancellationToken cancellationToken)
    {
        await _wallpaperSystemService.SetWallpaperToDesktop(cancellationToken);

        return Unit.Value;
    }
}
