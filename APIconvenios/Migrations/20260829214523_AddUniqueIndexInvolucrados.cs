using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIconvenios.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexInvolucrados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Fix existing dirty data: null telefonos -> '' before making column NOT NULL
            migrationBuilder.Sql("UPDATE \"Involucrados\" SET \"Telefono\" = '' WHERE \"Telefono\" IS NULL;");

            migrationBuilder.AlterColumn<string>(
                name: "Telefono",
                table: "Involucrados",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Involucrados_Nombre_Apellido_Telefono",
                table: "Involucrados",
                columns: new[] { "Nombre", "Apellido", "Telefono" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Involucrados_Nombre_Apellido_Telefono",
                table: "Involucrados");

            migrationBuilder.AlterColumn<string>(
                name: "Telefono",
                table: "Involucrados",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
