using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class ChangeFileLengthType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "FileSize",
                table: "ProductImages",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("523e56b1-1354-4c5a-8138-81b053f76f34"),
                column: "ConcurrencyStamp",
                value: "7850807d-58d5-454d-8443-a5e9bdb58efa");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("be57d91d-2229-4dfd-a901-5f041978787f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "87f9e213-cb01-401c-b5a8-37ce0ad5b79e", "AQAAAAEAACcQAAAAEKBaF2beqlJ5q/M529qkRA0fZD/ujQi8PNIm1iAu9Yr20qQhyl5PHvLV3T4srZtvrA==" });

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
                value: new DateTime(2023, 3, 25, 21, 34, 50, 688, DateTimeKind.Local).AddTicks(6404));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FileSize",
                table: "ProductImages",
                type: "int",
                nullable: false,
                oldClrType: typeof(long));

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
        }
    }
}
