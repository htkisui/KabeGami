using ErrorOr;
using MediatR;

namespace KabeGami.Application.Galleries.Commands.CreateSubGallery;
public sealed record CreateSubGalleryCommand(
    Guid GalleryGuid,
    string Name,
    string ShortcutKey,
    string CronSchedule) : IRequest<ErrorOr<Unit>>;
