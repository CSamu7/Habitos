using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Habits.Migrations
{
    /// <inheritdoc />
    public partial class AddMinGoalHistoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MinGoalHistory",
                columns: table => new
                {
                    IdMinGoalHistory = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MinGoal = table.Column<int>(type: "int", nullable: false),
                    ChangedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinGoalHistory", x => x.IdMinGoalHistory);
                    table.ForeignKey(
                        name: "FK_MinGoalHistory_AspNetUsers",
                        column: x => x.IdUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MinGoalHistory_IdUser",
                table: "MinGoalHistory",
                column: "IdUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MinGoalHistory");
        }
    }
}
