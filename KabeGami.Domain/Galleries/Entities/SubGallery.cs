using ErrorOr;
using KabeGami.Domain.Common.Errors;
using KabeGami.Domain.Common.Primitives;
using KabeGami.Domain.Galleries.ValueObjects;
using KabeGami.Domain.Images.ValueObjects;
using System.Text.RegularExpressions;

namespace KabeGami.Domain.Galleries.Entities;
public sealed partial class SubGallery : Entity<SubGalleryId>
{
    public string Name { get; private set; }
    public string Combination { get; private set; }
    public string CronSchedule { get; private set; }
    public IReadOnlyList<ImageId> ImageIds => _imageIds.AsReadOnly();
    private readonly List<ImageId> _imageIds = [];
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private const string cronSchedulePattern = @"^\*(\/[0-5][0-9])? \*(\/[0-5][0-9])? \* \* \* \? \*$";
    private const string combinationPattern = @"^Control\+NumPad[1-9]{1}$";

    private SubGallery(
        SubGalleryId subGalleryId,
        string name,
        string combination,
        string cronSchedule,
        DateTime createdDateTime,
        DateTime updatedDateTime)
        : base(subGalleryId)
    {
        Name = name;
        Combination = combination;
        CronSchedule = cronSchedule;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

#pragma warning disable CS8618
    private SubGallery() { }
#pragma warning restore CS8618

    public static ErrorOr<SubGallery> Create(
        string name,
        string combination,
        string cronSchedule)
    {
        if (IsCombinationValid(combination) is false)
        {
            return Errors.Gallery.SubGallery.ShortcutKeyNotValid(combination);
        }

        if (IsCronScheduleValid(cronSchedule) is false)
        {
            return Errors.Gallery.SubGallery.CronScheduleNotValid(cronSchedule);
        }

        return new SubGallery(
            SubGalleryId.CreateUnique(),
            name,
            combination,
            cronSchedule,
            DateTime.Now,
            DateTime.Now);
    }

    [GeneratedRegex(cronSchedulePattern)]
    private static partial Regex CronScheduleRegex();
    private static bool IsCronScheduleValid(string cronSchedule)
    {
        var regex = CronScheduleRegex();

        return regex.IsMatch(cronSchedule);
    }

    [GeneratedRegex(combinationPattern)]
    private static partial Regex CombinationRegex();
    private static bool IsCombinationValid(string shortcutKey)
    {
        var regex = CombinationRegex();

        return regex.IsMatch(shortcutKey);
    }

    public void SetImageIds(List<ImageId> imageIds)
    {
        _imageIds.Clear();
        _imageIds.AddRange(imageIds);
    }
}
