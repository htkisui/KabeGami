using ErrorOr;
using KabeGami.Domain.Common.Errors;
using KabeGami.Domain.Common.Primitives;
using KabeGami.Domain.Galleries.ValueObjects;
using KabeGami.Domain.Homes.Entities;
using KabeGami.Domain.Homes.ValueObjects;
using MediatR;

namespace KabeGami.Domain.Homes;
public sealed class Home : AggregateRoot<HomeId>
{
    public Kabe? DefaultKabe { get; private set; } = null!;
    public IReadOnlyList<Kabe> Kabes => _kabes.AsReadOnly();
    private readonly List<Kabe> _kabes = [];
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private Home(
        HomeId homeId,
        DateTime createdDateTime,
        DateTime updatedDateTime)
        : base(homeId)
    {
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    private Home() { }

    public static ErrorOr<Home> Create()
    {
        return new Home(
            HomeId.CreateUnique(),
            DateTime.Now,
            DateTime.Now);
    }

    public ErrorOr<Kabe> CreateKabe(
        string name,
        string combination,
        string cronSchedule,
        GalleryId galleryId)
    {
        if (_kabes.Any(k => k.Name == name))
        {
            return Errors.Home.Kabe.KabeNameDuplicated(name);
        }

        if (_kabes.Any(k => k.Combination == combination))
        {
            return Errors.Home.Kabe.KabeCombinationDuplicated(combination);
        }

        var kabe = Kabe.Create(
            name,
            combination,
            cronSchedule,
            galleryId);
        if (kabe.IsError)
        {
            return kabe.Errors;
        }

        _kabes.Add(kabe.Value);

        if (_kabes.Count == 1)
        {
            DefaultKabe = kabe.Value;
        }

        return kabe;
    }

    public ErrorOr<Unit> DeleteKabe(
        KabeId kabeId)
    {
        var kabe = _kabes.SingleOrDefault(k => k.Id == kabeId);
        if (kabe is null)
        {
            return Errors.Home.Kabe.KabeNotFound(kabeId);
        }
        if (DefaultKabe == kabe)
        {
            return Errors.Home.DefaultKabeCannotBeDeleted(kabeId);
        }

        _kabes.Remove(kabe);

        return Unit.Value;
    }

    public ErrorOr<Unit> IsGalleryAlreadyAssigned(GalleryId galleryId)
    {
        var galleryAlreadyUsed = _kabes.SingleOrDefault(k => k.GalleryId == galleryId);
        if (galleryAlreadyUsed != null)
        {
            return Errors.Home.Kabe.GalleryAlreadyAssigned(galleryId);
        }
        return Unit.Value;
    }

    public ErrorOr<Unit> SetDefaultKabe(
        KabeId kabeId)
    {
        var kabe = _kabes.SingleOrDefault(k => k.Id == kabeId);
        if (kabe is null)
        {
            return Errors.Home.Kabe.KabeNotFound(kabeId);
        }

        DefaultKabe = kabe;

        return Unit.Value;
    }
}
