using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackingDemoApi.Migrations
{
    public partial class a : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Createdby",
                table: "Location",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Datecreated",
                table: "Location",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Dateupdated",
                table: "Location",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Updatedby",
                table: "Location",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Createdby",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "Datecreated",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "Dateupdated",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "Updatedby",
                table: "Location");
        }
    }
}
