using Microsoft.EntityFrameworkCore.Migrations;

namespace OSKI_SOLUTIONS.DataAccess.Migrations
{
    public partial class editName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "maxQuestionsCount",
                table: "QuestionsOfTests");

            migrationBuilder.DropColumn(
                name: "maxSelectedQuestionsCount",
                table: "QuestionsOfTests");

            migrationBuilder.AddColumn<int>(
                name: "maxOptionsCount",
                table: "QuestionsOfTests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "maxSelectedOptionsCount",
                table: "QuestionsOfTests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "admin",
                column: "PasswordHash",
                value: "CB0137MHNXHKEHUAED88B1D618E41CB2A8915638F1065F66FC75F9E1D48B0CF3B2AD0360CCC881593F5622C587EBB8A4BE4EEBA05D");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "maxOptionsCount",
                table: "QuestionsOfTests");

            migrationBuilder.DropColumn(
                name: "maxSelectedOptionsCount",
                table: "QuestionsOfTests");

            migrationBuilder.AddColumn<int>(
                name: "maxQuestionsCount",
                table: "QuestionsOfTests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "maxSelectedQuestionsCount",
                table: "QuestionsOfTests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "admin",
                column: "PasswordHash",
                value: "B166ADZL6OMJRZN1759B1BC0DB74110C63A1EC22C940E1002705F51A7C1062908AFA2353DEA4E16E5FF90A982477C9EEBAE620DB40");
        }
    }
}
