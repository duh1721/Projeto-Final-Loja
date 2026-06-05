using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoLoja.Repositorio.Migrations
{
    /// <inheritdoc />
    public partial class RelacionamentoDeItensProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Produtos_ProdutoId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_ProdutoId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "ProdutoId",
                table: "Pedidos");

            migrationBuilder.AddColumn<int>(
                name: "ItensPedidoId",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItensPedidoId",
                table: "Pedidos");

            migrationBuilder.AddColumn<int>(
                name: "ProdutoId",
                table: "Pedidos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ProdutoId",
                table: "Pedidos",
                column: "ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Produtos_ProdutoId",
                table: "Pedidos",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "ProdutoId");
        }
    }
}
