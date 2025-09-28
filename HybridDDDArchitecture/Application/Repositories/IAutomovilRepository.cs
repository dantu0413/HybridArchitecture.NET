using Domain.Entities;

namespace Application.Repositories
{
    /// <summary>
    /// Contrato de acceso a datos para Automovil.
    /// Se usa desde la Capa de Aplicación (Handlers/UseCases).
    /// La implementación concreta vive en Infraestructura (EF Core).
    /// </summary>
    public interface IAutomovilRepository
    {
        // Lectura
        Task<Automovil?> GetByIdAsync(int id);
        Task<Automovil?> GetByChasisAsync(string numeroChasis);
        Task<List<Automovil>> GetAllAsync();

        // Escritura
        Task AddAsync(Automovil automovil);
        Task UpdateAsync(Automovil automovil);
        Task DeleteAsync(int id);

        // Soporte de reglas (unicidad exigida por el parcial)
        Task<bool> ExistsMotorAsync(string numeroMotor, int? excludeId = null);
        Task<bool> ExistsChasisAsync(string numeroChasis, int? excludeId = null);
    }
}
