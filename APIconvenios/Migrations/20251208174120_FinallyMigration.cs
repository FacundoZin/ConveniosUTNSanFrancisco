using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIconvenios.Migrations
{
    /// <inheritdoc />
    public partial class FinallyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Involucrados_Carreras_AreaId",
                table: "Involucrados");

            migrationBuilder.RenameColumn(
                name: "IdArea",
                table: "Involucrados",
                newName: "IdCarrera");

            migrationBuilder.RenameColumn(
                name: "AreaId",
                table: "Involucrados",
                newName: "CarreraId");

            migrationBuilder.RenameIndex(
                name: "IX_Involucrados_AreaId",
                table: "Involucrados",
                newName: "IX_Involucrados_CarreraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Involucrados_Carreras_CarreraId",
                table: "Involucrados",
                column: "CarreraId",
                principalTable: "Carreras",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Involucrados_Carreras_CarreraId",
                table: "Involucrados");

            migrationBuilder.RenameColumn(
                name: "IdCarrera",
                table: "Involucrados",
                newName: "IdArea");

            migrationBuilder.RenameColumn(
                name: "CarreraId",
                table: "Involucrados",
                newName: "AreaId");

            migrationBuilder.RenameIndex(
                name: "IX_Involucrados_CarreraId",
                table: "Involucrados",
                newName: "IX_Involucrados_AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Involucrados_Carreras_AreaId",
                table: "Involucrados",
                column: "AreaId",
                principalTable: "Carreras",
                principalColumn: "Id");
        }
    }
}
