﻿// <auto-generated />
using System;
using KabeGami.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KabeGami.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("KabeGami.Domain.Common.Primitives.DomainEvent", b =>
                {
                    b.Property<Guid>("Guid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("GalleryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ImageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("KabeGamiCoreId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Guid");

                    b.HasIndex("GalleryId");

                    b.HasIndex("ImageId");

                    b.HasIndex("KabeGamiCoreId");

                    b.ToTable("DomainEvents", (string)null);
                });

            modelBuilder.Entity("KabeGami.Domain.Galleries.Gallery", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("SubGalleryId")
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValue(new Guid("00000000-0000-0000-0000-000000000000"));

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Galleries", (string)null);
                });

            modelBuilder.Entity("KabeGami.Domain.Images.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageCategory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageExtension")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsSFW")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Images", (string)null);
                });

            modelBuilder.Entity("KabeGami.Domain.KabeGamis.KabeGamiCore", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("GalleryId")
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValue(new Guid("00000000-0000-0000-0000-000000000000"));

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("KabeGamiCore", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("26cb5bec-088d-4068-8518-b7b77a818616"),
                            CreatedDateTime = new DateTime(2024, 4, 13, 18, 44, 41, 288, DateTimeKind.Local).AddTicks(7519),
                            GalleryId = new Guid("00000000-0000-0000-0000-000000000000"),
                            UpdatedDateTime = new DateTime(2024, 4, 13, 18, 44, 41, 288, DateTimeKind.Local).AddTicks(7560)
                        });
                });

            modelBuilder.Entity("KabeGami.Domain.Common.Primitives.DomainEvent", b =>
                {
                    b.HasOne("KabeGami.Domain.Galleries.Gallery", null)
                        .WithMany("DomainEvents")
                        .HasForeignKey("GalleryId");

                    b.HasOne("KabeGami.Domain.Images.Image", null)
                        .WithMany("DomainEvents")
                        .HasForeignKey("ImageId");

                    b.HasOne("KabeGami.Domain.KabeGamis.KabeGamiCore", null)
                        .WithMany("DomainEvents")
                        .HasForeignKey("KabeGamiCoreId");
                });

            modelBuilder.Entity("KabeGami.Domain.Galleries.Gallery", b =>
                {
                    b.OwnsMany("KabeGami.Domain.Galleries.Entities.SubGallery", "SubGalleries", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Combination")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<DateTime>("CreatedDateTime")
                                .HasColumnType("datetime2");

                            b1.Property<string>("CronSchedule")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<Guid>("GalleryId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("nvarchar(255)");

                            b1.Property<DateTime>("UpdatedDateTime")
                                .HasColumnType("datetime2");

                            b1.HasKey("Id");

                            b1.HasIndex("GalleryId");

                            b1.ToTable("SubGalleries", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("GalleryId");

                            b1.OwnsMany("KabeGami.Domain.Images.ValueObjects.ImageId", "ImageIds", b2 =>
                                {
                                    b2.Property<Guid>("SubGalleryId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int");

                                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b2.Property<int>("Id"));

                                    b2.Property<Guid>("Value")
                                        .HasColumnType("uniqueidentifier")
                                        .HasColumnName("ImageId");

                                    b2.HasKey("SubGalleryId", "Id");

                                    b2.ToTable("SubGalleries_Images", (string)null);

                                    b2.WithOwner()
                                        .HasForeignKey("SubGalleryId");
                                });

                            b1.Navigation("ImageIds");
                        });

                    b.Navigation("SubGalleries");
                });

            modelBuilder.Entity("KabeGami.Domain.Galleries.Gallery", b =>
                {
                    b.Navigation("DomainEvents");
                });

            modelBuilder.Entity("KabeGami.Domain.Images.Image", b =>
                {
                    b.Navigation("DomainEvents");
                });

            modelBuilder.Entity("KabeGami.Domain.KabeGamis.KabeGamiCore", b =>
                {
                    b.Navigation("DomainEvents");
                });
#pragma warning restore 612, 618
        }
    }
}
