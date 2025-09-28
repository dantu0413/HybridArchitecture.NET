using Application.DataTransferObjects;
using Application.Repositories;
using Application.UseCases.Automoviles.Commands;
using Domain.Entities;

namespace Application.UseCases.Automoviles.Handlers
{
    /// <summary>
    /// Handler del caso de uso: crear un Automóvil.
    /// Requisitos del parcial:
    /// - Validar requeridos (los básicos los hace la entidad).
    /// - Garantizar unicidad de NumeroMotor y NumeroChasis (409 si colisiona).
    /// - Persistir y devolver el objeto creado.
    /// 
    /// Nota: El "409" se materializa en la capa API. Aquí lanzamos InvalidOperationException
    /// con un código reconocible; el filtro de la API lo traducirá a 409.
    /// </summary>
    public class CreateAutomovilHandler
    {
        private readonly IAutomovilRepository _repo;

        public CreateAutomovilHandler(IAutomovilRepository repo)
        {
            _repo = repo;
        }

        public async Task<AutomovilResultDto> Handle(CreateAutomovilCommand cmd)
        {
            var d = cmd.Dto;

            // Reglas de unicidad exigidas por el parcial
            if (await _repo.ExistsMotorAsync(d.NumeroMotor))
                throw new InvalidOperationException("NUMERO_MOTOR_DUPLICADO");

            if (await _repo.ExistsChasisAsync(d.NumeroChasis))
                throw new InvalidOperationException("NUMERO_CHASIS_DUPLICADO");

            // La entidad valida requeridos y año
            var entity = new Automovil(
                d.Marca, d.Modelo, d.Color, d.Fabricacion,
                d.NumeroMotor, d.NumeroChasis
            );

            await _repo.AddAsync(entity);

            return new AutomovilResultDto(
                entity.Id, entity.Marca, entity.Modelo, entity.Color,
                entity.Fabricacion, entity.NumeroMotor, entity.NumeroChasis
            );
        }
    }
}
