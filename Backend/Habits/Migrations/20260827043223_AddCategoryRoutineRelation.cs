using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Habits.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryRoutineRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "RoutineCategories",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_RoutineCategories_id_user",
                table: "Categories",
                newName: "IX_Categories_id_user");

            migrationBuilder.AddColumn<int>(
                name: "RoutineIdRoutine",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_RoutineIdRoutine",
                table: "Categories",
                column: "RoutineIdRoutine");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Routines_RoutineIdRoutine",
                table: "Categories",
                column: "RoutineIdRoutine",
                principalTable: "Routines",
                principalColumn: "id_routine");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Routines_RoutineIdRoutine",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_RoutineIdRoutine",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "RoutineIdRoutine",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "RoutineCategories");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_id_user",
                table: "RoutineCategories",
                newName: "IX_RoutineCategories_id_user");
        }
    }
}
