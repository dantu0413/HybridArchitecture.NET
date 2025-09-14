using Domain.Entities;

namespace Application.Repositories
{
    public interface IAutomovilRepository
    {
        Task<Automovil?> GetByIdAsync(int id);
        Task<Automovil?> GetByChasisAsync(string chasis);
        Task<List<Automovil>> GetAllAsync();
        Task AddAsync(Automovil auto);
        Task UpdateAsync(Automovil auto);
        Task DeleteAsync(int id);

        Task<bool> ExistsMotorAsync(string numeroMotor, int? excludeId = null);
        Task<bool> ExistsChasisAsync(string numeroChasis, int? excludeId = null);
    }
}
