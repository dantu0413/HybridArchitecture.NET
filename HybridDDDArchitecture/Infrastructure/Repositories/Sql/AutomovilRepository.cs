using Application.Repositories;
using Domain.Entities;
using Infrastructure.Factories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Sql
{
    /// <summary>
    /// Implementación EF Core del repositorio de Automóviles.
    /// Cumple el contrato de Application.Repositories.IAutomovilRepository.
    /// </summary>
    public class AutomovilRepository : IAutomovilRepository
    {
        private readonly AutomovilDbContext _db;

        public AutomovilRepository(AutomovilDbContext db)
        {
            _db = db;
        }

        // ---- Lectura ----
        public Task<Automovil?> GetByIdAsync(int id)
            => _db.Automoviles.FirstOrDefaultAsync(x => x.Id == id);

        public Task<Automovil?> GetByChasisAsync(string numeroChasis)
            => _db.Automoviles.FirstOrDefaultAsync(x => x.NumeroChasis == numeroChasis);

        public Task<List<Automovil>> GetAllAsync()
            => _db.Automoviles.AsNoTracking().ToListAsync();

        // ---- Escritura ----
        public async Task AddAsync(Automovil automovil)
        {
            _db.Automoviles.Add(automovil);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Automovil automovil)
        {
            _db.Automoviles.Update(automovil);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity is null) return; // el handler decide si 404
            _db.Automoviles.Remove(entity);
            await _db.SaveChangesAsync();
        }

        // ---- Unicidad (soporte a reglas del parcial) ----
        public Task<bool> ExistsMotorAsync(string numeroMotor, int? excludeId = null)
            => _db.Automoviles.AnyAsync(x => x.NumeroMotor == numeroMotor && (excludeId == null || x.Id != excludeId));

        public Task<bool> ExistsChasisAsync(string numeroChasis, int? excludeId = null)
            => _db.Automoviles.AnyAsync(x => x.NumeroChasis == numeroChasis && (excludeId == null || x.Id != excludeId));
    }
}
