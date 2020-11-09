using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogCore.AccesosDatos.Migrations
{
    public partial class NuevoCampoDescripcion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Articulo",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Articulo",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Articulo");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Articulo",
                newName: "id");
        }
    }
}
