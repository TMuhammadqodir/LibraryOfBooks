using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryOfBooks.Dataccess.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BookCategories",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, new DateTime(2024, 6, 29, 8, 53, 38, 322, DateTimeKind.Utc).AddTicks(2651), false, "Adabiyot", null },
                    { 2L, new DateTime(2024, 6, 29, 8, 53, 38, 322, DateTimeKind.Utc).AddTicks(2653), false, "Ilmiy-fantastika", null },
                    { 3L, new DateTime(2024, 6, 29, 8, 53, 38, 322, DateTimeKind.Utc).AddTicks(2655), false, "Fantaziya", null },
                    { 4L, new DateTime(2024, 6, 29, 8, 53, 38, 322, DateTimeKind.Utc).AddTicks(2656), false, "Detektiv va Triller", null },
                    { 5L, new DateTime(2024, 6, 29, 8, 53, 38, 322, DateTimeKind.Utc).AddTicks(2657), false, "Romantika", null },
                    { 6L, new DateTime(2024, 6, 29, 8, 53, 38, 322, DateTimeKind.Utc).AddTicks(2658), false, "Ilmiy", null },
                    { 7L, new DateTime(2024, 6, 29, 8, 53, 38, 322, DateTimeKind.Utc).AddTicks(2660), false, "Biznes va Iqtisodiyot", null },
                    { 8L, new DateTime(2024, 6, 29, 8, 53, 38, 322, DateTimeKind.Utc).AddTicks(2661), false, "O'z-o'zini rivojlantirish", null },
                    { 9L, new DateTime(2024, 6, 29, 8, 53, 38, 322, DateTimeKind.Utc).AddTicks(2662), false, "Tarix", null },
                    { 10L, new DateTime(2024, 6, 29, 8, 53, 38, 322, DateTimeKind.Utc).AddTicks(2663), false, "Bolalar adabiyoti", null },
                    { 11L, new DateTime(2024, 6, 29, 8, 53, 38, 322, DateTimeKind.Utc).AddTicks(2664), false, "San'at va Madaniyat", null },
                    { 12L, new DateTime(2024, 6, 29, 8, 53, 38, 322, DateTimeKind.Utc).AddTicks(2665), false, "Bolalar adabiyoti", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BookCategories",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "BookCategories",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "BookCategories",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "BookCategories",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "BookCategories",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "BookCategories",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "BookCategories",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "BookCategories",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "BookCategories",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "BookCategories",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "BookCategories",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "BookCategories",
                keyColumn: "Id",
                keyValue: 12L);
        }
    }
}
