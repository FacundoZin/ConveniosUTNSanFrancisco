using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIconvenios.Migrations
{
    /// <inheritdoc />
    public partial class FixMappeoCarreraConvenioEspecifico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarrerasConvenioEspecifico_Carreras_CarrerasInvolucradasId",
                table: "CarrerasConvenioEspecifico");

            migrationBuilder.DropForeignKey(
                name: "FK_CarrerasConvenioEspecifico_ConveniosEspecificos_ConveniosInvolucradosId",
                table: "CarrerasConvenioEspecifico");

            migrationBuilder.RenameColumn(
                name: "ConveniosInvolucradosId",
                table: "CarrerasConvenioEspecifico",
                newName: "ConvenioEspecificoId");

            migrationBuilder.RenameColumn(
                name: "CarrerasInvolucradasId",
                table: "CarrerasConvenioEspecifico",
                newName: "CarreraId");

            migrationBuilder.RenameIndex(
                name: "IX_CarrerasConvenioEspecifico_ConveniosInvolucradosId",
                table: "CarrerasConvenioEspecifico",
                newName: "IX_CarrerasConvenioEspecifico_ConvenioEspecificoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarrerasConvenioEspecifico_Carreras_CarreraId",
                table: "CarrerasConvenioEspecifico",
                column: "CarreraId",
                principalTable: "Carreras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarrerasConvenioEspecifico_ConveniosEspecificos_ConvenioEspecificoId",
                table: "CarrerasConvenioEspecifico",
                column: "ConvenioEspecificoId",
                principalTable: "ConveniosEspecificos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarrerasConvenioEspecifico_Carreras_CarreraId",
                table: "CarrerasConvenioEspecifico");

            migrationBuilder.DropForeignKey(
                name: "FK_CarrerasConvenioEspecifico_ConveniosEspecificos_ConvenioEspecificoId",
                table: "CarrerasConvenioEspecifico");

            migrationBuilder.RenameColumn(
                name: "ConvenioEspecificoId",
                table: "CarrerasConvenioEspecifico",
                newName: "ConveniosInvolucradosId");

            migrationBuilder.RenameColumn(
                name: "CarreraId",
                table: "CarrerasConvenioEspecifico",
                newName: "CarrerasInvolucradasId");

            migrationBuilder.RenameIndex(
                name: "IX_CarrerasConvenioEspecifico_ConvenioEspecificoId",
                table: "CarrerasConvenioEspecifico",
                newName: "IX_CarrerasConvenioEspecifico_ConveniosInvolucradosId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarrerasConvenioEspecifico_Carreras_CarrerasInvolucradasId",
                table: "CarrerasConvenioEspecifico",
                column: "CarrerasInvolucradasId",
                principalTable: "Carreras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarrerasConvenioEspecifico_ConveniosEspecificos_ConveniosInvolucradosId",
                table: "CarrerasConvenioEspecifico",
                column: "ConveniosInvolucradosId",
                principalTable: "ConveniosEspecificos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
