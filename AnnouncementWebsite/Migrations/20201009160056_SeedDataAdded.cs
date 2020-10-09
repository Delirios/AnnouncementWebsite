using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnnouncementWebsite.Migrations
{
    public partial class SeedDataAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_Categories_CategoryId",
                table: "Announcements");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Announcements",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[] { 1, "Vehicle" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[] { 2, "Others" });

            migrationBuilder.InsertData(
                table: "Announcements",
                columns: new[] { "AnnouncementId", "CategoryId", "DateAdded", "Description", "Title" },
                values: new object[] { 1, 1, new DateTime(2020, 10, 9, 19, 0, 55, 997, DateTimeKind.Local).AddTicks(6563), "Some First Description", "First" });

            migrationBuilder.InsertData(
                table: "Announcements",
                columns: new[] { "AnnouncementId", "CategoryId", "DateAdded", "Description", "Title" },
                values: new object[] { 2, 1, new DateTime(2020, 10, 9, 19, 0, 56, 2, DateTimeKind.Local).AddTicks(358), "Some Second Description", "Second" });

            migrationBuilder.InsertData(
                table: "Announcements",
                columns: new[] { "AnnouncementId", "CategoryId", "DateAdded", "Description", "Title" },
                values: new object[] { 3, 2, new DateTime(2020, 10, 9, 19, 0, 56, 2, DateTimeKind.Local).AddTicks(535), "Some Third Description", "Third" });

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_Categories_CategoryId",
                table: "Announcements",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_Categories_CategoryId",
                table: "Announcements");

            migrationBuilder.DeleteData(
                table: "Announcements",
                keyColumn: "AnnouncementId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Announcements",
                keyColumn: "AnnouncementId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Announcements",
                keyColumn: "AnnouncementId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Announcements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_Categories_CategoryId",
                table: "Announcements",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
