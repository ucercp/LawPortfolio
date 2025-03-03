using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPortolio.Migrations
{
    public partial class paswordResetToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordResetToken",
                table: "AdminUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetTokenExpires",
                table: "AdminUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "AdminUserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$5Wu/xs5G5jk9eYL39/Xgk..PA.Vyd9uaWlOHzPXlwowKkPXehKjc.");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordResetToken",
                table: "AdminUsers");

            migrationBuilder.DropColumn(
                name: "ResetTokenExpires",
                table: "AdminUsers");

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "AdminUserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$xPVQReoczs8zzCxTN.XCdu0f/U55okokJShRr3ElLmD1aKsVKcEyy");
        }
    }
}
