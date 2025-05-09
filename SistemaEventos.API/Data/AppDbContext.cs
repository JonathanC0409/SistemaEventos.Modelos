using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaEventos.Modelos;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<SistemaEventos.Modelos.Certificado> Certificados { get; set; } = default!;

    public DbSet<SistemaEventos.Modelos.Evento> Eventos { get; set; } = default!;

    public DbSet<SistemaEventos.Modelos.Inscripcion> Inscripciones { get; set; } = default!;

    public DbSet<SistemaEventos.Modelos.Participante> Participantes { get; set; } = default!;

    public DbSet<SistemaEventos.Modelos.Ponente> Ponentes { get; set; } = default!;

    public DbSet<SistemaEventos.Modelos.RegistroPago> RegistroPagos { get; set; } = default!;

    public DbSet<SistemaEventos.Modelos.Sesion> Sesiones { get; set; } = default!;
}
