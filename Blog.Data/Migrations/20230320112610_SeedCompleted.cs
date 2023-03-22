using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedCompleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "articles",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsDeleted", "ModifiedBy", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("3c5c732f-92e5-4707-b26e-e3482a60540f"), "Admin test", new DateTime(2023, 3, 20, 14, 26, 9, 918, DateTimeKind.Local).AddTicks(962), "admin", null, false, "admin", null, "vs 2022" },
                    { new Guid("d2b8f597-96f8-418b-9819-96c150cad909"), "Admin test", new DateTime(2023, 3, 20, 14, 26, 9, 918, DateTimeKind.Local).AddTicks(959), "admin", null, false, "admin", null, "asp .net core mvc" }
                });

            migrationBuilder.InsertData(
                table: "ımages",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "FileName", "FileType", "IsDeleted", "ModifiedBy", "ModifiedDate" },
                values: new object[,]
                {
                    { new Guid("9c6a7406-5073-45c5-b2de-a56773c220b4"), "Admin Test", new DateTime(2023, 3, 20, 14, 26, 9, 918, DateTimeKind.Local).AddTicks(1078), "admin", null, "images/testimage", "jpg", false, "admin", null },
                    { new Guid("d5a78a10-21a8-49a3-a3b6-e21add292023"), "Admin Test", new DateTime(2023, 3, 20, 14, 26, 9, 918, DateTimeKind.Local).AddTicks(1075), "admin", null, "images/testimage", "jpg", false, "admin", null }
                });

            migrationBuilder.InsertData(
                table: "articles",
                columns: new[] { "Id", "CategoryId", "Content", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "ImageId", "IsDeleted", "ModifiedBy", "ModifiedDate", "Title", "ViewCount" },
                values: new object[,]
                {
                    { new Guid("92980c0e-f47f-4393-8da7-2d6a743edbc0"), new Guid("3c5c732f-92e5-4707-b26e-e3482a60540f"), "vs açıklama", "admin test", new DateTime(2023, 3, 20, 14, 26, 9, 918, DateTimeKind.Local).AddTicks(776), "admin", null, new Guid("9c6a7406-5073-45c5-b2de-a56773c220b4"), false, "admin", null, "vs deneme makalesi 1", 1 },
                    { new Guid("eb32fec5-9b50-4091-831f-9bac732a2be8"), new Guid("d2b8f597-96f8-418b-9819-96c150cad909"), "asp. net açıklama", "admin test", new DateTime(2023, 3, 20, 14, 26, 9, 918, DateTimeKind.Local).AddTicks(768), "admin", null, new Guid("d5a78a10-21a8-49a3-a3b6-e21add292023"), false, "admin", null, "Asp.net core deneme makalesi 1", 15 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "articles",
                keyColumn: "Id",
                keyValue: new Guid("92980c0e-f47f-4393-8da7-2d6a743edbc0"));

            migrationBuilder.DeleteData(
                table: "articles",
                keyColumn: "Id",
                keyValue: new Guid("eb32fec5-9b50-4091-831f-9bac732a2be8"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("3c5c732f-92e5-4707-b26e-e3482a60540f"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("d2b8f597-96f8-418b-9819-96c150cad909"));

            migrationBuilder.DeleteData(
                table: "ımages",
                keyColumn: "Id",
                keyValue: new Guid("9c6a7406-5073-45c5-b2de-a56773c220b4"));

            migrationBuilder.DeleteData(
                table: "ımages",
                keyColumn: "Id",
                keyValue: new Guid("d5a78a10-21a8-49a3-a3b6-e21add292023"));

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "articles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);
        }
    }
}
