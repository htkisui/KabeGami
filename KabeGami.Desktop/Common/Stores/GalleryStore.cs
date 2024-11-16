using KabeGami.Application.Galleries.Commands.CreateGallery;
using KabeGami.Application.Galleries.Commands.DeleteGallery;
using KabeGami.Application.Galleries.Commands.UpdateGalleryImages;
using KabeGami.Application.Galleries.Queries.GetGalleries;
using KabeGami.Desktop.Common.Events.Galleries;
using KabeGami.Desktop.Common.Interfaces.Services;
using KabeGami.Desktop.Common.Interfaces.Stores;
using KabeGami.Desktop.ViewModels.Common.Models;
using MapsterMapper;
using MediatR;

namespace KabeGami.Desktop.Common.Stores;

public sealed class GalleryStore(
    IErrorHandlingService errorHandlingService,
    IEventAggregator eventAggregator,
    IMapper mapper,
    ISender sender)
        : IGalleryStore
{
    private readonly IErrorHandlingService _errorHandlingService = errorHandlingService;
    private readonly IEventAggregator _eventAggregator = eventAggregator;
    private readonly IMapper _mapper = mapper;
    private readonly ISender _sender = sender;

    public List<GalleryDisplayModel> Galleries { get; private set; } = [];

    public async Task CreateGalleryAsync(string name)
    {
        var command = new CreateGalleryCommand(name);
        var res = await _sender.Send(command);
        if (res.IsError)
        {
            _errorHandlingService.HandlerErrors(res.Errors);
            return;
        }

        Galleries.Add(_mapper.Map<GalleryDisplayModel>(res.Value));

        _eventAggregator.GetEvent<GalleryCreatedEvent>().Publish(Galleries);
    }

    public async Task DeleteGalleryAsync(Guid galleryGuid)
    {
        var command = new DeleteGalleryCommand(galleryGuid);
        var res = await _sender.Send(command);
        if (res.IsError)
        {
            _errorHandlingService.HandlerErrors(res.Errors);
            return;
        }

        var gallery = Galleries.SingleOrDefault(g => g.GalleryGuid == galleryGuid);
        if (gallery != null)
        {
            Galleries.Remove(gallery);
            _eventAggregator.GetEvent<GalleryDeletedEvent>().Publish(Galleries);
        }
    }

    public async Task InitializeAsync()
    {
        var query = new GetGalleriesQuery();
        var res = await _sender.Send(query);
        if (res.IsError)
        {
            _errorHandlingService.HandlerErrors(res.Errors);
            return;
        }
        Galleries = res.Value.Select(_mapper.Map<GalleryDisplayModel>).ToList();
    }

    public async Task UpdateGalleryImagesAsync(Guid galleryGuid, List<ImageDisplayModel> images)
    {
        var command = new UpdateGalleryImagesCommand(new(galleryGuid, images.Select(i => i.ImageGuid).ToList()));
        var res = await _sender.Send(command);
        if (res.IsError)
        {
            _errorHandlingService.HandlerErrors(res.Errors);
            return;
        }
        var galleryDisplayModel = _mapper.Map<GalleryDisplayModel>(res.Value);
        Galleries = Galleries.Select(g => g.GalleryGuid == galleryDisplayModel.GalleryGuid ? galleryDisplayModel : g).ToList();

        _eventAggregator.GetEvent<GalleryImagesUpdatedEvent>().Publish(galleryDisplayModel);
    }
}
