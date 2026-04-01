using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Infrastructure.Factories
{
    /// <summary>
    /// DbContext de Automóviles.
    /// Define el DbSet y la configuración de EF Core (índices únicos incluidos).
    /// </summary>
    public class AutomovilDbContext : DbContext
    {
        public AutomovilDbContext(DbContextOptions<AutomovilDbContext> options) : base(options) { }

        // ¡OJO con el nombre exacto! Usaremos Automoviles (A mayúscula).
        public DbSet<Automovil> Automoviles => Set<Automovil>();

        protected override void OnModelCreating(ModelBuilder mb)
        {
            var e = mb.Entity<Automovil>();

            e.HasKey(x => x.Id);
            e.Property(x => x.Id).ValueGeneratedOnAdd();

            e.Property(x => x.Marca).IsRequired();
            e.Property(x => x.Modelo).IsRequired();
            e.Property(x => x.Color).IsRequired();
            e.Property(x => x.Fabricacion).IsRequired();

            e.Property(x => x.NumeroMotor).IsRequired();
            e.Property(x => x.NumeroChasis).IsRequired();

            // Reglas de unicidad exigidas por el parcial
            e.HasIndex(x => x.NumeroMotor).IsUnique();
            e.HasIndex(x => x.NumeroChasis).IsUnique();

            base.OnModelCreating(mb);
        }
    }
}
