using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaDeSupermercado.Data.Migrations
{
    public partial class AtualizarFornecedor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Fornecedor",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Fornecedor");
        }
    }
}
