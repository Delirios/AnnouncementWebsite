using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnnouncementWebsite.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageId);
                });

            migrationBuilder.CreateTable(
                name: "Announcements",
                columns: table => new
                {
                    AnnouncementId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.AnnouncementId);
                    table.ForeignKey(
                        name: "FK_Announcements_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnnouncementImages",
                columns: table => new
                {
                    AnnouncementId = table.Column<int>(nullable: false),
                    ImageId = table.Column<int>(nullable: false),
                    AnnouncementImageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementImages", x => new { x.AnnouncementId, x.ImageId });
                    table.ForeignKey(
                        name: "FK_AnnouncementImages_Announcements_AnnouncementId",
                        column: x => x.AnnouncementId,
                        principalTable: "Announcements",
                        principalColumn: "AnnouncementId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnnouncementImages_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "ImageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Vehicle" },
                    { 2, "Others" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "Name" },
                values: new object[,]
                {
                    { 1, "1.jpg" },
                    { 2, "2.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Announcements",
                columns: new[] { "AnnouncementId", "CategoryId", "DateAdded", "Description", "Title" },
                values: new object[] { 1, 1, new DateTime(2020, 10, 10, 18, 43, 31, 893, DateTimeKind.Local).AddTicks(9543), "Some First Description", "First" });

            migrationBuilder.InsertData(
                table: "Announcements",
                columns: new[] { "AnnouncementId", "CategoryId", "DateAdded", "Description", "Title" },
                values: new object[] { 2, 1, new DateTime(2020, 10, 10, 18, 43, 31, 901, DateTimeKind.Local).AddTicks(5197), "Some Second Description", "Second" });

            migrationBuilder.InsertData(
                table: "Announcements",
                columns: new[] { "AnnouncementId", "CategoryId", "DateAdded", "Description", "Title" },
                values: new object[] { 3, 2, new DateTime(2020, 10, 10, 18, 43, 31, 901, DateTimeKind.Local).AddTicks(5510), "Some Third Description", "Third" });

            migrationBuilder.InsertData(
                table: "AnnouncementImages",
                columns: new[] { "AnnouncementId", "ImageId", "AnnouncementImageId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "AnnouncementImages",
                columns: new[] { "AnnouncementId", "ImageId", "AnnouncementImageId" },
                values: new object[] { 2, 2, 2 });

            migrationBuilder.InsertData(
                table: "AnnouncementImages",
                columns: new[] { "AnnouncementId", "ImageId", "AnnouncementImageId" },
                values: new object[] { 3, 1, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementImages_ImageId",
                table: "AnnouncementImages",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_CategoryId",
                table: "Announcements",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnouncementImages");

            migrationBuilder.DropTable(
                name: "Announcements");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
