using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MigrationGeneration.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meters",
                columns: table => new
                {
                    MeterId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MeterName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meters", x => x.MeterId);
                });

            migrationBuilder.CreateTable(
                name: "RateTypes",
                columns: table => new
                {
                    RateTypeId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RateTypeName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateTypes", x => x.RateTypeId);
                });

            migrationBuilder.CreateTable(
                name: "MeterValues",
                columns: table => new
                {
                    MeterValueId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MeterId = table.Column<int>(nullable: false),
                    MeterDate = table.Column<DateTime>(nullable: false),
                    Value = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeterValues", x => x.MeterValueId);
                    table.ForeignKey(
                        name: "FK_MeterValues_Meters_MeterId",
                        column: x => x.MeterId,
                        principalTable: "Meters",
                        principalColumn: "MeterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    RateId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RateTypeId = table.Column<int>(nullable: false),
                    RateName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.RateId);
                    table.ForeignKey(
                        name: "FK_Rates_RateTypes_RateTypeId",
                        column: x => x.RateTypeId,
                        principalTable: "RateTypes",
                        principalColumn: "RateTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeterRates",
                columns: table => new
                {
                    MeterRateId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MeterId = table.Column<int>(nullable: false),
                    RateId = table.Column<int>(nullable: false),
                    ActiveFrom = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeterRates", x => x.MeterRateId);
                    table.ForeignKey(
                        name: "FK_MeterRates_Meters_MeterId",
                        column: x => x.MeterId,
                        principalTable: "Meters",
                        principalColumn: "MeterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeterRates_Rates_RateId",
                        column: x => x.RateId,
                        principalTable: "Rates",
                        principalColumn: "RateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RateValues",
                columns: table => new
                {
                    RateValueId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RateId = table.Column<int>(nullable: false),
                    MeterValueFrom = table.Column<decimal>(nullable: true),
                    MeterValueTo = table.Column<decimal>(nullable: true),
                    TimeFrom = table.Column<TimeSpan>(nullable: true),
                    TimeTo = table.Column<TimeSpan>(nullable: true),
                    ActiveFrom = table.Column<DateTime>(nullable: false),
                    Value = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateValues", x => x.RateId);
                    table.UniqueConstraint("AK_RateValues_RateValueId", x => x.RateValueId);
                    table.ForeignKey(
                        name: "FK_RateValues_Rates_RateId",
                        column: x => x.RateId,
                        principalTable: "Rates",
                        principalColumn: "RateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeterRates_MeterId",
                table: "MeterRates",
                column: "MeterId");

            migrationBuilder.CreateIndex(
                name: "IX_MeterRates_RateId",
                table: "MeterRates",
                column: "RateId");

            migrationBuilder.CreateIndex(
                name: "IX_MeterValues_MeterId",
                table: "MeterValues",
                column: "MeterId");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_RateTypeId",
                table: "Rates",
                column: "RateTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeterRates");

            migrationBuilder.DropTable(
                name: "MeterValues");

            migrationBuilder.DropTable(
                name: "RateValues");

            migrationBuilder.DropTable(
                name: "Meters");

            migrationBuilder.DropTable(
                name: "Rates");

            migrationBuilder.DropTable(
                name: "RateTypes");
        }
    }
}
