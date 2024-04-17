using ErrorOr;
using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Domain.Galleries;
using MediatR;

namespace KabeGami.Application.Galleries.Commands.CreateGallery;
internal sealed class CreateGalleryCommandHandler(
    IGalleryRepository galleryRepository, IUnitOfWork unitOfWork)
        : IRequestHandler<CreateGalleryCommand, ErrorOr<Unit>>
{
    private readonly IGalleryRepository _galleryRepository = galleryRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<Unit>> Handle(CreateGalleryCommand request, CancellationToken cancellationToken)
    {
        var isGalleryNameAvailable = await _galleryRepository.IsGalleryNameAvailableAsync(request.Name, cancellationToken);
        if (isGalleryNameAvailable.IsError)
        {
            return isGalleryNameAvailable.Errors;
        }

        var gallery = Gallery.Create(request.Name);
        if (gallery.IsError)
        {
            return gallery.Errors;
        }

        await _galleryRepository.AddGalleryAsync(gallery.Value, cancellationToken);
        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return Unit.Value;
    }
}
