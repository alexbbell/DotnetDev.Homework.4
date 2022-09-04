using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBManager.Migrations
{
    public partial class _2nd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserAccounts",
                columns: new[] { "Id", "Currency", "DateCreation", "UserId" },
                values: new object[] { 1, "USD", "2021-12-14", 1 });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "AccountId", "PaymentDate", "PaymentSum", "PaymentType" },
                values: new object[] { 1, 1, new DateTime(2022, 9, 2, 6, 57, 0, 41, DateTimeKind.Local).AddTicks(7078), 200.0, "in" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserAccounts",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
