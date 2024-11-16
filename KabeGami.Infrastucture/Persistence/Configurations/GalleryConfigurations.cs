using KabeGami.Domain.Galleries;
using KabeGami.Domain.Galleries.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KabeGami.Infrastucture.Persistence.Configurations;
internal sealed class GalleryConfigurations
    : IEntityTypeConfiguration<Gallery>
{
    public void Configure(EntityTypeBuilder<Gallery> builder)
    {
        ConfigureGalleriesTable(builder);
    }

    private static void ConfigureGalleriesTable(EntityTypeBuilder<Gallery> builder)
    {
        builder.ToTable("Galleries");

        builder.HasKey(g => g.Id);

        builder.Property(g => g.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => GalleryId.Create(value));

        builder.Property(g => g.Name)
            .HasMaxLength(255);

        builder.OwnsMany(g => g.ImageIds, gib =>
        {
            gib.ToTable("Galleries_Images");

            gib.WithOwner().HasForeignKey("GalleryId");

            gib.Property(i => i.Value)
                .HasColumnName("ImageId")
                .ValueGeneratedNever();
        });

        builder.Navigation(g => g.ImageIds).Metadata.SetField("_imageIds");
        builder.Navigation(g => g.ImageIds).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
