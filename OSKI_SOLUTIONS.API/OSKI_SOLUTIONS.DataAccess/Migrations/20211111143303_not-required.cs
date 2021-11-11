using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OSKI_SOLUTIONS.DataAccess.Migrations
{
    public partial class notrequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDateTime",
                table: "SessionsOfTests",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "admin",
                column: "PasswordHash",
                value: "A9031GWBTFBHFPHA81373877F69EFFF27D00B175EE4CCA606F6966D68BC9550210CDEA8496130F6295DE7507A934B31D02AB2A5A85");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDateTime",
                table: "SessionsOfTests",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "admin",
                column: "PasswordHash",
                value: "F34D11NAFHWPR4T8253502A06030E66E1FA46DB28872FF60693BDA425AA785497C374EBD35FA4F5BFA2E09902EFCD8FF1F9C22365A");
        }
    }
}
