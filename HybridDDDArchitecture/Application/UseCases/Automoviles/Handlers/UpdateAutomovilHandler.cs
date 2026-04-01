using Application.DataTransferObjects;
using Application.Repositories;
using Application.UseCases.Automoviles.Commands;

namespace Application.UseCases.Automoviles.Handlers
{
    /// <summary>
    /// Handler del caso de uso: actualizar un Automóvil (PUT /api/v1/automovil/{id})
    /// Requisitos del parcial:
    /// - 404 si el Id no existe
    /// - Validar unicidad de NumeroMotor y NumeroChasis (409 si colisiona)
    /// - Actualizar y devolver el objeto resultante
    /// 
    /// Convenciones:
    /// - Lanzamos KeyNotFoundException -> la API la mapeará a 404
    /// - Lanzamos InvalidOperationException("NUMERO_*_DUPLICADO") -> 409
    /// </summary>
    public class UpdateAutomovilHandler
    {
        private readonly IAutomovilRepository _repo;

        public UpdateAutomovilHandler(IAutomovilRepository repo)
        {
            _repo = repo;
        }

        public async Task<AutomovilResultDto> Handle(UpdateAutomovilCommand cmd)
        {
            var id = cmd.Id;
            var dto = cmd.Dto;

            // 1) Traer entidad
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null)
                throw new KeyNotFoundException("AUTOMOVIL_NO_ENCONTRADO");

            // 2) Unicidad (si vienen cambios en motor/chasis)
            if (!string.IsNullOrWhiteSpace(dto.NumeroMotor) &&
                await _repo.ExistsMotorAsync(dto.NumeroMotor!, excludeId: id))
                throw new InvalidOperationException("NUMERO_MOTOR_DUPLICADO");

            if (!string.IsNullOrWhiteSpace(dto.NumeroChasis) &&
                await _repo.ExistsChasisAsync(dto.NumeroChasis!, excludeId: id))
                throw new InvalidOperationException("NUMERO_CHASIS_DUPLICADO");

            // 3) Actualizar valores (la entidad revalida reglas básicas)
            entity.Update(
                color: dto.Color,
                numeroMotor: dto.NumeroMotor,
                numeroChasis: dto.NumeroChasis,
                marca: dto.Marca,
                modelo: dto.Modelo,
                fabricacion: dto.Fabricacion
            );

            // 4) Persistir
            await _repo.UpdateAsync(entity);

            // 5) Devolver DTO de resultado
            return new AutomovilResultDto(
                entity.Id, entity.Marca, entity.Modelo, entity.Color,
                entity.Fabricacion, entity.NumeroMotor, entity.NumeroChasis
            );
        }
    }
}
