using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaDeSupermercado.Data.Migrations
{
    public partial class ModificandoVenda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estoque_Produto_ProdutoId",
                table: "Estoque");

            migrationBuilder.AddColumn<float>(
                name: "Quantidade",
                table: "Saida",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "VendaId",
                table: "Saida",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProdutoId",
                table: "Estoque",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Saida_VendaId",
                table: "Saida",
                column: "VendaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estoque_Produto_ProdutoId",
                table: "Estoque",
                column: "ProdutoId",
                principalTable: "Produto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Saida_Venda_VendaId",
                table: "Saida",
                column: "VendaId",
                principalTable: "Venda",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estoque_Produto_ProdutoId",
                table: "Estoque");

            migrationBuilder.DropForeignKey(
                name: "FK_Saida_Venda_VendaId",
                table: "Saida");

            migrationBuilder.DropIndex(
                name: "IX_Saida_VendaId",
                table: "Saida");

            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "Saida");

            migrationBuilder.DropColumn(
                name: "VendaId",
                table: "Saida");

            migrationBuilder.AlterColumn<int>(
                name: "ProdutoId",
                table: "Estoque",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Estoque_Produto_ProdutoId",
                table: "Estoque",
                column: "ProdutoId",
                principalTable: "Produto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
