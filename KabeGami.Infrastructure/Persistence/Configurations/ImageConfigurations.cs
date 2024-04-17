using KabeGami.Domain.Images;
using KabeGami.Domain.Images.Enumerations;
using KabeGami.Domain.Images.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KabeGami.Infrastructure.Persistence.Configurations;
internal sealed class ImageConfigurations
    : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        ConfigureImagesTable(builder);
        //ConfigureImageAlterIdsTable(builder);
    }

    //private static void ConfigureImageAlterIdsTable(EntityTypeBuilder<Image> builder)
    //{
    //    builder.OwnsMany(i => i.ImageAlterIds, iab =>
    //    {
    //        iab.ToTable("ImageAlterIds");
    //        iab.WithOwner().HasForeignKey("ImageId");
    //        iab.HasKey("Id");
    //        iab.Property(i => i.Value)
    //            .HasColumnName("ImageId")
    //            .ValueGeneratedNever();
    //    });

    //    builder.Metadata.FindNavigation(nameof(Image.ImageAlterIds))!
    //        .SetPropertyAccessMode(PropertyAccessMode.Field);
    //}

    private static void ConfigureImagesTable(EntityTypeBuilder<Image> builder)
    {
        builder.ToTable("Images");

        builder.HasKey(i => i.Id);

        builder
            .Property(i => i.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => ImageId.Create(value));

        builder
            .Property(i => i.ImageExtension)
            .HasConversion(
                imageExtension => imageExtension.Name,
                name => ImageExtension.FromName(name)!);

        builder
            .Property(i => i.ImageCategory)
            .HasConversion(
                imageCategory => imageCategory.Name,
                name => ImageCategory.FromName(name)!);

        builder.Property(i => i.IsSFW);
    }
}
