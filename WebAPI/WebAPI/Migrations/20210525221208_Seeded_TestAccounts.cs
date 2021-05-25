using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class Seeded_TestAccounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeterReadingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MeterReadValue = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountId", "FirstName", "LastName", "MeterReadValue", "MeterReadingDate" },
                values: new object[,]
                {
                    { 2344, "Tommy", "Test", null, null },
                    { 1246, "Jo", "Test", null, null },
                    { 1245, "Neville", "Test", null, null },
                    { 1244, "Tony", "Test", null, null },
                    { 1243, "Graham", "Test", null, null },
                    { 1242, "Tim", "Test", null, null },
                    { 1241, "Lara", "Test", null, null },
                    { 1240, "Archie", "Test", null, null },
                    { 1239, "Noddy", "Test", null, null },
                    { 1234, "Freya", "Test", null, null },
                    { 4534, "JOSH", "TEST", null, null },
                    { 6776, "Laura", "Test", null, null },
                    { 1247, "Jim", "Test", null, null },
                    { 2356, "Craig", "Test", null, null },
                    { 2353, "Tony", "Test", null, null },
                    { 2352, "Greg", "Test", null, null },
                    { 2351, "Gladys", "Test", null, null },
                    { 2350, "Colin", "Test", null, null },
                    { 2349, "Simon", "Test", null, null },
                    { 2348, "Tammy", "Test", null, null },
                    { 2347, "Tara", "Test", null, null },
                    { 2346, "Ollie", "Test", null, null },
                    { 2345, "Jerry", "Test", null, null },
                    { 8766, "Sally", "Test", null, null },
                    { 2233, "Barry", "Test", null, null },
                    { 2355, "Arthur", "Test", null, null },
                    { 1248, "Pam", "Test", null, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
