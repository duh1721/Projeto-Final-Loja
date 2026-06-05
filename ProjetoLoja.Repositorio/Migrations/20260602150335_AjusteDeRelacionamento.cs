using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoLoja.Repositorio.Migrations
{
    /// <inheritdoc />
    public partial class AjusteDeRelacionamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItensPedidoId",
                table: "Pedidos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItensPedidoId",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
