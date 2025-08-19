using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIconvenios.Migrations
{
    /// <inheritdoc />
    public partial class MigrationToSqlLite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    RazonSocial = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Cuit = table.Column<string>(type: "TEXT", nullable: false),
                    Direccion = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Telefono = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Involucrados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Apellido = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Telefono = table.Column<string>(type: "TEXT", nullable: false),
                    Legajo = table.Column<int>(type: "INTEGER", nullable: false),
                    RolInvolucrado = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Involucrados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConveniosMarcos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    numeroconvenio = table.Column<int>(type: "INTEGER", nullable: false),
                    Titulo = table.Column<string>(type: "TEXT", nullable: false),
                    EmpresaId = table.Column<int>(type: "INTEGER", nullable: false),
                    FechaFirmaConvenio = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    FechaFin = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    ComentarioOpcional = table.Column<string>(type: "TEXT", nullable: true),
                    RutaArchivo = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConveniosMarcos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConveniosMarcos_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConveniosEspecificos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    numeroconvenio = table.Column<int>(type: "INTEGER", nullable: false),
                    Titulo = table.Column<string>(type: "TEXT", nullable: false),
                    FechaFirmaConvenio = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    FechaInicioActividades = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    FechaFinConvenio = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    ConvenioMarcoId = table.Column<int>(type: "INTEGER", nullable: false),
                    ComentarioOpcional = table.Column<string>(type: "TEXT", nullable: true),
                    RutaArchivo = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConveniosEspecificos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConveniosEspecificos_ConveniosMarcos_ConvenioMarcoId",
                        column: x => x.ConvenioMarcoId,
                        principalTable: "ConveniosMarcos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConvenioEspecificoInvolucrados",
                columns: table => new
                {
                    ConveniosEspecificosId = table.Column<int>(type: "INTEGER", nullable: false),
                    InvolucradosId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConvenioEspecificoInvolucrados", x => new { x.ConveniosEspecificosId, x.InvolucradosId });
                    table.ForeignKey(
                        name: "FK_ConvenioEspecificoInvolucrados_ConveniosEspecificos_ConveniosEspecificosId",
                        column: x => x.ConveniosEspecificosId,
                        principalTable: "ConveniosEspecificos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConvenioEspecificoInvolucrados_Involucrados_InvolucradosId",
                        column: x => x.InvolucradosId,
                        principalTable: "Involucrados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConvenioEspecificoInvolucrados_InvolucradosId",
                table: "ConvenioEspecificoInvolucrados",
                column: "InvolucradosId");

            migrationBuilder.CreateIndex(
                name: "IX_ConveniosEspecificos_ConvenioMarcoId",
                table: "ConveniosEspecificos",
                column: "ConvenioMarcoId");

            migrationBuilder.CreateIndex(
                name: "IX_ConveniosMarcos_EmpresaId",
                table: "ConveniosMarcos",
                column: "EmpresaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConvenioEspecificoInvolucrados");

            migrationBuilder.DropTable(
                name: "ConveniosEspecificos");

            migrationBuilder.DropTable(
                name: "Involucrados");

            migrationBuilder.DropTable(
                name: "ConveniosMarcos");

            migrationBuilder.DropTable(
                name: "Empresas");
        }
    }
}
