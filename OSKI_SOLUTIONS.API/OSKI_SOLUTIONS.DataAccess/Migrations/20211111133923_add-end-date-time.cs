using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OSKI_SOLUTIONS.DataAccess.Migrations
{
    public partial class addenddatetime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDateTime",
                table: "SessionsOfTests",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "admin",
                column: "PasswordHash",
                value: "F34D11NAFHWPR4T8253502A06030E66E1FA46DB28872FF60693BDA425AA785497C374EBD35FA4F5BFA2E09902EFCD8FF1F9C22365A");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDateTime",
                table: "SessionsOfTests");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "admin",
                column: "PasswordHash",
                value: "6C2D8P66+ONA12X28E315BE69B1517827CFC342F8C4B134A7184A1066209B3D4B043BBDA18D5B464E97D48149449200E181C3AD619");
        }
    }
}
