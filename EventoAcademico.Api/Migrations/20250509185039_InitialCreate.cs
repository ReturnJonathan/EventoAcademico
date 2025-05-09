using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EventoAcademico.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    Lugar = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Participantes",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Apellido = table.Column<string>(type: "text", nullable: false),
                    Institucion = table.Column<string>(type: "text", nullable: false),
                    Correo = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participantes", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Ponentes",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Apellido = table.Column<string>(type: "text", nullable: false),
                    Institucion = table.Column<string>(type: "text", nullable: false),
                    Correo = table.Column<string>(type: "text", nullable: false),
                    CodigoEvento = table.Column<int>(type: "integer", nullable: false),
                    CodigoSesion = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ponentes", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Sesiones",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HorarioInicio = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    HorarioFin = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Sala = table.Column<string>(type: "text", nullable: false),
                    CodigoEvento = table.Column<int>(type: "integer", nullable: false),
                    EventoCodigo = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sesiones", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Sesiones_Eventos_EventoCodigo",
                        column: x => x.EventoCodigo,
                        principalTable: "Eventos",
                        principalColumn: "Codigo");
                });

            migrationBuilder.CreateTable(
                name: "EventoPonente",
                columns: table => new
                {
                    EventosCodigo = table.Column<int>(type: "integer", nullable: false),
                    PonentesCodigo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventoPonente", x => new { x.EventosCodigo, x.PonentesCodigo });
                    table.ForeignKey(
                        name: "FK_EventoPonente_Eventos_EventosCodigo",
                        column: x => x.EventosCodigo,
                        principalTable: "Eventos",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventoPonente_Ponentes_PonentesCodigo",
                        column: x => x.PonentesCodigo,
                        principalTable: "Ponentes",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inscripciones",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Estado = table.Column<bool>(type: "boolean", nullable: false),
                    FechaInscripcion = table.Column<DateOnly>(type: "date", nullable: false),
                    CodigoEvento = table.Column<int>(type: "integer", nullable: false),
                    CodigoParticipante = table.Column<int>(type: "integer", nullable: false),
                    CodigoSesion = table.Column<int>(type: "integer", nullable: false),
                    CodigoPonente = table.Column<int>(type: "integer", nullable: false),
                    ParticipanteCodigo = table.Column<int>(type: "integer", nullable: true),
                    EventoCodigo = table.Column<int>(type: "integer", nullable: true),
                    SesionCodigo = table.Column<int>(type: "integer", nullable: true),
                    PonenteCodigo = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscripciones", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Inscripciones_Eventos_EventoCodigo",
                        column: x => x.EventoCodigo,
                        principalTable: "Eventos",
                        principalColumn: "Codigo");
                    table.ForeignKey(
                        name: "FK_Inscripciones_Participantes_ParticipanteCodigo",
                        column: x => x.ParticipanteCodigo,
                        principalTable: "Participantes",
                        principalColumn: "Codigo");
                    table.ForeignKey(
                        name: "FK_Inscripciones_Ponentes_PonenteCodigo",
                        column: x => x.PonenteCodigo,
                        principalTable: "Ponentes",
                        principalColumn: "Codigo");
                    table.ForeignKey(
                        name: "FK_Inscripciones_Sesiones_SesionCodigo",
                        column: x => x.SesionCodigo,
                        principalTable: "Sesiones",
                        principalColumn: "Codigo");
                });

            migrationBuilder.CreateTable(
                name: "Certificados",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FechaEmision = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Asistencia = table.Column<bool>(type: "boolean", nullable: false),
                    TipoCertificado = table.Column<string>(type: "text", nullable: false),
                    NombreCertificado = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    CodigoInscripcion = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificados", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Certificados_Inscripciones_CodigoInscripcion",
                        column: x => x.CodigoInscripcion,
                        principalTable: "Inscripciones",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MetodoPago = table.Column<string>(type: "text", nullable: false),
                    Monto = table.Column<double>(type: "double precision", nullable: false),
                    Pagado = table.Column<bool>(type: "boolean", nullable: false),
                    CodigoInscripcion = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Pagos_Inscripciones_CodigoInscripcion",
                        column: x => x.CodigoInscripcion,
                        principalTable: "Inscripciones",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Certificados_CodigoInscripcion",
                table: "Certificados",
                column: "CodigoInscripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventoPonente_PonentesCodigo",
                table: "EventoPonente",
                column: "PonentesCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_EventoCodigo",
                table: "Inscripciones",
                column: "EventoCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_ParticipanteCodigo",
                table: "Inscripciones",
                column: "ParticipanteCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_PonenteCodigo",
                table: "Inscripciones",
                column: "PonenteCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_SesionCodigo",
                table: "Inscripciones",
                column: "SesionCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_CodigoInscripcion",
                table: "Pagos",
                column: "CodigoInscripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sesiones_EventoCodigo",
                table: "Sesiones",
                column: "EventoCodigo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Certificados");

            migrationBuilder.DropTable(
                name: "EventoPonente");

            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DropTable(
                name: "Inscripciones");

            migrationBuilder.DropTable(
                name: "Participantes");

            migrationBuilder.DropTable(
                name: "Ponentes");

            migrationBuilder.DropTable(
                name: "Sesiones");

            migrationBuilder.DropTable(
                name: "Eventos");
        }
    }
}
