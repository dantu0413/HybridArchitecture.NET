using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Factories
{
    public class AutomovilDbContext : DbContext
    {
        public AutomovilDbContext(DbContextOptions<AutomovilDbContext> options) : base(options) { }

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

            e.HasIndex(x => x.NumeroMotor).IsUnique();
            e.HasIndex(x => x.NumeroChasis).IsUnique();
        }
    }
}
