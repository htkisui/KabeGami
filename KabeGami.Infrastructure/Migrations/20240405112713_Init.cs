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
                name: "DomainEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomainEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DomainEvent_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ImageAlterIds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageAlterIds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageAlterIds_Images_ImageId1",
                        column: x => x.ImageId1,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DomainEvent_ImageId",
                table: "DomainEvent",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageAlterIds_ImageId1",
                table: "ImageAlterIds",
                column: "ImageId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DomainEvent");

            migrationBuilder.DropTable(
                name: "ImageAlterIds");

            migrationBuilder.DropTable(
                name: "Images");
        }
    }
}
