    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    #nullable disable

    namespace SistemaEventos.API.Migrations
    {
        /// <inheritdoc />
        public partial class v01 : Migration
        {
            /// <inheritdoc />
            protected override void Up(MigrationBuilder migrationBuilder)
            {
                migrationBuilder.CreateTable(
                    name: "Participantes",
                    columns: table => new
                    {
                        Codigo = table.Column<int>(type: "NUMBER(10)", nullable: false)
                            .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                        Nombre = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                        Facultad = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                        Carrera = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                        Nivel = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                        AsistenciaCompleta = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Participantes", x => x.Codigo);
                    });

                migrationBuilder.CreateTable(
                    name: "Ponentes",
                    columns: table => new
                    {
                        Codigo = table.Column<int>(type: "NUMBER(10)", nullable: false)
                            .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                        Nombre = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                        Apellido = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                        Pais = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Ponentes", x => x.Codigo);
                    });

                migrationBuilder.CreateTable(
                    name: "Certificados",
                    columns: table => new
                    {
                        Codigo = table.Column<int>(type: "NUMBER(10)", nullable: false)
                            .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                        Nombre = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                        TipoCertificado = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                        ParticipanteCodigo = table.Column<int>(type: "NUMBER(10)", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Certificados", x => x.Codigo);
                        table.ForeignKey(
                            name: "FK_Certificados_Participantes_ParticipanteCodigo",
                            column: x => x.ParticipanteCodigo,
                            principalTable: "Participantes",
                            principalColumn: "Codigo",
                            onDelete: ReferentialAction.Cascade);
                    });

                migrationBuilder.CreateTable(
                    name: "Inscripciones",
                    columns: table => new
                    {
                        Codigo = table.Column<int>(type: "NUMBER(10)", nullable: false)
                            .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                        Pago = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                        Estado = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                        FechaInscripcion = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                        ParticipanteCodigo = table.Column<int>(type: "NUMBER(10)", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Inscripciones", x => x.Codigo);
                        table.ForeignKey(
                            name: "FK_Inscripciones_Participantes_ParticipanteCodigo",
                            column: x => x.ParticipanteCodigo,
                            principalTable: "Participantes",
                            principalColumn: "Codigo",
                            onDelete: ReferentialAction.Cascade);
                    });

                migrationBuilder.CreateTable(
                    name: "Eventos",
                    columns: table => new
                    {
                        Codigo = table.Column<int>(type: "NUMBER(10)", nullable: false)
                            .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                        Nombre = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                        Fecha = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                        Lugar = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                        Tipo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                        PonenteCodigo = table.Column<int>(type: "NUMBER(10)", nullable: false),
                        ParticipanteCodigo = table.Column<int>(type: "NUMBER(10)", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Eventos", x => x.Codigo);
                        table.ForeignKey(
                            name: "FK_Eventos_Participantes_ParticipanteCodigo",
                            column: x => x.ParticipanteCodigo,
                            principalTable: "Participantes",
                            principalColumn: "Codigo",
                            onDelete: ReferentialAction.Cascade);
                        table.ForeignKey(
                            name: "FK_Eventos_Ponentes_PonenteCodigo",
                            column: x => x.PonenteCodigo,
                            principalTable: "Ponentes",
                            principalColumn: "Codigo",
                            onDelete: ReferentialAction.Cascade);
                    });

                migrationBuilder.CreateTable(
                    name: "RegistroPagos",
                    columns: table => new
                    {
                        Codigo = table.Column<int>(type: "NUMBER(10)", nullable: false)
                            .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                        Nombre = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                        TipoPago = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                        Monto = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                        InscripcionCodigo = table.Column<int>(type: "NUMBER(10)", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_RegistroPagos", x => x.Codigo);
                        table.ForeignKey(
                            name: "FK_RegistroPagos_Inscripciones_InscripcionCodigo",
                            column: x => x.InscripcionCodigo,
                            principalTable: "Inscripciones",
                            principalColumn: "Codigo",
                            onDelete: ReferentialAction.Cascade);
                    });

                migrationBuilder.CreateTable(
                    name: "Sesiones",
                    columns: table => new
                    {
                        Codigo = table.Column<int>(type: "NUMBER(10)", nullable: false)
                            .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                        Nombre = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                        HorarioInicio = table.Column<int>(type: "NUMBER(10)", nullable: false),
                        HorarioFin = table.Column<int>(type: "NUMBER(10)", nullable: false),
                        Sala = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                        EventoCodigo = table.Column<int>(type: "NUMBER(10)", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Sesiones", x => x.Codigo);
                        table.ForeignKey(
                            name: "FK_Sesiones_Eventos_EventoCodigo",
                            column: x => x.EventoCodigo,
                            principalTable: "Eventos",
                            principalColumn: "Codigo",
                            onDelete: ReferentialAction.Cascade);
                    });

                migrationBuilder.CreateIndex(
                    name: "IX_Certificados_ParticipanteCodigo",
                    table: "Certificados",
                    column: "ParticipanteCodigo");

                migrationBuilder.CreateIndex(
                    name: "IX_Eventos_ParticipanteCodigo",
                    table: "Eventos",
                    column: "ParticipanteCodigo");

                migrationBuilder.CreateIndex(
                    name: "IX_Eventos_PonenteCodigo",
                    table: "Eventos",
                    column: "PonenteCodigo");

                migrationBuilder.CreateIndex(
                    name: "IX_Inscripciones_ParticipanteCodigo",
                    table: "Inscripciones",
                    column: "ParticipanteCodigo");

                migrationBuilder.CreateIndex(
                    name: "IX_RegistroPagos_InscripcionCodigo",
                    table: "RegistroPagos",
                    column: "InscripcionCodigo");

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
                    name: "RegistroPagos");

                migrationBuilder.DropTable(
                    name: "Sesiones");

                migrationBuilder.DropTable(
                    name: "Inscripciones");

                migrationBuilder.DropTable(
                    name: "Eventos");

                migrationBuilder.DropTable(
                    name: "Participantes");

                migrationBuilder.DropTable(
                    name: "Ponentes");
            }
        }
    }
