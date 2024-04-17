using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KabeGami.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Galleries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SubGalleryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("00000000-0000-0000-0000-000000000000")),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Galleries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSFW = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KabeGamiCore",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GalleryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("00000000-0000-0000-0000-000000000000")),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KabeGamiCore", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubGalleries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Combination = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CronSchedule = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GalleryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubGalleries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubGalleries_Galleries_GalleryId",
                        column: x => x.GalleryId,
                        principalTable: "Galleries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DomainEvents",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GalleryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    KabeGamiCoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomainEvents", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_DomainEvents_Galleries_GalleryId",
                        column: x => x.GalleryId,
                        principalTable: "Galleries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DomainEvents_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DomainEvents_KabeGamiCore_KabeGamiCoreId",
                        column: x => x.KabeGamiCoreId,
                        principalTable: "KabeGamiCore",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubGalleries_Images",
                columns: table => new
                {
                    SubGalleryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubGalleries_Images", x => new { x.SubGalleryId, x.Id });
                    table.ForeignKey(
                        name: "FK_SubGalleries_Images_SubGalleries_SubGalleryId",
                        column: x => x.SubGalleryId,
                        principalTable: "SubGalleries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "KabeGamiCore",
                columns: new[] { "Id", "CreatedDateTime", "GalleryId", "UpdatedDateTime" },
                values: new object[] { new Guid("26cb5bec-088d-4068-8518-b7b77a818616"), new DateTime(2024, 4, 13, 18, 44, 41, 288, DateTimeKind.Local).AddTicks(7519), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2024, 4, 13, 18, 44, 41, 288, DateTimeKind.Local).AddTicks(7560) });

            migrationBuilder.CreateIndex(
                name: "IX_DomainEvents_GalleryId",
                table: "DomainEvents",
                column: "GalleryId");

            migrationBuilder.CreateIndex(
                name: "IX_DomainEvents_ImageId",
                table: "DomainEvents",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_DomainEvents_KabeGamiCoreId",
                table: "DomainEvents",
                column: "KabeGamiCoreId");

            migrationBuilder.CreateIndex(
                name: "IX_SubGalleries_GalleryId",
                table: "SubGalleries",
                column: "GalleryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DomainEvents");

            migrationBuilder.DropTable(
                name: "SubGalleries_Images");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "KabeGamiCore");

            migrationBuilder.DropTable(
                name: "SubGalleries");

            migrationBuilder.DropTable(
                name: "Galleries");
        }
    }
}
