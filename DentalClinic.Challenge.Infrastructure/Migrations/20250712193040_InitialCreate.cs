using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalClinic.Challenge.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "especialidades",
                columns: table => new
                {
                    id_especialidad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cod_especialidad = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    rowversion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_especialidades", x => x.id_especialidad);
                });

            migrationBuilder.CreateIndex(
                name: "IX_especialidades_cod_especialidad",
                table: "especialidades",
                column: "cod_especialidad",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_especialidades_descripcion",
                table: "especialidades",
                column: "descripcion",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "especialidades");
        }
    }
}
