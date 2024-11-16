using ErrorOr;
using MediatR;

namespace KabeGami.Application.Wallpapers.Commands.SetNextWallpaper;
public sealed record SetNextWallpaperCommand
    : IRequest<ErrorOr<Unit>>;
