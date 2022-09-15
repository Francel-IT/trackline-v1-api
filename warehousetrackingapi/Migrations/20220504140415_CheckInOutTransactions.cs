using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackingDemoApi.Migrations
{
    public partial class CheckInOutTransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CheckInOutTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tag = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Mode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Employee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionNo = table.Column<long>(type: "bigint", nullable: false),
                    Datecreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Createdby = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Dateupdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updatedby = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckInOutTransactions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckInOutTransactions");
        }
    }
}
