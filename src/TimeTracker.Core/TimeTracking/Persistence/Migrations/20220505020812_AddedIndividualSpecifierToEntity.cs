using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTracker.Core.TimeTracking.Persistence.Migrations
{
    public partial class AddedIndividualSpecifierToEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IndividualId",
                schema: "time-tracker",
                table: "Tag",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IndividualId",
                schema: "time-tracker",
                table: "Mandates",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IndividualId",
                schema: "time-tracker",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IndividualId",
                schema: "time-tracker",
                table: "ActivityLabels",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IndividualId",
                schema: "time-tracker",
                table: "Activities",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_IndividualId",
                schema: "time-tracker",
                table: "Tag",
                column: "IndividualId");

            migrationBuilder.CreateIndex(
                name: "IX_Mandates_IndividualId",
                schema: "time-tracker",
                table: "Mandates",
                column: "IndividualId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_IndividualId",
                schema: "time-tracker",
                table: "Categories",
                column: "IndividualId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLabels_IndividualId",
                schema: "time-tracker",
                table: "ActivityLabels",
                column: "IndividualId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_IndividualId",
                schema: "time-tracker",
                table: "Activities",
                column: "IndividualId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tag_IndividualId",
                schema: "time-tracker",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Mandates_IndividualId",
                schema: "time-tracker",
                table: "Mandates");

            migrationBuilder.DropIndex(
                name: "IX_Categories_IndividualId",
                schema: "time-tracker",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_ActivityLabels_IndividualId",
                schema: "time-tracker",
                table: "ActivityLabels");

            migrationBuilder.DropIndex(
                name: "IX_Activities_IndividualId",
                schema: "time-tracker",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "IndividualId",
                schema: "time-tracker",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "IndividualId",
                schema: "time-tracker",
                table: "Mandates");

            migrationBuilder.DropColumn(
                name: "IndividualId",
                schema: "time-tracker",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "IndividualId",
                schema: "time-tracker",
                table: "ActivityLabels");

            migrationBuilder.DropColumn(
                name: "IndividualId",
                schema: "time-tracker",
                table: "Activities");
        }
    }
}
