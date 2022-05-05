using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTracker.Core.TimeTracking.Persistence.Migrations
{
    public partial class AddedMandates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mandates",
                schema: "time-tracker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ColorCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mandates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mandates_Name",
                schema: "time-tracker",
                table: "Mandates",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mandates",
                schema: "time-tracker");
        }
    }
}
