using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataProvider.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 29, 12, 17, 50, 281, DateTimeKind.Local).AddTicks(5129), new DateTime(2024, 11, 29, 12, 17, 50, 281, DateTimeKind.Local).AddTicks(5141) });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 29, 12, 17, 50, 281, DateTimeKind.Local).AddTicks(5156), new DateTime(2024, 11, 29, 12, 17, 50, 281, DateTimeKind.Local).AddTicks(5158) });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 29, 12, 17, 50, 281, DateTimeKind.Local).AddTicks(5161), new DateTime(2024, 11, 29, 12, 17, 50, 281, DateTimeKind.Local).AddTicks(5162) });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 29, 12, 17, 50, 281, DateTimeKind.Local).AddTicks(5164), new DateTime(2024, 11, 29, 12, 17, 50, 281, DateTimeKind.Local).AddTicks(5165) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "LastPasswordChangeTime", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "GJTGHVCXAUB6HNP2G792MM1BLZSFQ4ZC", new DateTime(2024, 11, 29, 12, 17, 50, 307, DateTimeKind.Local).AddTicks(3382), new DateTime(2024, 11, 29, 12, 17, 50, 307, DateTimeKind.Local).AddTicks(2909), "K+OT46JO1mOyM2ssV1kk5UtqYjRwioEBrMe6N6pZgak=.U2joiPVNNgMNGliJSILOow==", "NUQJOS31XX2T3ZGVLUSR6WUA7ZXFVUSD", new DateTime(2024, 11, 29, 12, 17, 50, 307, DateTimeKind.Local).AddTicks(3391) });

            migrationBuilder.UpdateData(
                table: "UserRole",
                keyColumns: new[] { "Id", "RoleId", "UserId" },
                keyValues: new object[] { 0, 1, 1 },
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 29, 12, 17, 50, 278, DateTimeKind.Local).AddTicks(5726), new DateTime(2024, 11, 29, 12, 17, 50, 280, DateTimeKind.Local).AddTicks(7286) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 29, 12, 15, 1, 961, DateTimeKind.Local).AddTicks(6489), new DateTime(2024, 11, 29, 12, 15, 1, 961, DateTimeKind.Local).AddTicks(6502) });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 29, 12, 15, 1, 961, DateTimeKind.Local).AddTicks(6520), new DateTime(2024, 11, 29, 12, 15, 1, 961, DateTimeKind.Local).AddTicks(6523) });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 29, 12, 15, 1, 961, DateTimeKind.Local).AddTicks(6525), new DateTime(2024, 11, 29, 12, 15, 1, 961, DateTimeKind.Local).AddTicks(6527) });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 29, 12, 15, 1, 961, DateTimeKind.Local).AddTicks(6529), new DateTime(2024, 11, 29, 12, 15, 1, 961, DateTimeKind.Local).AddTicks(6530) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "LastPasswordChangeTime", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { "AVJLJO8MZPNS0EU06K2YPWC11FHCDMG7", new DateTime(2024, 11, 29, 12, 15, 1, 988, DateTimeKind.Local).AddTicks(7678), new DateTime(2024, 11, 29, 12, 15, 1, 988, DateTimeKind.Local).AddTicks(7195), "183+L5O8zIiQrakIbQT9KfZ/94Pqhw4EblDmrif/t0w=.YLcvHV8Sz0UWUz3scCM3lQ==", "7CY36WHO2Z83IYWAYG0SNJTTCUT5IXU7", new DateTime(2024, 11, 29, 12, 15, 1, 988, DateTimeKind.Local).AddTicks(7687) });

            migrationBuilder.UpdateData(
                table: "UserRole",
                keyColumns: new[] { "Id", "RoleId", "UserId" },
                keyValues: new object[] { 0, 1, 1 },
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 29, 12, 15, 1, 958, DateTimeKind.Local).AddTicks(6731), new DateTime(2024, 11, 29, 12, 15, 1, 960, DateTimeKind.Local).AddTicks(8027) });
        }
    }
}
