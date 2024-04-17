using ErrorOr;
using KabeGami.Application.Common.Errors;
using KabeGami.Application.Common.Interfaces.Services;
using KabeGami.Application.Images.Common.Results;
using MediatR;
using Quartz;
using System.Runtime.InteropServices;

namespace KabeGami.Infrastructure.Services;
internal sealed partial class WallpaperSystemService
    : IWallpaperSystemService
{
    private readonly IScheduler _scheduler;

    public Guid GalleryGuid { get; private set; }
    public Guid SubGalleryGuid { get; private set; }
    public List<Guid> ImageGuids { get; private set; } = [];
    private static List<string> imagePaths = [];

    private const string defaultCronSchedule = "0 0 0 1 1 ? 2000";
    private const string wallpaperGroup = "WallpapaerGroup";
    private const string wallpaperJob = "WallpaperJob";
    private const string wallpaperTrigger = "WallpaperTrigger";
    private const int SPI_SETDESKWALLPAPER = 0x0014;
    private const int SPIF_UPDATEINIFILE = 0x01;
    private const int SPIF_SENDCHANGE = 0x02;

    public WallpaperSystemService(IScheduler scheduler)
    {
        _scheduler = scheduler;
        SetDefaultJob();
    }

    private static string GenerateRandomImagePath()
    {
        int randomIndex = Random.Shared.Next(imagePaths.Count);
        return imagePaths[randomIndex];
    }

    private void SetDefaultJob()
    {
        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity(wallpaperTrigger, wallpaperGroup)
            .WithCronSchedule(defaultCronSchedule)
            .Build();

        IJobDetail job = JobBuilder.Create()
            .OfType<WallpaperSystemJob>()
            .WithIdentity(wallpaperJob, wallpaperGroup)
            .Build();

        _scheduler.ScheduleJob(job, trigger);
    }

    private void SetImagePaths(List<ImageResult> imageResults)
    {
        imagePaths.Clear();
        ImageGuids.Clear();

        foreach (var imageResult in imageResults)
        {
            var imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{imageResult.CategoryDirectoryPath}{imageResult.ImageGuidResult}{imageResult.Extension}");
            imagePaths.Add(imagePath);
            ImageGuids.Add(imageResult.ImageGuidResult);
        }
    }

    public async Task SetWallpaperToDesktop()
    {
        if (imagePaths.Count == 0)
        {
            return;
        }

        string imagePath = GenerateRandomImagePath();
        await SystemParametersInfo(imagePath);
    }

    public async Task<ErrorOr<Unit>> SetWallpaperSystemJob(
        List<ImageResult> imageResults,
        string cronSchedule,
        CancellationToken cancellationToken)
    {
        if (imageResults.Count == 0)
        {
            return Errors.WallpaperSystemService.ImageResultsEmpty;
        }

        SetImagePaths(imageResults);

        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity(wallpaperTrigger, wallpaperGroup)
            .WithCronSchedule(cronSchedule)
            .Build();

        IJobDetail job = JobBuilder.Create<WallpaperSystemJob>()
            .WithIdentity(wallpaperJob, wallpaperGroup)
            .Build();

        await _scheduler.DeleteJob(new JobKey(wallpaperJob, wallpaperGroup), cancellationToken);

        await _scheduler.ScheduleJob(job, trigger, cancellationToken);

        await _scheduler.Start(cancellationToken);

        await SetWallpaperToDesktop();

        return Unit.Value;
    }

    private partial class WallpaperSystemJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            string imagePath = GenerateRandomImagePath();
            await SystemParametersInfo(imagePath);
        }
    }

    public static async Task SystemParametersInfo(string imagePath)
    {
        await Task.Run(() => SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, imagePath, SPIF_UPDATEINIFILE | SPIF_SENDCHANGE));
    }

    [LibraryImport("user32.dll", EntryPoint = "SystemParametersInfoW", StringMarshalling = StringMarshalling.Utf16)]
    private static partial int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);
}
