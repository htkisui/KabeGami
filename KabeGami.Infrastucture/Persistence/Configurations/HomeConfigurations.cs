using KabeGami.Domain.Galleries.ValueObjects;
using KabeGami.Domain.Homes;
using KabeGami.Domain.Homes.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KabeGami.Infrastucture.Persistence.Configurations;
internal sealed class HomeConfigurations
    : IEntityTypeConfiguration<Home>
{
    public void Configure(EntityTypeBuilder<Home> builder)
    {
        ConfigureHomesTable(builder);
        ConfigureKabesTable(builder);
    }

    private static void ConfigureKabesTable(EntityTypeBuilder<Home> builder)
    {
        builder.OwnsMany(h => h.Kabes, kb =>
        {
            kb.ToTable("Kabes");

            kb.WithOwner()
                .HasForeignKey("HomeId");
            
            kb.HasKey(k => k.Id);

            kb.Property(k => k.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => KabeId.Create(value));

            kb.Property(k => k.Name)
                .HasMaxLength(255);

            kb.Property(k => k.Combination)
                .HasMaxLength(255);

            kb.Property(k => k.CronSchedule)
                .HasMaxLength(255);

            kb.Property(k => k.GalleryId)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => GalleryId.Create(value));
        });

        builder.OwnsOne(h => h.DefaultKabe, kb =>
        {
            kb.WithOwner()
                .HasForeignKey("HomeId");

            kb.Property(k => k.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => KabeId.Create(value));

            kb.Property(k => k.Name)
                .HasMaxLength(255);

            kb.Property(k => k.Combination)
                .HasMaxLength(255);

            kb.Property(k => k.CronSchedule)
                .HasMaxLength(255);

            kb.Property(k => k.GalleryId)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => GalleryId.Create(value));
        });

    }

    private static void ConfigureHomesTable(EntityTypeBuilder<Home> builder)
    {
        builder.ToTable("Home");

        builder.HasKey(h => h.Id);

        builder
            .Property(h => h.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => HomeId.Create(value));
    }
}
