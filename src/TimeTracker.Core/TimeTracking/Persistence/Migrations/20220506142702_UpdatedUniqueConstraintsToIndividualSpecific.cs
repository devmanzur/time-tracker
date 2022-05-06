using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTracker.Core.TimeTracking.Persistence.Migrations
{
    public partial class UpdatedUniqueConstraintsToIndividualSpecific : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Mandates_Name",
                schema: "time-tracker",
                table: "Mandates");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Name",
                schema: "time-tracker",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_ActivityLabels_Name",
                schema: "time-tracker",
                table: "ActivityLabels");

            migrationBuilder.CreateIndex(
                name: "IX_Mandates_Name_IndividualId",
                schema: "time-tracker",
                table: "Mandates",
                columns: new[] { "Name", "IndividualId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name_IndividualId",
                schema: "time-tracker",
                table: "Categories",
                columns: new[] { "Name", "IndividualId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLabels_Name_IndividualId",
                schema: "time-tracker",
                table: "ActivityLabels",
                columns: new[] { "Name", "IndividualId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Mandates_Name_IndividualId",
                schema: "time-tracker",
                table: "Mandates");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Name_IndividualId",
                schema: "time-tracker",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_ActivityLabels_Name_IndividualId",
                schema: "time-tracker",
                table: "ActivityLabels");

            migrationBuilder.CreateIndex(
                name: "IX_Mandates_Name",
                schema: "time-tracker",
                table: "Mandates",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                schema: "time-tracker",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLabels_Name",
                schema: "time-tracker",
                table: "ActivityLabels",
                column: "Name",
                unique: true);
        }
    }
}
