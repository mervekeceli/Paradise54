using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class addNoteagain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "Foods");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "CartItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "CartItems");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Foods",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
