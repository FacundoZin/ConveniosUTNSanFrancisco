using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIconvenios.Migrations
{
    /// <inheritdoc />
    public partial class migracionFinalProduccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carreras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carreras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    RazonSocial = table.Column<string>(type: "TEXT", nullable: true),
                    Cuit = table.Column<string>(type: "TEXT", nullable: true),
                    Direccion = table.Column<string>(type: "TEXT", nullable: true),
                    Telefono = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    ConvenioMarcoId = table.Column<int>(type: "INTEGER", nullable: true)
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
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Telefono = table.Column<string>(type: "TEXT", nullable: true),
                    Legajo = table.Column<int>(type: "INTEGER", nullable: true),
                    RolInvolucrado = table.Column<int>(type: "INTEGER", nullable: false),
                    IdCarrera = table.Column<int>(type: "INTEGER", nullable: true),
                    CarrerasId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Involucrados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Involucrados_Carreras_CarrerasId",
                        column: x => x.CarrerasId,
                        principalTable: "Carreras",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Involucrados_Carreras_IdCarrera",
                        column: x => x.IdCarrera,
                        principalTable: "Carreras",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConveniosMarcos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    numeroconvenio = table.Column<string>(type: "TEXT", nullable: true),
                    Titulo = table.Column<string>(type: "TEXT", nullable: false),
                    FechaFirmaConvenio = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    FechaFin = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    ComentarioOpcional = table.Column<string>(type: "TEXT", nullable: true),
                    Estado = table.Column<int>(type: "INTEGER", nullable: false),
                    NumeroResolucion = table.Column<string>(type: "TEXT", nullable: true),
                    Refrendado = table.Column<bool>(type: "INTEGER", nullable: false),
                    EmpresaId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConveniosMarcos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConveniosMarcos_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConveniosEspecificos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    numeroconvenio = table.Column<string>(type: "TEXT", nullable: true),
                    TituloConvenio = table.Column<string>(type: "TEXT", nullable: false),
                    FechaFirmaConvenio = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    FechaInicioActividades = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    FechaFinConvenio = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    ComentarioOpcional = table.Column<string>(type: "TEXT", nullable: true),
                    Estado = table.Column<int>(type: "INTEGER", nullable: false),
                    EsActa = table.Column<bool>(type: "INTEGER", nullable: false),
                    NumeroResolucion = table.Column<string>(type: "TEXT", nullable: true),
                    Refrendado = table.Column<bool>(type: "INTEGER", nullable: false),
                    ConvenioMarcoId = table.Column<int>(type: "INTEGER", nullable: true),
                    EmpresaId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConveniosEspecificos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConveniosEspecificos_ConveniosMarcos_ConvenioMarcoId",
                        column: x => x.ConvenioMarcoId,
                        principalTable: "ConveniosMarcos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConveniosEspecificos_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ArchivosAdjuntos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NombreArchivo = table.Column<string>(type: "TEXT", nullable: false),
                    RutaArchivo = table.Column<string>(type: "TEXT", nullable: false),
                    ContentType = table.Column<string>(type: "TEXT", nullable: false),
                    ConvenioEspecificoId = table.Column<int>(type: "INTEGER", nullable: true),
                    ConvenioMarcoId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivosAdjuntos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArchivosAdjuntos_ConveniosEspecificos_ConvenioEspecificoId",
                        column: x => x.ConvenioEspecificoId,
                        principalTable: "ConveniosEspecificos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ArchivosAdjuntos_ConveniosMarcos_ConvenioMarcoId",
                        column: x => x.ConvenioMarcoId,
                        principalTable: "ConveniosMarcos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CarrerasConvenioEspecifico",
                columns: table => new
                {
                    CarreraId = table.Column<int>(type: "INTEGER", nullable: false),
                    ConvenioEspecificoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrerasConvenioEspecifico", x => new { x.CarreraId, x.ConvenioEspecificoId });
                    table.ForeignKey(
                        name: "FK_CarrerasConvenioEspecifico_Carreras_CarreraId",
                        column: x => x.CarreraId,
                        principalTable: "Carreras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarrerasConvenioEspecifico_ConveniosEspecificos_ConvenioEspecificoId",
                        column: x => x.ConvenioEspecificoId,
                        principalTable: "ConveniosEspecificos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvolucradosConvenioEspecifico",
                columns: table => new
                {
                    ConvenioEspecificoId = table.Column<int>(type: "INTEGER", nullable: false),
                    InvolucradosId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvolucradosConvenioEspecifico", x => new { x.ConvenioEspecificoId, x.InvolucradosId });
                    table.ForeignKey(
                        name: "FK_InvolucradosConvenioEspecifico_ConveniosEspecificos_ConvenioEspecificoId",
                        column: x => x.ConvenioEspecificoId,
                        principalTable: "ConveniosEspecificos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvolucradosConvenioEspecifico_Involucrados_InvolucradosId",
                        column: x => x.InvolucradosId,
                        principalTable: "Involucrados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Carreras",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Ingeniería Química" },
                    { 2, "Ingeniería en Sistemas" },
                    { 3, "Ingeniería Electrónica" },
                    { 4, "Ingeniería Electromecánica" },
                    { 5, "Tecnicatura en Programación" },
                    { 6, "Materias Basicas" },
                    { 7, "SEU" },
                    { 8, "Vinculación Tecnológica" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArchivosAdjuntos_ConvenioEspecificoId",
                table: "ArchivosAdjuntos",
                column: "ConvenioEspecificoId");

            migrationBuilder.CreateIndex(
                name: "IX_ArchivosAdjuntos_ConvenioMarcoId",
                table: "ArchivosAdjuntos",
                column: "ConvenioMarcoId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrerasConvenioEspecifico_ConvenioEspecificoId",
                table: "CarrerasConvenioEspecifico",
                column: "ConvenioEspecificoId");

            migrationBuilder.CreateIndex(
                name: "IX_ConveniosEspecificos_ConvenioMarcoId",
                table: "ConveniosEspecificos",
                column: "ConvenioMarcoId");

            migrationBuilder.CreateIndex(
                name: "IX_ConveniosEspecificos_EmpresaId",
                table: "ConveniosEspecificos",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_ConveniosMarcos_EmpresaId",
                table: "ConveniosMarcos",
                column: "EmpresaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Involucrados_CarrerasId",
                table: "Involucrados",
                column: "CarrerasId");

            migrationBuilder.CreateIndex(
                name: "IX_Involucrados_IdCarrera",
                table: "Involucrados",
                column: "IdCarrera");

            migrationBuilder.CreateIndex(
                name: "IX_InvolucradosConvenioEspecifico_InvolucradosId",
                table: "InvolucradosConvenioEspecifico",
                column: "InvolucradosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchivosAdjuntos");

            migrationBuilder.DropTable(
                name: "CarrerasConvenioEspecifico");

            migrationBuilder.DropTable(
                name: "InvolucradosConvenioEspecifico");

            migrationBuilder.DropTable(
                name: "ConveniosEspecificos");

            migrationBuilder.DropTable(
                name: "Involucrados");

            migrationBuilder.DropTable(
                name: "ConveniosMarcos");

            migrationBuilder.DropTable(
                name: "Carreras");

            migrationBuilder.DropTable(
                name: "Empresas");
        }
    }
}
