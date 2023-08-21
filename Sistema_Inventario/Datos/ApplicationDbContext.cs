using Microsoft.EntityFrameworkCore;
using Sistema_Inventario.Models;
using System;

namespace Sistema_Inventario.Datos
{
    public class ApplicationDbContext
    {
        public class ApplicationDbContexto : DbContext
        {
            public ApplicationDbContexto(DbContextOptions<ApplicationDbContexto> options) : base(options)
            {
            }
            public DbSet<Categoria> categorias { get; set; }
            public DbSet<Producto> productos { get; set; }
            public DbSet<tbProvincia> tbProvincias { get; set; }
            public DbSet<tbDistrito> tbDistritos { get; set; }
            public DbSet<tbCanton> tbCantones { get; set; }
            public DbSet<tbBarrios> tbBarrios { get; set; }
            public DbSet<Cliente> Clientes { get; set; }
            public DbSet<Persona> Personas { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<tbProvincia>()
                    .HasKey(p => p.Cod);

                modelBuilder.Entity<tbCanton>()
                    .HasKey(c => new { c.ProvinciaID, c.Canton });

                modelBuilder.Entity<tbDistrito>()
                    .HasKey(d => new { d.ProvinciaID, d.CantonID, d.Distrito });

                modelBuilder.Entity<tbBarrios>()
                    .HasKey(b => new { b.ProvinciaID, b.CantonID, b.DistritoID, b.Barrio });

                modelBuilder.Entity<tbDistrito>()
                    .HasOne(d => d.Canton)
                    .WithMany(c => c.Distritos)
                    .HasForeignKey(d => new { d.ProvinciaID, d.CantonID });

                modelBuilder.Entity<tbBarrios>()
                    .HasOne(b => b.Distrito)
                    .WithMany(d => d.Barrios)
                    .HasForeignKey(b => new { b.ProvinciaID, b.CantonID, b.DistritoID });

                modelBuilder.Entity<Cliente>().ToTable("Cliente");

            }
        }
    }

}


