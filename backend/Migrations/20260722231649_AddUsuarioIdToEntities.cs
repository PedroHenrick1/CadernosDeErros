using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CadernosDeErros.Migrations
{
    /// <inheritdoc />
    public partial class AddUsuarioIdToEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Materias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Erros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Assuntos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Materias_UsuarioId",
                table: "Materias",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Erros_UsuarioId",
                table: "Erros",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Assuntos_UsuarioId",
                table: "Assuntos",
                column: "UsuarioId");

            // Limpar registros antigos sem usuário associado antes de aplicar as Foreign Keys
            migrationBuilder.Sql("DELETE FROM Erros WHERE UsuarioId NOT IN (SELECT Id FROM Usuarios);");
            migrationBuilder.Sql("DELETE FROM Assuntos WHERE UsuarioId NOT IN (SELECT Id FROM Usuarios);");
            migrationBuilder.Sql("DELETE FROM Materias WHERE UsuarioId NOT IN (SELECT Id FROM Usuarios);");

            migrationBuilder.AddForeignKey(
                name: "FK_Assuntos_Usuarios_UsuarioId",
                table: "Assuntos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Erros_Usuarios_UsuarioId",
                table: "Erros",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Materias_Usuarios_UsuarioId",
                table: "Materias",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assuntos_Usuarios_UsuarioId",
                table: "Assuntos");

            migrationBuilder.DropForeignKey(
                name: "FK_Erros_Usuarios_UsuarioId",
                table: "Erros");

            migrationBuilder.DropForeignKey(
                name: "FK_Materias_Usuarios_UsuarioId",
                table: "Materias");

            migrationBuilder.DropIndex(
                name: "IX_Materias_UsuarioId",
                table: "Materias");

            migrationBuilder.DropIndex(
                name: "IX_Erros_UsuarioId",
                table: "Erros");

            migrationBuilder.DropIndex(
                name: "IX_Assuntos_UsuarioId",
                table: "Assuntos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Materias");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Erros");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Assuntos");
        }
    }
}
