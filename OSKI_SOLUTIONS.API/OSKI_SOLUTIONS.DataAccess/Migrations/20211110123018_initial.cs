using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OSKI_SOLUTIONS.DataAccess.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    MaxCountOfQuestions = table.Column<int>(nullable: false),
                    TestLengthInMinuts = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Login = table.Column<string>(maxLength: 120, nullable: false),
                    PasswordHash = table.Column<string>(maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionsOfTests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TestId = table.Column<int>(nullable: false),
                    Question = table.Column<string>(nullable: false),
                    maxSelectedQuestionsCount = table.Column<int>(nullable: false),
                    maxQuestionsCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsOfTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionsOfTests_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActiveRefreshTokens",
                columns: table => new
                {
                    Jti = table.Column<string>(nullable: false),
                    UUID = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Expire = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveRefreshTokens", x => x.Jti);
                    table.ForeignKey(
                        name: "FK_ActiveRefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SessionsOfTests",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    TestId = table.Column<int>(nullable: false),
                    StartDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionsOfTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionsOfTests_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionsOfTests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OptionsOfQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Answer = table.Column<string>(nullable: false),
                    Correct = table.Column<bool>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionsOfQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OptionsOfQuestions_QuestionsOfTests_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "QuestionsOfTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionsOfSessions",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    BaseQuestionId = table.Column<int>(nullable: false),
                    SessionOfTestId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsOfSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionsOfSessions_QuestionsOfTests_BaseQuestionId",
                        column: x => x.BaseQuestionId,
                        principalTable: "QuestionsOfTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionsOfSessions_SessionsOfTests_SessionOfTestId",
                        column: x => x.SessionOfTestId,
                        principalTable: "SessionsOfTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OptionsOfQuestionsInSessions",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Selected = table.Column<bool>(nullable: false),
                    BaseOptionId = table.Column<int>(nullable: false),
                    QuestionOfSessionId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionsOfQuestionsInSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OptionsOfQuestionsInSessions_OptionsOfQuestions_BaseOptionId",
                        column: x => x.BaseOptionId,
                        principalTable: "OptionsOfQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OptionsOfQuestionsInSessions_QuestionsOfSessions_QuestionOf~",
                        column: x => x.QuestionOfSessionId,
                        principalTable: "QuestionsOfSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Login", "PasswordHash" },
                values: new object[] { "admin", "admin", "B166ADZL6OMJRZN1759B1BC0DB74110C63A1EC22C940E1002705F51A7C1062908AFA2353DEA4E16E5FF90A982477C9EEBAE620DB40" });

            migrationBuilder.CreateIndex(
                name: "IX_ActiveRefreshTokens_UserId",
                table: "ActiveRefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionsOfQuestions_QuestionId",
                table: "OptionsOfQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionsOfQuestionsInSessions_BaseOptionId",
                table: "OptionsOfQuestionsInSessions",
                column: "BaseOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionsOfQuestionsInSessions_QuestionOfSessionId",
                table: "OptionsOfQuestionsInSessions",
                column: "QuestionOfSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsOfSessions_BaseQuestionId",
                table: "QuestionsOfSessions",
                column: "BaseQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsOfSessions_SessionOfTestId",
                table: "QuestionsOfSessions",
                column: "SessionOfTestId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsOfTests_TestId",
                table: "QuestionsOfTests",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionsOfTests_TestId",
                table: "SessionsOfTests",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionsOfTests_UserId",
                table: "SessionsOfTests",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveRefreshTokens");

            migrationBuilder.DropTable(
                name: "OptionsOfQuestionsInSessions");

            migrationBuilder.DropTable(
                name: "OptionsOfQuestions");

            migrationBuilder.DropTable(
                name: "QuestionsOfSessions");

            migrationBuilder.DropTable(
                name: "QuestionsOfTests");

            migrationBuilder.DropTable(
                name: "SessionsOfTests");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
