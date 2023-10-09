using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Data.Migrations
{
    public partial class Kindergartens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Kindergarten",
                table: "Kindergarten");

            migrationBuilder.RenameTable(
                name: "Kindergarten",
                newName: "Kindergartens");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kindergartens",
                table: "Kindergartens",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Kindergartens",
                table: "Kindergartens");

            migrationBuilder.RenameTable(
                name: "Kindergartens",
                newName: "Kindergarten");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kindergarten",
                table: "Kindergarten",
                column: "Id");
        }
    }
}
