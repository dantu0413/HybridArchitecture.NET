using Application.Repositories;
using Domain.Entities;
using Infrastructure.Factories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Sql
{
    public class AutomovilRepository : IAutomovilRepository
    {
        private readonly AutomovilDbContext _db;
        public AutomovilRepository(AutomovilDbContext db) => _db = db;

        public Task<Automovil?> GetByIdAsync(int id) => _db.Automoviles.FirstOrDefaultAsync(x => x.Id == id);
        public Task<Automovil?> GetByChasisAsync(string chasis) => _db.Automoviles.FirstOrDefaultAsync(x => x.NumeroChasis == chasis);
        public Task<List<Automovil>> GetAllAsync() => _db.Automoviles.AsNoTracking().ToListAsync();

        public async Task AddAsync(Automovil a) { _db.Automoviles.Add(a); await _db.SaveChangesAsync(); }
        public async Task UpdateAsync(Automovil a) { _db.Automoviles.Update(a); await _db.SaveChangesAsync(); }
        public async Task DeleteAsync(int id) { var e = await GetByIdAsync(id); if (e is null) return; _db.Remove(e); await _db.SaveChangesAsync(); }

        public Task<bool> ExistsMotorAsync(string n, int? excludeId = null)
            => _db.Automoviles.AnyAsync(x => x.NumeroMotor == n && (excludeId == null || x.Id != excludeId));
        public Task<bool> ExistsChasisAsync(string n, int? excludeId = null)
            => _db.Automoviles.AnyAsync(x => x.NumeroChasis == n && (excludeId == null || x.Id != excludeId));
    }
}
