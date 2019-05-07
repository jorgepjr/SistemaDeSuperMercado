using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaDeSupermercado.Data.Migrations
{
    public partial class AtualizarCategoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Categoria",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Categoria");
        }
    }
}
