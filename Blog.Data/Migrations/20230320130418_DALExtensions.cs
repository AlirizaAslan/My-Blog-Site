using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class DALExtensions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "articles",
                keyColumn: "Id",
                keyValue: new Guid("92980c0e-f47f-4393-8da7-2d6a743edbc0"));

            migrationBuilder.DeleteData(
                table: "articles",
                keyColumn: "Id",
                keyValue: new Guid("eb32fec5-9b50-4091-831f-9bac732a2be8"));

            migrationBuilder.InsertData(
                table: "articles",
                columns: new[] { "Id", "CategoryId", "Content", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "ImageId", "IsDeleted", "ModifiedBy", "ModifiedDate", "Title", "ViewCount" },
                values: new object[,]
                {
                    { new Guid("a166a5bd-1790-431f-b449-b5694c6ccad6"), new Guid("3c5c732f-92e5-4707-b26e-e3482a60540f"), "vs açıklama", "admin test", new DateTime(2023, 3, 20, 16, 4, 18, 407, DateTimeKind.Local).AddTicks(5512), "admin", null, new Guid("9c6a7406-5073-45c5-b2de-a56773c220b4"), false, "False", null, "vs deneme makalesi 1", 1 },
                    { new Guid("e0c349aa-9f4d-49cd-ace0-7a44fd94c904"), new Guid("d2b8f597-96f8-418b-9819-96c150cad909"), "asp. net açıklama", "admin test", new DateTime(2023, 3, 20, 16, 4, 18, 407, DateTimeKind.Local).AddTicks(5508), "False", null, new Guid("d5a78a10-21a8-49a3-a3b6-e21add292023"), false, "admin", null, "Asp.net core deneme makalesi 1", 15 }
                });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("3c5c732f-92e5-4707-b26e-e3482a60540f"),
                columns: new[] { "CreatedDate", "DeletedBy" },
                values: new object[] { new DateTime(2023, 3, 20, 16, 4, 18, 407, DateTimeKind.Local).AddTicks(5711), "false" });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("d2b8f597-96f8-418b-9819-96c150cad909"),
                columns: new[] { "CreatedDate", "DeletedBy" },
                values: new object[] { new DateTime(2023, 3, 20, 16, 4, 18, 407, DateTimeKind.Local).AddTicks(5708), "false" });

            migrationBuilder.UpdateData(
                table: "ımages",
                keyColumn: "Id",
                keyValue: new Guid("9c6a7406-5073-45c5-b2de-a56773c220b4"),
                columns: new[] { "CreatedDate", "DeletedBy" },
                values: new object[] { new DateTime(2023, 3, 20, 16, 4, 18, 407, DateTimeKind.Local).AddTicks(5816), "false" });

            migrationBuilder.UpdateData(
                table: "ımages",
                keyColumn: "Id",
                keyValue: new Guid("d5a78a10-21a8-49a3-a3b6-e21add292023"),
                columns: new[] { "CreatedDate", "DeletedBy" },
                values: new object[] { new DateTime(2023, 3, 20, 16, 4, 18, 407, DateTimeKind.Local).AddTicks(5813), "false" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "articles",
                keyColumn: "Id",
                keyValue: new Guid("a166a5bd-1790-431f-b449-b5694c6ccad6"));

            migrationBuilder.DeleteData(
                table: "articles",
                keyColumn: "Id",
                keyValue: new Guid("e0c349aa-9f4d-49cd-ace0-7a44fd94c904"));

            migrationBuilder.InsertData(
                table: "articles",
                columns: new[] { "Id", "CategoryId", "Content", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "ImageId", "IsDeleted", "ModifiedBy", "ModifiedDate", "Title", "ViewCount" },
                values: new object[,]
                {
                    { new Guid("92980c0e-f47f-4393-8da7-2d6a743edbc0"), new Guid("3c5c732f-92e5-4707-b26e-e3482a60540f"), "vs açıklama", "admin test", new DateTime(2023, 3, 20, 14, 26, 9, 918, DateTimeKind.Local).AddTicks(776), "admin", null, new Guid("9c6a7406-5073-45c5-b2de-a56773c220b4"), false, "admin", null, "vs deneme makalesi 1", 1 },
                    { new Guid("eb32fec5-9b50-4091-831f-9bac732a2be8"), new Guid("d2b8f597-96f8-418b-9819-96c150cad909"), "asp. net açıklama", "admin test", new DateTime(2023, 3, 20, 14, 26, 9, 918, DateTimeKind.Local).AddTicks(768), "admin", null, new Guid("d5a78a10-21a8-49a3-a3b6-e21add292023"), false, "admin", null, "Asp.net core deneme makalesi 1", 15 }
                });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("3c5c732f-92e5-4707-b26e-e3482a60540f"),
                columns: new[] { "CreatedDate", "DeletedBy" },
                values: new object[] { new DateTime(2023, 3, 20, 14, 26, 9, 918, DateTimeKind.Local).AddTicks(962), "admin" });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("d2b8f597-96f8-418b-9819-96c150cad909"),
                columns: new[] { "CreatedDate", "DeletedBy" },
                values: new object[] { new DateTime(2023, 3, 20, 14, 26, 9, 918, DateTimeKind.Local).AddTicks(959), "admin" });

            migrationBuilder.UpdateData(
                table: "ımages",
                keyColumn: "Id",
                keyValue: new Guid("9c6a7406-5073-45c5-b2de-a56773c220b4"),
                columns: new[] { "CreatedDate", "DeletedBy" },
                values: new object[] { new DateTime(2023, 3, 20, 14, 26, 9, 918, DateTimeKind.Local).AddTicks(1078), "admin" });

            migrationBuilder.UpdateData(
                table: "ımages",
                keyColumn: "Id",
                keyValue: new Guid("d5a78a10-21a8-49a3-a3b6-e21add292023"),
                columns: new[] { "CreatedDate", "DeletedBy" },
                values: new object[] { new DateTime(2023, 3, 20, 14, 26, 9, 918, DateTimeKind.Local).AddTicks(1075), "admin" });
        }
    }
}
