using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackingDemoApi.Migrations
{
    public partial class catalogue2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "assetType",
                table: "Catalogues",
                newName: "assetGuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "assetGuid",
                table: "Catalogues",
                newName: "assetType");
        }
    }
}
