using KabeGami.Application.Homes.Commands.CreateKabe;
using KabeGami.Application.Homes.Commands.DeleteKabe;
using KabeGami.Application.Homes.Commands.SetDefaultKabe;
using KabeGami.Application.Homes.Queries.GetHome;
using KabeGami.Desktop.Common.Events.Homes;
using KabeGami.Desktop.Common.Interfaces.Services;
using KabeGami.Desktop.Common.Interfaces.Stores;
using KabeGami.Desktop.ViewModels.Common.Models;
using MapsterMapper;
using MediatR;

namespace KabeGami.Desktop.Common.Stores;

public sealed class HomeStore(
    IErrorHandlingService errorHandlingService,
    IEventAggregator eventAggregator,
    IMapper mapper,
    ISender sender)
        : IHomeStore
{
    private readonly IErrorHandlingService _errorHandlingService = errorHandlingService;
    private readonly IEventAggregator _eventAggregator = eventAggregator;
    private readonly IMapper _mapper = mapper;
    private readonly ISender _sender = sender;

    public HomeDisplayModel Home { get; private set; } = null!;

    public async Task CreateKabeAsync(string name, string combination, string cronSchedule, string galleryName)
    {
        var command = new CreateKabeCommand(new(name, combination, cronSchedule, galleryName));
        var res = await _sender.Send(command);
        if (res.IsError)
        {
            _errorHandlingService.HandlerErrors(res.Errors);
            return;
        }

        Home.Kabes.Add(_mapper.Map<KabeDisplayModel>(res.Value));

        _eventAggregator.GetEvent<KabeCreatedEvent>().Publish(Home.Kabes);
        if (Home.Kabes.Count == 1)
        {
            _eventAggregator.GetEvent<DefaultKabeSetEvent>().Publish(res.Value.GalleryGuid);
        }
    }

    public async Task DeleteKabeAsync(Guid kabeGuid)
    {
        var command = new DeleteKabeCommand(new(kabeGuid));
        var res = await _sender.Send(command);
        if (res.IsError)
        {
            _errorHandlingService.HandlerErrors(res.Errors);
            return;
        }

        var kabe = Home.Kabes.SingleOrDefault(k => k.KabeGuid == kabeGuid);
        if (kabe != null)
        {
            Home.Kabes.Remove(kabe);
            _eventAggregator.GetEvent<KabeDeletedEvent>().Publish(Home.Kabes);
        }
    }

    public async Task InitializeAsync()
    {
        var query = new GetHomeQuery();
        var res = await _sender.Send(query);
        if (res.IsError)
        {
            _errorHandlingService.HandlerErrors(res.Errors);
            return;
        }
        Home = _mapper.Map<HomeDisplayModel>(res.Value);
    }

    public async Task SetDefaultKabeAsync(Guid kabeGuid)
    {
        var command = new SetDefaultKabeCommand(new(kabeGuid));
        var res = await _sender.Send(command);
        if (res.IsError)
        {
            _errorHandlingService.HandlerErrors(res.Errors);
            return;
        }
        Home = _mapper.Map<HomeDisplayModel>(res.Value);
        _eventAggregator.GetEvent<DefaultKabeSetEvent>().Publish(Home.DefaultKabe.KabeGuid);
    }
}
