using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoLoja.Repositorio.Migrations
{
    /// <inheritdoc />
    public partial class TipoUsuarioBancodeDados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoUsuarioId",
                table: "Logins",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipoUsuarioId",
                table: "Clientes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Logins",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "LoginId",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "TipoUsuarioId",
                table: "Clientes");

            migrationBuilder.RenameColumn(
                name: "TipoUsuarioId",
                table: "Logins",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Logins",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Logins",
                table: "Logins",
                column: "Id");
        }
    }
}
