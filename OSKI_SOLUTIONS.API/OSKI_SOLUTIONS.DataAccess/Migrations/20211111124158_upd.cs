using Microsoft.EntityFrameworkCore.Migrations;

namespace OSKI_SOLUTIONS.DataAccess.Migrations
{
    public partial class upd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OptionsOfQuestionsInSessions_QuestionsOfSessions_QuestionOf~",
                table: "OptionsOfQuestionsInSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionsOfSessions_SessionsOfTests_SessionOfTestId",
                table: "QuestionsOfSessions");

            migrationBuilder.AlterColumn<string>(
                name: "SessionOfTestId",
                table: "QuestionsOfSessions",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "QuestionOfSessionId",
                table: "OptionsOfQuestionsInSessions",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "admin",
                column: "PasswordHash",
                value: "6C2D8P66+ONA12X28E315BE69B1517827CFC342F8C4B134A7184A1066209B3D4B043BBDA18D5B464E97D48149449200E181C3AD619");

            migrationBuilder.AddForeignKey(
                name: "FK_OptionsOfQuestionsInSessions_QuestionsOfSessions_QuestionOf~",
                table: "OptionsOfQuestionsInSessions",
                column: "QuestionOfSessionId",
                principalTable: "QuestionsOfSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionsOfSessions_SessionsOfTests_SessionOfTestId",
                table: "QuestionsOfSessions",
                column: "SessionOfTestId",
                principalTable: "SessionsOfTests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade); 
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OptionsOfQuestionsInSessions_QuestionsOfSessions_QuestionOf~",
                table: "OptionsOfQuestionsInSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionsOfSessions_SessionsOfTests_SessionOfTestId",
                table: "QuestionsOfSessions");

            migrationBuilder.AlterColumn<string>(
                name: "SessionOfTestId",
                table: "QuestionsOfSessions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "QuestionOfSessionId",
                table: "OptionsOfQuestionsInSessions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "admin",
                column: "PasswordHash",
                value: "9267FDHKMSORYQ1B489B793E69D49D09864B72D036F52D256E80339736B36CBE956BEDA5C7614AED104B244A5B12F97032C8EDDF9C");

            migrationBuilder.AddForeignKey(
                name: "FK_OptionsOfQuestionsInSessions_QuestionsOfSessions_QuestionOf~",
                table: "OptionsOfQuestionsInSessions",
                column: "QuestionOfSessionId",
                principalTable: "QuestionsOfSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionsOfSessions_SessionsOfTests_SessionOfTestId",
                table: "QuestionsOfSessions",
                column: "SessionOfTestId",
                principalTable: "SessionsOfTests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
