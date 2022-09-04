using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBManager.Migrations
{
    public partial class _3rf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2022, 9, 2, 6, 59, 42, 223, DateTimeKind.Local).AddTicks(8031));

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "AccountId", "PaymentDate", "PaymentSum", "PaymentType" },
                values: new object[] { 5, 1, new DateTime(2022, 9, 2, 6, 59, 42, 223, DateTimeKind.Local).AddTicks(8043), 200.0, "in" });

            migrationBuilder.InsertData(
                table: "UserAccounts",
                columns: new[] { "Id", "Currency", "DateCreation", "UserId" },
                values: new object[,]
                {
                    { 2, "EUR", "2021-12-14", 1 },
                    { 3, "RUB", "2021-12-14", 1 },
                    { 4, "USD", "2021-12-14", 2 },
                    { 5, "EUR", "2021-12-14", 2 },
                    { 6, "RUB", "2021-12-14", 2 }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "AccountId", "PaymentDate", "PaymentSum", "PaymentType" },
                values: new object[,]
                {
                    { 2, 2, new DateTime(2022, 9, 2, 6, 59, 42, 223, DateTimeKind.Local).AddTicks(8041), 200.0, "out" },
                    { 3, 2, new DateTime(2022, 9, 2, 6, 59, 42, 223, DateTimeKind.Local).AddTicks(8042), 200.0, "in" },
                    { 4, 5, new DateTime(2022, 9, 2, 6, 59, 42, 223, DateTimeKind.Local).AddTicks(8043), 200.0, "out" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "UserAccounts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UserAccounts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "UserAccounts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "UserAccounts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserAccounts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2022, 9, 2, 6, 57, 0, 41, DateTimeKind.Local).AddTicks(7078));
        }
    }
}
