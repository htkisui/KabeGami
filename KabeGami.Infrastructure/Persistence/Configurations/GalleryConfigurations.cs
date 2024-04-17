using KabeGami.Domain.Galleries;
using KabeGami.Domain.Galleries.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KabeGami.Infrastructure.Persistence.Configurations;
internal class GalleryConfigurations
    : IEntityTypeConfiguration<Gallery>
{
    public void Configure(EntityTypeBuilder<Gallery> builder)
    {
        ConfigureGalleriesTable(builder);
        ConfigureSubGalleriesTable(builder);
    }

    private static void ConfigureSubGalleriesTable(EntityTypeBuilder<Gallery> builder)
    {
        builder.OwnsMany(g => g.SubGalleries, sgb =>
        {
            sgb.ToTable("SubGalleries");

            sgb.WithOwner()
                .HasForeignKey("GalleryId");

            sgb.HasKey(sg => sg.Id);

            sgb.Property(sg => sg.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => SubGalleryId.Create(value));

            sgb.Property(sg => sg.Name)
                .HasMaxLength(255);

            sgb.Property(sg => sg.Combination)
                .HasMaxLength(50);

            sgb.Property(sg => sg.CronSchedule)
                .HasMaxLength(50);

            sgb.OwnsMany(sg => sg.ImageIds, ib =>
            {
                ib.ToTable("SubGalleries_Images");

                ib.WithOwner().HasForeignKey("SubGalleryId");

                ib.Property(i => i.Value)
                    .HasColumnName("ImageId")
                    .ValueGeneratedNever();
            });

            sgb.Navigation(sg => sg.ImageIds).Metadata.SetField("_imageIds");
            sgb.Navigation(sg => sg.ImageIds).UsePropertyAccessMode(PropertyAccessMode.Field);
        });
    }

    private static void ConfigureGalleriesTable(EntityTypeBuilder<Gallery> builder)
    {
        builder.ToTable("Galleries");

        builder.HasKey(g => g.Id);

        builder
            .Property(g => g.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => GalleryId.Create(value));

        builder
            .Property(g => g.SubGalleryId)
            .ValueGeneratedNever()
            .HasDefaultValue(SubGalleryId.CreateEmpty())
            .HasConversion(
                id => id.Value,
                value => SubGalleryId.Create(value));

        builder
            .Property(g => g.Name)
            .HasMaxLength(255);
    }
}
