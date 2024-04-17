using KabeGami.Domain.Common.Primitives;
using KabeGami.Domain.Galleries.ValueObjects;
using KabeGami.Domain.KabeGamis.ValueObjects;

namespace KabeGami.Domain.KabeGamis;
public sealed class KabeGamiCore : AggregateRoot<KabeGamiCoreId>
{
    public GalleryId GalleryId { get; private set; } = GalleryId.CreateEmpty();
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private KabeGamiCore(
        KabeGamiCoreId kamiGamiCoreId,
        DateTime createdDateTime,
        DateTime updatedDateTime)
        : base(kamiGamiCoreId)
    {
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    private KabeGamiCore() { }

    public static KabeGamiCore Create()
    {
        return new(
            KabeGamiCoreId.CreateUnique(),
            DateTime.Now,
            DateTime.Now);
    }

    public bool HasGalleryId(GalleryId galleryId)
    {
        return GalleryId == galleryId;
    }

    public void SetGalleryId(GalleryId galleryId)
    {
        GalleryId = galleryId;
    }

    public void ResetGalleryId()
    {
        GalleryId = GalleryId.Create(Guid.Empty);
    }
}
