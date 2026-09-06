using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Habits.Migrations
{
    /// <inheritdoc />
    public partial class FixCategoryRoutineRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Routines_RoutineIdRoutine",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Routines_RoutineCategories",
                table: "Routines");

            migrationBuilder.DropIndex(
                name: "IX_Routines_id_routine_category",
                table: "Routines");

            migrationBuilder.DropIndex(
                name: "IX_Categories_RoutineIdRoutine",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "id_routine_category",
                table: "Routines");

            migrationBuilder.DropColumn(
                name: "RoutineIdRoutine",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "CategoryRoutine",
                columns: table => new
                {
                    CategoriesIdCategory = table.Column<int>(type: "int", nullable: false),
                    RoutinesIdRoutine = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryRoutine", x => new { x.CategoriesIdCategory, x.RoutinesIdRoutine });
                    table.ForeignKey(
                        name: "FK_CategoryRoutine_Categories_CategoriesIdCategory",
                        column: x => x.CategoriesIdCategory,
                        principalTable: "Categories",
                        principalColumn: "id_routine_category",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryRoutine_Routines_RoutinesIdRoutine",
                        column: x => x.RoutinesIdRoutine,
                        principalTable: "Routines",
                        principalColumn: "id_routine",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryRoutine_RoutinesIdRoutine",
                table: "CategoryRoutine",
                column: "RoutinesIdRoutine");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryRoutine");

            migrationBuilder.AddColumn<int>(
                name: "id_routine_category",
                table: "Routines",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoutineIdRoutine",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Routines_id_routine_category",
                table: "Routines",
                column: "id_routine_category");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Routines_RoutineCategories",
                table: "Routines",
                column: "id_routine_category",
                principalTable: "Categories",
                principalColumn: "id_routine_category");
        }
    }
}
