using Microsoft.EntityFrameworkCore;
using Dominio;

namespace Persistencia
{
    public class Context : DbContext
    {

        public Context(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<Ciudades> Ciudades { get; set; }
        public virtual DbSet<Provincias> Provincias { get; set; }
        public virtual DbSet<Vehiculos> Vehiculos { get; set; }
        public virtual DbSet<VehiculosTipoVehiculo> VehiculosTipoVehiculo { get; set; }
        public virtual DbSet<Viajes> Viajes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ciudades>(entity =>
            {
                entity.HasKey(e => e.IdCiudad);

                entity.ToTable("Ciudades", "dbo");

                entity.Property(e => e.IdCiudad).HasColumnName("IdCiudad");

                entity.Property(e => e.Ciudad)
                    .IsRequired()
                    .HasColumnName("Ciudad");
                
                entity.Property(e => e.IdOpenWeather).HasColumnName("IdOpenWeather");

                entity.Property(e => e.Latitud).HasColumnName("Latitud");

                entity.Property(e => e.Longitud).HasColumnName("Longitud");

                entity.Property(e => e.Descripcion).HasColumnName("Descripcion");

                entity.HasOne(d => d.Provincia)
                    .WithMany(p => p.Ciudades)
                    .HasForeignKey(d => d.IdProvincia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Provincias_Ciudades");
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

            modelBuilder.Entity<Vehiculos>(entity =>
            {
                entity.HasKey(e => e.IdVehiculo);

                entity.ToTable("Vehiculos", "dbo");

                entity.Property(e => e.IdVehiculo).HasColumnName("IdVehiculo");

                entity.Property(e => e.Patente)
                    .IsRequired()
                    .HasMaxLength(7)
                    .HasColumnName("Patente");
                
                entity.Property(e => e.Marca)
                    .IsRequired()
                    .HasColumnName("Marca");

                entity.Property(e => e.Modelo)
                    .IsRequired()
                    .HasColumnName("Modelo");

                entity.Property(e => e.Comentarios).HasColumnName("Comentarios");

                entity.HasOne(d => d.TipoVehiculo)
                    .WithMany(p => p.Vehiculos)
                    .HasForeignKey(d => d.IdTipoVehiculo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehiculos_VehiculosTipoVehiculo");
            });

            modelBuilder.Entity<VehiculosTipoVehiculo>(entity =>
            {
                entity.HasKey(e => e.IdTipoVehiculo);

                entity.ToTable("VehiculosTipoVehiculo", "dbo");

                entity.Property(e => e.IdTipoVehiculo).HasColumnName("IdTipoVehiculo");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("Descripcion");
            });

            modelBuilder.Entity<Viajes>(entity =>
            {
                entity.HasKey(e => e.IdViaje);

                entity.ToTable("Viajes", "dbo");

                entity.Property(e => e.IdViaje).HasColumnName("IdViaje");

                entity.Property(e => e.IdsVehiculos)
                    .IsRequired()
                    .HasColumnName("IdsVehiculos")
                    .HasMaxLength(30);

                entity.Property(e => e.Fecha)
                    .IsRequired()
                    .HasColumnName("Fecha")
                    .HasColumnType("date");

                entity.Property(e => e.NombreReserva)
                    .IsRequired()
                    .HasColumnName("NombreReserva");

                entity.HasOne(d => d.Ciudad)
                    .WithMany(p => p.Viajes)
                    .HasForeignKey(d => d.IdCiudad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Viajes_Ciudades");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
