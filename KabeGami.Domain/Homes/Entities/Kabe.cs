using ErrorOr;
using KabeGami.Domain.Common.Errors;
using KabeGami.Domain.Common.Primitives;
using KabeGami.Domain.Galleries.ValueObjects;
using KabeGami.Domain.Homes.ValueObjects;
using System.Text.RegularExpressions;

namespace KabeGami.Domain.Homes.Entities;
public sealed partial class Kabe : Entity<KabeId>
{
    public string Name { get; private set; }
    public string Combination { get; private set; }
    public string CronSchedule { get; private set; }
    public GalleryId GalleryId { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private const string cronSchedulePattern = @"^\*(\/[0-5][0-9])? \*(\/[0-5][0-9])? \* \* \* \? \*$";
    private const string combinationPattern = @"^Control\+NumPad[1-9]{1}$";

    private Kabe(
        KabeId kabeId,
        string name,
        string combination,
        string cronSchedule,
        GalleryId galleryId,
        DateTime createdDateTime,
        DateTime updatedDateTime) 
        : base(kabeId)
    {
        Name = name;
        Combination = combination;
        CronSchedule = cronSchedule;
        GalleryId = galleryId;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

#pragma warning disable CS8618
    private Kabe() { }
#pragma warning restore CS8618

    public static ErrorOr<Kabe> Create(
        string name,
        string combination,
        string cronSchedule,
        GalleryId galleryId)
    {
        if (IsCombinationValid(combination) is false)
        {
            return Errors.Home.Kabe.KabeCombinationNotValid(combination);
        }

        if (IsCronScheduleValid(cronSchedule) is false)
        {
            return Errors.Home.Kabe.KabeCronScheduleNotValid(cronSchedule);
        }

        return new Kabe(
            KabeId.CreateUnique(),
            name,
            combination,
            cronSchedule,
            galleryId,
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
}
