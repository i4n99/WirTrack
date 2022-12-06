using Microsoft.EntityFrameworkCore;
using Dominio;

namespace Persistencia
{
    public class Context : DbContext
    {

        public Context(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<Viajes> Viajes { get; set; }
        public virtual DbSet<Provincias> Provincias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Viajes>(entity =>
            {
                entity.HasKey(e => e.IdViaje);

                entity.ToTable("Viajes", "dbo");

                entity.Property(e => e.IdViaje).HasColumnName("IdViaje");

                entity.Property(e => e.IdCiudad)
                    .IsRequired()
                    .HasColumnName("IdCiudad");

                entity.Property(e => e.IdTipoVehiculos)
                    .IsRequired()
                    .HasColumnName("IdTipoVehiculos")
                    .HasColumnType("text")
                    .HasMaxLength(30);

                entity.Property(e => e.Fecha)
                    .IsRequired()
                    .HasColumnName("Fecha")
                    .HasColumnType("date");

                entity.Property(e => e.NombreReserva)
                    .IsRequired()
                    .HasColumnName("NombreReserva");
            });

            modelBuilder.Entity<Provincias>(entity =>
            {
                entity.HasKey(e => e.IdProvincia);

                entity.ToTable("Provincias", "dbo");

                entity.Property(e => e.IdProvincia).HasColumnName("IdProvincia");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("Descripcion");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
