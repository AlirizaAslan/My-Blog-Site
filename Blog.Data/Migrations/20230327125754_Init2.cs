using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "articles",
                keyColumn: "Id",
                keyValue: new Guid("516c9d95-196e-4963-ae3c-1204ac27a864"));

            migrationBuilder.DeleteData(
                table: "articles",
                keyColumn: "Id",
                keyValue: new Guid("66c55041-21ff-42e0-ba71-abcf553b1520"));

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "ımages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "ımages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "categories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "categories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "articles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "articles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("326b8367-9f58-432f-8929-495fe67c698b"),
                column: "ConcurrencyStamp",
                value: "3d93c1f7-5147-4e3a-8e30-c8630347b5bd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("57f9b651-2ec2-4819-9d9b-01d15e3c7e48"),
                column: "ConcurrencyStamp",
                value: "64ff7c0c-57c2-497f-9cdd-b360127bc59e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b90bc202-5666-4016-9a46-2f6d5e9bff6e"),
                column: "ConcurrencyStamp",
                value: "2e50a863-9074-4a59-a8f8-3093bf9e41a6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("52296e36-52d2-4a53-858c-c884baa6e4cc"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "87712b2f-aecc-4cd6-a4b9-46a1acebf6a1", "AQAAAAEAACcQAAAAEDXWH63uAd5HfnSZPAtTHkaWHH9HYMhW+CbORROMCyuD11myk0LCF4mNQu1047jURw==", "e4b41b82-92ce-4b0d-9b43-5beb0a41ddb4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9ee7f3a2-9787-4f52-af19-11dd788ba40f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "186df266-ecfb-4198-ac48-3d1bd0dbaebe", "AQAAAAEAACcQAAAAEEbNZJ4rXNXaSqa4AAQBa01onaKWiP5hZB5p+JXCiThiKntzDulOmQJeZXQ68a+1YQ==", "4b30addb-2b43-4999-a1cd-b6d744ee9062" });

            migrationBuilder.InsertData(
                table: "articles",
                columns: new[] { "Id", "CategoryId", "Content", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "ImageId", "IsDeleted", "ModifiedBy", "ModifiedDate", "Title", "UserId", "ViewCount" },
                values: new object[,]
                {
                    { new Guid("8a184027-218c-45a7-9298-660a2af2601d"), new Guid("3c5c732f-92e5-4707-b26e-e3482a60540f"), "vs açıklama", "admin test", new DateTime(2023, 3, 27, 15, 57, 54, 770, DateTimeKind.Local).AddTicks(7205), "admin", null, new Guid("9c6a7406-5073-45c5-b2de-a56773c220b4"), false, "False", null, "vs deneme makalesi 1", new Guid("52296e36-52d2-4a53-858c-c884baa6e4cc"), 1 },
                    { new Guid("98c51efd-a0c4-46be-83c2-59ba302720c5"), new Guid("d2b8f597-96f8-418b-9819-96c150cad909"), "asp. net açıklama", "admin test", new DateTime(2023, 3, 27, 15, 57, 54, 770, DateTimeKind.Local).AddTicks(7199), "False", null, new Guid("d5a78a10-21a8-49a3-a3b6-e21add292023"), false, "admin", null, "Asp.net core deneme makalesi 1", new Guid("9ee7f3a2-9787-4f52-af19-11dd788ba40f"), 15 }
                });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("3c5c732f-92e5-4707-b26e-e3482a60540f"),
                column: "CreatedDate",
                value: new DateTime(2023, 3, 27, 15, 57, 54, 770, DateTimeKind.Local).AddTicks(7392));

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("d2b8f597-96f8-418b-9819-96c150cad909"),
                column: "CreatedDate",
                value: new DateTime(2023, 3, 27, 15, 57, 54, 770, DateTimeKind.Local).AddTicks(7388));

            migrationBuilder.UpdateData(
                table: "ımages",
                keyColumn: "Id",
                keyValue: new Guid("9c6a7406-5073-45c5-b2de-a56773c220b4"),
                column: "CreatedDate",
                value: new DateTime(2023, 3, 27, 15, 57, 54, 770, DateTimeKind.Local).AddTicks(7508));

            migrationBuilder.UpdateData(
                table: "ımages",
                keyColumn: "Id",
                keyValue: new Guid("d5a78a10-21a8-49a3-a3b6-e21add292023"),
                column: "CreatedDate",
                value: new DateTime(2023, 3, 27, 15, 57, 54, 770, DateTimeKind.Local).AddTicks(7480));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "articles",
                keyColumn: "Id",
                keyValue: new Guid("8a184027-218c-45a7-9298-660a2af2601d"));

            migrationBuilder.DeleteData(
                table: "articles",
                keyColumn: "Id",
                keyValue: new Guid("98c51efd-a0c4-46be-83c2-59ba302720c5"));

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "ımages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "ımages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "articles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "articles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("326b8367-9f58-432f-8929-495fe67c698b"),
                column: "ConcurrencyStamp",
                value: "1c742d24-4193-4518-a69d-85c88b0727f7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("57f9b651-2ec2-4819-9d9b-01d15e3c7e48"),
                column: "ConcurrencyStamp",
                value: "4d137890-8892-480e-b156-4750e45971a0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b90bc202-5666-4016-9a46-2f6d5e9bff6e"),
                column: "ConcurrencyStamp",
                value: "60b51ef0-a97c-49de-8683-99887f91fc0c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("52296e36-52d2-4a53-858c-c884baa6e4cc"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8652b49a-93ea-4d6a-9174-1672d0e0ca9f", "AQAAAAEAACcQAAAAEDxxVw4UtNK01ONxEMHQy8+l8uEANG/+ssflBPg1ftsIwTMwePNmwhGoMh1/7YIwPQ==", "76e8ffe0-c246-4226-9ff3-9e39e75309a0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9ee7f3a2-9787-4f52-af19-11dd788ba40f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "130fde6c-e539-4842-885c-690304117351", "AQAAAAEAACcQAAAAECKmHnuc64FJj5HEfLNJhBuJjhGctvbdKpP7MUwvzuEiHb9fCjhnB52xG/uttshtbg==", "0255b05e-5214-48ab-b2ae-891fcf7dc27e" });

            migrationBuilder.InsertData(
                table: "articles",
                columns: new[] { "Id", "CategoryId", "Content", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "ImageId", "IsDeleted", "ModifiedBy", "ModifiedDate", "Title", "UserId", "ViewCount" },
                values: new object[,]
                {
                    { new Guid("516c9d95-196e-4963-ae3c-1204ac27a864"), new Guid("d2b8f597-96f8-418b-9819-96c150cad909"), "asp. net açıklama", "admin test", new DateTime(2023, 3, 24, 23, 7, 15, 344, DateTimeKind.Local).AddTicks(513), "False", null, new Guid("d5a78a10-21a8-49a3-a3b6-e21add292023"), false, "admin", null, "Asp.net core deneme makalesi 1", new Guid("9ee7f3a2-9787-4f52-af19-11dd788ba40f"), 15 },
                    { new Guid("66c55041-21ff-42e0-ba71-abcf553b1520"), new Guid("3c5c732f-92e5-4707-b26e-e3482a60540f"), "vs açıklama", "admin test", new DateTime(2023, 3, 24, 23, 7, 15, 344, DateTimeKind.Local).AddTicks(518), "admin", null, new Guid("9c6a7406-5073-45c5-b2de-a56773c220b4"), false, "False", null, "vs deneme makalesi 1", new Guid("52296e36-52d2-4a53-858c-c884baa6e4cc"), 1 }
                });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("3c5c732f-92e5-4707-b26e-e3482a60540f"),
                column: "CreatedDate",
                value: new DateTime(2023, 3, 24, 23, 7, 15, 344, DateTimeKind.Local).AddTicks(697));

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("d2b8f597-96f8-418b-9819-96c150cad909"),
                column: "CreatedDate",
                value: new DateTime(2023, 3, 24, 23, 7, 15, 344, DateTimeKind.Local).AddTicks(694));

            migrationBuilder.UpdateData(
                table: "ımages",
                keyColumn: "Id",
                keyValue: new Guid("9c6a7406-5073-45c5-b2de-a56773c220b4"),
                column: "CreatedDate",
                value: new DateTime(2023, 3, 24, 23, 7, 15, 344, DateTimeKind.Local).AddTicks(788));

            migrationBuilder.UpdateData(
                table: "ımages",
                keyColumn: "Id",
                keyValue: new Guid("d5a78a10-21a8-49a3-a3b6-e21add292023"),
                column: "CreatedDate",
                value: new DateTime(2023, 3, 24, 23, 7, 15, 344, DateTimeKind.Local).AddTicks(786));
        }
    }
}
