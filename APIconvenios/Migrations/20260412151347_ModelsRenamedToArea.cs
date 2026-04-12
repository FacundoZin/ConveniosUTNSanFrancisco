using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIconvenios.Migrations
{
    /// <inheritdoc />
    public partial class ModelsRenamedToArea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Involucrados_Carreras_CarrerasId",
                table: "Involucrados");

            migrationBuilder.RenameColumn(
                name: "CarrerasId",
                table: "Involucrados",
                newName: "AreaId");

            migrationBuilder.RenameIndex(
                name: "IX_Involucrados_CarrerasId",
                table: "Involucrados",
                newName: "IX_Involucrados_AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Involucrados_Carreras_AreaId",
                table: "Involucrados",
                column: "AreaId",
                principalTable: "Carreras",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Involucrados_Carreras_AreaId",
                table: "Involucrados");

            migrationBuilder.RenameColumn(
                name: "AreaId",
                table: "Involucrados",
                newName: "CarrerasId");

            migrationBuilder.RenameIndex(
                name: "IX_Involucrados_AreaId",
                table: "Involucrados",
                newName: "IX_Involucrados_CarrerasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Involucrados_Carreras_CarrerasId",
                table: "Involucrados",
                column: "CarrerasId",
                principalTable: "Carreras",
                principalColumn: "Id");
        }
    }
}
