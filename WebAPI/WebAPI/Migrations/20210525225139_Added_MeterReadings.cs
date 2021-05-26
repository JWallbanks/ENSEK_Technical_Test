using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class Added_MeterReadings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeterReadValue",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "MeterReadingDate",
                table: "Accounts");

            migrationBuilder.CreateTable(
                name: "MeterReadings",
                columns: table => new
                {
                    MeterReadingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeterReadingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MeterReadValue = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeterReadings", x => x.MeterReadingId);
                    table.ForeignKey(
                        name: "FK_MeterReadings_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeterReadings_AccountId",
                table: "MeterReadings",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeterReadings");

            migrationBuilder.AddColumn<int>(
                name: "MeterReadValue",
                table: "Accounts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MeterReadingDate",
                table: "Accounts",
                type: "datetime2",
                nullable: true);
        }
    }
}
