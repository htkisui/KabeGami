using ErrorOr;
using KabeGami.Application.Wallpapers.Requests;
using MediatR;

namespace KabeGami.Application.Wallpapers.Commands.ChangeWallpaper;
public sealed record ChangeWallpaperCommand(
    ChangeWallpaperRequest Payload)
        : IRequest<ErrorOr<Unit>>;
