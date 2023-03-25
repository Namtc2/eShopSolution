using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class SeedIdentityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 25, 10, 24, 1, 649, DateTimeKind.Local).AddTicks(4352),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 3, 25, 9, 38, 19, 644, DateTimeKind.Local).AddTicks(3016));

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("523e56b1-1354-4c5a-8138-81b053f76f34"), "2896d0ae-63e7-400d-8975-12c88d188d87", "Administrator role", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("523e56b1-1354-4c5a-8138-81b053f76f34"), new Guid("be57d91d-2229-4dfd-a901-5f041978787f") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("be57d91d-2229-4dfd-a901-5f041978787f"), 0, "8fd1b22a-d0b5-4b11-9f13-7541808b57fd", new DateTime(1996, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "trancongnam15@gmail.com", true, "Nam", "Tran", false, null, "trancongnam15@gmail.com", "admin", "AQAAAAEAACcQAAAAEIyY3Dl93xMgXBZ7FN5Ok5CMbdSMysMnPHwAdx0AI3+AZOKnWCxRj/QGrcaqHyIONg==", null, false, "", false, "admin" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("523e56b1-1354-4c5a-8138-81b053f76f34"));

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("523e56b1-1354-4c5a-8138-81b053f76f34"), new Guid("be57d91d-2229-4dfd-a901-5f041978787f") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("be57d91d-2229-4dfd-a901-5f041978787f"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 25, 9, 38, 19, 644, DateTimeKind.Local).AddTicks(3016),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2023, 3, 25, 10, 24, 1, 649, DateTimeKind.Local).AddTicks(4352));

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
                value: new DateTime(2023, 3, 25, 9, 38, 19, 655, DateTimeKind.Local).AddTicks(7969));
        }
    }
}
