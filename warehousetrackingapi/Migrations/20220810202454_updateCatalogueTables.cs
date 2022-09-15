using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackingDemoApi.Migrations
{
    public partial class updateCatalogueTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "assetGuid",
                table: "Catalogues");

            migrationBuilder.DropColumn(
                name: "assetType",
                table: "Catalogues");

            migrationBuilder.DropColumn(
                name: "quantity",
                table: "Catalogues");

            migrationBuilder.AddColumn<string>(
                name: "numberOfItems",
                table: "Catalogues",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CatalogueItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    assetType = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    assetGuid = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    quantity = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogueItems", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogueItems");

            migrationBuilder.DropColumn(
                name: "numberOfItems",
                table: "Catalogues");

            migrationBuilder.AddColumn<string>(
                name: "assetGuid",
                table: "Catalogues",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "assetType",
                table: "Catalogues",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "quantity",
                table: "Catalogues",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true);
        }
    }
}
