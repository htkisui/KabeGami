using ErrorOr;
using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Domain.Galleries.ValueObjects;
using MediatR;

namespace KabeGami.Application.Galleries.Commands.CreateSubGallery;
internal sealed class CreateSubGalleryCommandHandler(
    IGalleryRepository galleryRepository, 
    IUnitOfWork unitOfWork)
        : IRequestHandler<CreateSubGalleryCommand, ErrorOr<Unit>>
{
    private readonly IGalleryRepository _galleryRepository = galleryRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<Unit>> Handle(CreateSubGalleryCommand request, CancellationToken cancellationToken)
    {
        var gallery = await _galleryRepository.GetGalleryByIdAsync(GalleryId.Create(request.GalleryGuid), cancellationToken);
        if (gallery.IsError)
        {
            return gallery.Errors;
        }

        var subGalleryCreated = gallery.Value.CreateSubGallery(
            request.Name,
            request.ShortcutKey,
            request.CronSchedule);
        if (subGalleryCreated.IsError)
        {
            return subGalleryCreated.Errors;
        }

        await _unitOfWork.SaveChangeAsync(cancellationToken);
        return Unit.Value;
    }
}
