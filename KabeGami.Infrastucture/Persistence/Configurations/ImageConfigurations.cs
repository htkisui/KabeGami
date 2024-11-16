using KabeGami.Domain.Images;
using KabeGami.Domain.Images.Enumerations;
using KabeGami.Domain.Images.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KabeGami.Infrastucture.Persistence.Configurations;
internal sealed class ImageConfigurations
    : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        ConfigureImagesTable(builder);
    }

    private static void ConfigureImagesTable(EntityTypeBuilder<Image> builder)
    {
        builder.ToTable("Images");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => ImageId.Create(value));

        builder.Property(i => i.ImageExtension)
            .HasConversion(
                imageExtension => imageExtension.Name,
                name => ImageExtension.FromName(name)!);
    }
}
