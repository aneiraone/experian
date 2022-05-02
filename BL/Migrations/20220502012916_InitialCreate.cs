using Microsoft.EntityFrameworkCore.Migrations;

namespace ExperianCore.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Error",
                table: "Documento",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Razon",
                table: "Documento",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Error",
                table: "Documento");

            migrationBuilder.DropColumn(
                name: "Razon",
                table: "Documento");
        }
    }
}
