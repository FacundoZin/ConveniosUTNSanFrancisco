using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIconvenios.Migrations
{
    /// <inheritdoc />
    public partial class AddContentTypeToArchivos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "ArchivosAdjuntos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "ArchivosAdjuntos");
        }
    }
}
