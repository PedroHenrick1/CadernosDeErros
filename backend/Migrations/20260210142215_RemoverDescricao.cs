using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CadernosDeErros.Migrations
{
    /// <inheritdoc />
    public partial class RemoverDescricao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Assuntos");

            migrationBuilder.AddColumn<string>(
                name: "MinhaResposta",
                table: "Erros",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinhaResposta",
                table: "Erros");

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Assuntos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
