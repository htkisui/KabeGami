using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KabeGami.Infrastucture.Migrations
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
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Galleries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Home",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DefaultKabe_Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DefaultKabe_Combination = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DefaultKabe_CronSchedule = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DefaultKabe_GalleryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DefaultKabe_CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DefaultKabe_UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DefaultKabe_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Home", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Galleries_Images",
                columns: table => new
                {
                    GalleryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Galleries_Images", x => new { x.GalleryId, x.Id });
                    table.ForeignKey(
                        name: "FK_Galleries_Images_Galleries_GalleryId",
                        column: x => x.GalleryId,
                        principalTable: "Galleries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kabes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Combination = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CronSchedule = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    GalleryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HomeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kabes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kabes_Home_HomeId",
                        column: x => x.HomeId,
                        principalTable: "Home",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Home",
                columns: new[] { "Id", "CreatedDateTime", "UpdatedDateTime" },
                values: new object[] { new Guid("79e86fa5-b774-42d7-85da-db738f0db52e"), new DateTime(2024, 10, 18, 10, 53, 57, 653, DateTimeKind.Local).AddTicks(5566), new DateTime(2024, 10, 18, 10, 53, 57, 653, DateTimeKind.Local).AddTicks(5608) });

            migrationBuilder.CreateIndex(
                name: "IX_Kabes_HomeId",
                table: "Kabes",
                column: "HomeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Galleries_Images");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Kabes");

            migrationBuilder.DropTable(
                name: "Galleries");

            migrationBuilder.DropTable(
                name: "Home");
        }
    }
}
