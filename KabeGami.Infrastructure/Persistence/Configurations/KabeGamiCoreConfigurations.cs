using KabeGami.Domain.Galleries.ValueObjects;
using KabeGami.Domain.KabeGamis;
using KabeGami.Domain.KabeGamis.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KabeGami.Infrastructure.Persistence.Configurations;
internal class KabeGamiCoreConfigurations
    : IEntityTypeConfiguration<KabeGamiCore>
{
    public void Configure(EntityTypeBuilder<KabeGamiCore> builder)
    {
        ConfigureKabeGamiCoreTable(builder);
    }



    private static void ConfigureKabeGamiCoreTable(EntityTypeBuilder<KabeGamiCore> builder)
    {
        builder.ToTable("KabeGamiCore");

        builder.HasKey(kgc => kgc.Id);

        builder
            .Property(kgc => kgc.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => KabeGamiCoreId.Create(value));

        builder
            .Property(kgc => kgc.GalleryId)
            .ValueGeneratedNever()
            .HasDefaultValue(GalleryId.CreateEmpty())
            .HasConversion(
                id => id.Value,
                value => GalleryId.Create(value));
    }
}
