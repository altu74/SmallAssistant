using Microsoft.EntityFrameworkCore.Migrations;

namespace MigrationGeneration.Migrations
{
    public partial class MeterNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MeterNumber",
                table: "Meters",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeterNumber",
                table: "Meters");
        }
    }
}
