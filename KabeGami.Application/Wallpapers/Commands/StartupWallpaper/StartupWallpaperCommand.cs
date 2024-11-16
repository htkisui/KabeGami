using ErrorOr;
using MediatR;

namespace KabeGami.Application.Wallpapers.Commands.StartupWallpaper;
public sealed record StartupWallpaperCommand
    : IRequest<ErrorOr<Unit>>;
