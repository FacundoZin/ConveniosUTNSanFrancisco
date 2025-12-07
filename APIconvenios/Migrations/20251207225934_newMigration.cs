using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIconvenios.Migrations
{
    /// <inheritdoc />
    public partial class newMigration : Migration
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
                    IdArea = table.Column<int>(type: "INTEGER", nullable: true),
                    AreaId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Involucrados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Involucrados_Carreras_AreaId",
                        column: x => x.AreaId,
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

            migrationBuilder.InsertData(
                table: "Empresas",
                columns: new[] { "Id", "ConvenioMarcoId", "Cuit", "Direccion", "Email", "Nombre", "RazonSocial", "Telefono" },
                values: new object[,]
                {
                    { 1, null, "30-12345678-9", "Av. Tecnologica 123", "contacto@techsolutions.com", "Tech Solutions S.A.", "Tech Solutions S.A.", "3564112233" },
                    { 2, null, "30-87654321-0", "Calle Innovacion 456", "info@innovacion.com", "Innovación Digital S.R.L.", "Innovación Digital S.R.L.", "3564445566" },
                    { 3, null, "30-11223344-5", "Bv. Industrial 789", "contacto@electronica.com", "Electrónica Avanzada S.A.", "Electrónica Avanzada S.A.", "3564223344" },
                    { 4, null, "30-99887766-1", "Parque Industrial 321", "info@quimica.com", "Química Industrial S.R.L.", "Química Industrial Soc. Resp. Limitada", "3564556677" },
                    { 5, null, "30-55667788-2", "Av. Libertad 555", "ventas@mecatronica.com", "Mecatrónica del Centro", "Mecatrónica del Centro S.A.", "3564889900" },
                    { 6, null, "30-44556677-3", "San Martin 888", "contacto@consultora.edu", "Consultora Educativa", "Consultora Educativa y Capacitación", "3564112244" }
                });

            migrationBuilder.InsertData(
                table: "Involucrados",
                columns: new[] { "Id", "Apellido", "AreaId", "Email", "IdArea", "Legajo", "Nombre", "RolInvolucrado", "Telefono" },
                values: new object[,]
                {
                    { 1, "Perez", null, "juan.perez@email.com", null, 12345, "Juan", 1, "3564998877" },
                    { 2, "Martinez", null, "sofia.martinez@email.com", null, 12346, "Sofia", 1, "3564887766" },
                    { 3, "Rodriguez", null, "lucas.rodriguez@email.com", null, 12347, "Lucas", 1, "3564776655" },
                    { 4, "Gomez", null, "maria.gomez@email.com", null, 67890, "Maria", 0, "3564776655" },
                    { 5, "Fernandez", null, "carlos.fernandez@utn.edu.ar", null, 67891, "Carlos", 0, "3564665544" },
                    { 6, "Lopez", null, "ana.lopez@utn.edu.ar", null, 67892, "Ana", 0, "3564554433" },
                    { 7, "Sanchez", null, "roberto.sanchez@utn.edu.ar", null, 89001, "Roberto", 2, "3564443322" },
                    { 8, "Diaz", null, "laura.diaz@utn.edu.ar", null, 89002, "Laura", 2, "3564332211" },
                    { 9, "Morales", null, "pedro.morales@techsolutions.com", null, null, "Pedro", 3, "3564221100" },
                    { 10, "Torres", null, "gabriela.torres@innovacion.com", null, null, "Gabriela", 3, "3564110099" },
                    { 11, "Ruiz", null, "miguel.ruiz@electronica.com", null, null, "Miguel", 3, "3564009988" }
                });

            migrationBuilder.InsertData(
                table: "ConveniosEspecificos",
                columns: new[] { "Id", "ComentarioOpcional", "ConvenioMarcoId", "EmpresaId", "EsActa", "Estado", "FechaFinConvenio", "FechaFirmaConvenio", "FechaInicioActividades", "NumeroResolucion", "Refrendado", "TituloConvenio", "numeroconvenio" },
                values: new object[,]
                {
                    { 4, null, null, 6, false, 1, new DateOnly(2024, 7, 15), new DateOnly(2024, 5, 15), new DateOnly(2024, 5, 20), "RES-2024-035", true, "Capacitación SEU - Seguridad Laboral", "CE-004/2024" },
                    { 5, null, null, 4, false, 1, new DateOnly(2024, 8, 15), new DateOnly(2024, 2, 10), new DateOnly(2024, 2, 15), "RES-2024-008", true, "Prácticas Profesionales Química", "CE-005/2024" },
                    { 9, "En negociación", null, 6, false, 0, null, null, null, null, false, "Futuro Convenio Materias Básicas", "CE-007/2024" }
                });

            migrationBuilder.InsertData(
                table: "ConveniosMarcos",
                columns: new[] { "Id", "ComentarioOpcional", "EmpresaId", "Estado", "FechaFin", "FechaFirmaConvenio", "NumeroResolucion", "Refrendado", "Titulo", "numeroconvenio" },
                values: new object[,]
                {
                    { 1, "Convenio vigente principal para actividades IT", 1, 1, new DateOnly(2026, 1, 15), new DateOnly(2024, 1, 15), "RES-2024-001", true, "Convenio Marco con Tech Solutions", "CM-001/2024" },
                    { 2, null, 2, 1, new DateOnly(2025, 2, 20), new DateOnly(2024, 2, 20), null, false, "Convenio Marco con Innovación Digital", "CM-002/2024" },
                    { 3, "Convenio finalizado exitosamente", 3, 2, new DateOnly(2024, 5, 10), new DateOnly(2023, 5, 10), "RES-2023-045", true, "Convenio Marco Electrónica - Finalizado", "CM-003/2023" },
                    { 4, "En proceso de revisión legal", 4, 0, null, null, null, false, "Convenio Marco Química Industrial - Borrador", "CM-004/2024" },
                    { 5, null, 5, 1, new DateOnly(2027, 6, 1), new DateOnly(2024, 6, 1), "RES-2024-078", true, "Convenio Marco Mecatrónica", "CM-005/2024" }
                });

            migrationBuilder.InsertData(
                table: "ArchivosAdjuntos",
                columns: new[] { "Id", "ContentType", "ConvenioEspecificoId", "ConvenioMarcoId", "NombreArchivo", "RutaArchivo" },
                values: new object[,]
                {
                    { 5, "application/pdf", 4, null, "programa_capacitacion.pdf", "/uploads/especificos/programa_capacitacion.pdf" },
                    { 6, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", 5, null, "cronograma_actividades.xlsx", "/uploads/especificos/cronograma.xlsx" },
                    { 8, "application/pdf", null, 1, "convenio_marco_tech.pdf", "/uploads/marcos/cm_tech.pdf" },
                    { 9, "application/pdf", null, 1, "resolucion_rectoral_001.pdf", "/uploads/marcos/res_001.pdf" },
                    { 10, "application/pdf", null, 2, "convenio_marco_innovacion.pdf", "/uploads/marcos/cm_innovacion.pdf" },
                    { 11, "application/pdf", null, 3, "convenio_marco_electronica.pdf", "/uploads/marcos/cm_electronica.pdf" },
                    { 12, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", null, 4, "borrador_convenio_quimica.docx", "/uploads/marcos/borrador_quimica.docx" }
                });

            migrationBuilder.InsertData(
                table: "CarrerasConvenioEspecifico",
                columns: new[] { "CarreraId", "ConvenioEspecificoId" },
                values: new object[,]
                {
                    { 1, 4 },
                    { 1, 5 },
                    { 2, 4 },
                    { 3, 4 },
                    { 4, 4 },
                    { 6, 5 },
                    { 6, 9 },
                    { 7, 4 }
                });

            migrationBuilder.InsertData(
                table: "ConveniosEspecificos",
                columns: new[] { "Id", "ComentarioOpcional", "ConvenioMarcoId", "EmpresaId", "EsActa", "Estado", "FechaFinConvenio", "FechaFirmaConvenio", "FechaInicioActividades", "NumeroResolucion", "Refrendado", "TituloConvenio", "numeroconvenio" },
                values: new object[,]
                {
                    { 1, "Pasantías rentadas para alumnos avanzados", 1, 1, false, 1, new DateOnly(2024, 12, 31), new DateOnly(2024, 3, 1), new DateOnly(2024, 3, 10), "RES-2024-015", true, "Pasantías 2024 - Sistemas y Programación", "CE-001/2024" },
                    { 2, "Proyecto de investigación en IA", 2, 2, true, 0, new DateOnly(2024, 10, 30), new DateOnly(2024, 4, 5), new DateOnly(2024, 4, 10), null, false, "Investigación Conjunta AI", "CE-002/2024" },
                    { 3, null, 3, 3, false, 1, new DateOnly(2024, 11, 30), new DateOnly(2024, 3, 20), new DateOnly(2024, 4, 1), "RES-2024-022", true, "Desarrollo de Prototipos Electrónicos", "CE-003/2024" },
                    { 6, "Proyecto finalizado con éxito", 3, 3, false, 2, new DateOnly(2023, 12, 20), new DateOnly(2023, 8, 1), new DateOnly(2023, 8, 10), "RES-2023-089", true, "Proyecto Final Integrador 2023", "CE-006/2023" },
                    { 7, null, 1, 1, true, 1, new DateOnly(2025, 7, 1), new DateOnly(2024, 7, 1), new DateOnly(2024, 7, 5), "RES-2024-055", true, "Acta Acuerdo Vinculación Tecnológica", "ACTA-001/2024" },
                    { 8, null, 5, 5, true, 1, new DateOnly(2024, 12, 31), new DateOnly(2024, 6, 10), new DateOnly(2024, 6, 15), null, false, "Acta Donación Equipamiento", "ACTA-002/2024" }
                });

            migrationBuilder.InsertData(
                table: "InvolucradosConvenioEspecifico",
                columns: new[] { "ConvenioEspecificoId", "InvolucradosId" },
                values: new object[,]
                {
                    { 4, 5 },
                    { 4, 7 },
                    { 4, 8 },
                    { 5, 1 },
                    { 5, 6 },
                    { 9, 8 }
                });

            migrationBuilder.InsertData(
                table: "ArchivosAdjuntos",
                columns: new[] { "Id", "ContentType", "ConvenioEspecificoId", "ConvenioMarcoId", "NombreArchivo", "RutaArchivo" },
                values: new object[,]
                {
                    { 1, "application/pdf", 1, null, "plan_trabajo.pdf", "/uploads/especificos/plan_trabajo.pdf" },
                    { 2, "application/pdf", 1, null, "acuerdo_confidencialidad.pdf", "/uploads/especificos/acuerdo_confidencialidad.pdf" },
                    { 3, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", 2, null, "proyecto_investigacion_ia.docx", "/uploads/especificos/proyecto_ia.docx" },
                    { 4, "application/pdf", 3, null, "especificaciones_tecnicas.pdf", "/uploads/especificos/especificaciones.pdf" },
                    { 7, "application/pdf", 6, null, "informe_final_pfi.pdf", "/uploads/especificos/informe_final.pdf" }
                });

            migrationBuilder.InsertData(
                table: "CarrerasConvenioEspecifico",
                columns: new[] { "CarreraId", "ConvenioEspecificoId" },
                values: new object[,]
                {
                    { 1, 8 },
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 7 },
                    { 2, 8 },
                    { 3, 3 },
                    { 3, 6 },
                    { 3, 8 },
                    { 4, 3 },
                    { 4, 8 },
                    { 5, 1 },
                    { 8, 7 }
                });

            migrationBuilder.InsertData(
                table: "InvolucradosConvenioEspecifico",
                columns: new[] { "ConvenioEspecificoId", "InvolucradosId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 4 },
                    { 1, 9 },
                    { 2, 4 },
                    { 2, 5 },
                    { 2, 10 },
                    { 3, 3 },
                    { 3, 6 },
                    { 3, 11 },
                    { 6, 3 },
                    { 6, 6 },
                    { 7, 5 },
                    { 7, 7 },
                    { 7, 9 },
                    { 8, 7 },
                    { 8, 8 }
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
                name: "IX_Involucrados_AreaId",
                table: "Involucrados",
                column: "AreaId");

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
