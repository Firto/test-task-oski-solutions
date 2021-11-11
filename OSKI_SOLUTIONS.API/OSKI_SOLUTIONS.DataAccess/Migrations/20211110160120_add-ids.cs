using Microsoft.EntityFrameworkCore.Migrations;

namespace OSKI_SOLUTIONS.DataAccess.Migrations
{
    public partial class addids : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "admin",
                column: "PasswordHash",
                value: "9267FDHKMSORYQ1B489B793E69D49D09864B72D036F52D256E80339736B36CBE956BEDA5C7614AED104B244A5B12F97032C8EDDF9C");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "admin",
                column: "PasswordHash",
                value: "8A438SU+OKTIM5G624D0CBF3E3B84605852BBEAF422849DC8BDACBDEE20657226785C6E25E3DD040B8E5F592AD0F326A4EAF3B98B5");
        }
    }
}
