using KabeGami.Application.Images.Commands.CreateImages;
using KabeGami.Application.Images.Commands.DeleteImages;
using KabeGami.Application.Images.Queries.GetImagePathsFromDirectory;
using KabeGami.Application.Images.Queries.GetImages;
using KabeGami.Desktop.Common.Events.Images;
using KabeGami.Desktop.Common.Interfaces.Services;
using KabeGami.Desktop.Common.Interfaces.Stores;
using KabeGami.Desktop.ViewModels.Common.Models;
using MapsterMapper;
using MediatR;
using System.Collections.Concurrent;
using System.Windows.Media.Imaging;

namespace KabeGami.Desktop.Common.Stores;

public sealed class ImageStore(
    IDispatcherService dispatcherService,
    IErrorHandlingService errorHandlingService,
    IEventAggregator eventAggregator,
    IMapper mapper,
    ISender sender)
        : IImageStore
{
    private readonly IDispatcherService _dispatcherService = dispatcherService;
    private readonly IErrorHandlingService _errorHandlingService = errorHandlingService;
    private readonly IEventAggregator _eventAggregator = eventAggregator;
    private readonly IMapper _mapper = mapper;
    private readonly ISender _sender = sender;

    public List<ImageDisplayModel> Images { get; private set; } = [];
    public Guid ImageServiceGuid { get; } = Guid.NewGuid();
    public Dictionary<Guid, CancellationTokenSource> ImageViewerDictionary { get; private set; } = [];

    private readonly SemaphoreSlim _semaphore = new(4);

    private const int imageUpload = 5;

    private async Task BitmapImagesBatchProcess(List<ImageDisplayModel> images, Guid guid, CancellationToken cancellationToken)
    {
        var imagePathsConcurrentBag = new ConcurrentBag<string>(images.Select(i => i.ImagePath).ToList());
        var tasks = new List<Task<BitmapImage>>();

        foreach (var image in images)
        {
            tasks.Add(Task.Run(async () =>
            {
                await _semaphore.WaitAsync();
                cancellationToken.ThrowIfCancellationRequested();
                try
                {
                    if (image.BitmapImage != null)
                    {
                        return image.BitmapImage;
                    }

                    var bitmapImage = CreateBitmapImage(image.ImagePath, 320, 160);
                    image.BitmapImage = bitmapImage;
                    return bitmapImage;
                }
                finally
                {
                    _semaphore.Release();
                }
            }, cancellationToken));
        }

        int generatedBitmapImageCount = 0;
        while (tasks.Count > 0)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var finishedTask = await Task.WhenAny(tasks);
            tasks.Remove(finishedTask);
            generatedBitmapImageCount++;
            if (finishedTask.IsCompletedSuccessfully)
            {
                _dispatcherService.Invoke(() =>
                {
                    if (tasks.Count % imageUpload == 0 && guid != Guid.Empty)
                    {
                        _eventAggregator.GetEvent<ImageViewerUpdatedEvent>().Publish(new(guid, images, generatedBitmapImageCount, images.Count));
                    }
                });
            }
            else if (finishedTask.IsFaulted)
            {
                //TODO ERRORS
            }
        }
    }

    private static BitmapImage CreateBitmapImage(string imagePath, int width, int height)
    {
        var bitmapImage = new BitmapImage();
        bitmapImage.BeginInit();
        bitmapImage.UriSource = new Uri(imagePath);
        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        bitmapImage.DecodePixelWidth = width;
        bitmapImage.DecodePixelHeight = height;
        bitmapImage.EndInit();
        bitmapImage.Freeze();
        return bitmapImage;
    }

    public async Task GenerateBitmapImagesAsync(List<ImageDisplayModel> images, Guid guid)
    {
        if (ImageViewerDictionary.TryGetValue(guid, out var oldCancellationTokenSource))
        {
            oldCancellationTokenSource.Cancel();
            oldCancellationTokenSource.Dispose();
        }
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;
        ImageViewerDictionary[guid] = cancellationTokenSource;

        try
        {
            await BitmapImagesBatchProcess(images, guid, cancellationToken);
        }
        finally
        {
            cancellationTokenSource.Dispose();
            ImageViewerDictionary.Remove(guid);
        }
    }

    public async Task InitializeAsync()
    {
        var query = new GetImagesQuery();
        var res = await _sender.Send(query);
        if (res.IsError)
        {
            _errorHandlingService.HandlerErrors(res.Errors);
            return;
        }
        Images = res.Value.Select(_mapper.Map<ImageDisplayModel>).ToList();
        await GenerateBitmapImagesAsync(Images, Guid.Empty);
    }

    public async Task<List<string>> GetImagePathsFromDirectoryPathAsync(string directoryPath)
    {
        var query = new GetImagePathsFromDirectoryQuery(new(directoryPath));
        var res = await _sender.Send(query);
        if (res.IsError)
        {
            _errorHandlingService.HandlerErrors(res.Errors);
            return [];
        }
        return res.Value;
    }

    public async Task CreateImagesAsync(List<ImageDisplayModel> images)
    {
        var command = new CreateImagesCommand(new(images.Select(i => i.ImagePath).ToList()));
        var res = await _sender.Send(command);

        Images.AddRange(res.Images.Select(_mapper.Map<ImageDisplayModel>).ToList());
        await GenerateBitmapImagesAsync(Images, Guid.Empty);

        _eventAggregator.GetEvent<ImageViewerUpdatedEvent>().Publish(new(ImageServiceGuid, Images, Images.Count, Images.Count));

        //TODO ERRORS
    }

    public async Task DeleteImagesAsync(List<ImageDisplayModel> images)
    {
        var command = new DeleteImagesCommand(new(images.Select(i => i.ImageGuid).ToList()));
        var res = await _sender.Send(command);
        if (res.IsError)
        {
            _errorHandlingService.HandlerErrors(res.Errors);
            return;
        }
        Images = Images.Except(images).ToList();

        _eventAggregator.GetEvent<ImageViewerUpdatedEvent>().Publish(new(ImageServiceGuid, Images, Images.Count, Images.Count));
    }
}
