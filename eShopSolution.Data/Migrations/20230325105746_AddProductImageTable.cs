using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class AddProductImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 3, 25, 10, 24, 1, 649, DateTimeKind.Local).AddTicks(4352));

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    ImagePath = table.Column<string>(maxLength: 200, nullable: false),
                    Caption = table.Column<string>(maxLength: 200, nullable: true),
                    IsDefault = table.Column<bool>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    FileSize = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("523e56b1-1354-4c5a-8138-81b053f76f34"),
                column: "ConcurrencyStamp",
                value: "e4065f5e-5996-41c8-aab4-5c592f2b4eab");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("be57d91d-2229-4dfd-a901-5f041978787f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0e221dac-d04d-4153-9dbf-c4fbc49a0566", "AQAAAAEAACcQAAAAEG1OsXZvsLiioylsbtE3IU0X/Hk1u5/SciiCd5eqnHenxxhA5y2DMTsMkZH1tQjPMA==" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 3, 25, 17, 57, 46, 353, DateTimeKind.Local).AddTicks(3216));

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 25, 10, 24, 1, 649, DateTimeKind.Local).AddTicks(4352),
                oldClrType: typeof(DateTime));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("523e56b1-1354-4c5a-8138-81b053f76f34"),
                column: "ConcurrencyStamp",
                value: "2896d0ae-63e7-400d-8975-12c88d188d87");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("be57d91d-2229-4dfd-a901-5f041978787f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8fd1b22a-d0b5-4b11-9f13-7541808b57fd", "AQAAAAEAACcQAAAAEIyY3Dl93xMgXBZ7FN5Ok5CMbdSMysMnPHwAdx0AI3+AZOKnWCxRj/QGrcaqHyIONg==" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 3, 25, 10, 24, 1, 657, DateTimeKind.Local).AddTicks(4180));
        }
    }
}
