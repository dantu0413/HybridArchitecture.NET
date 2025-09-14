using Application.DataTransferObjects;
using Application.Repositories;
using Application.UseCases.Automoviles.Commands;
using Domain.Entities;

namespace Application.UseCases.Automoviles.Handlers
{
    public class CreateAutomovilHandler
    {
        private readonly IAutomovilRepository _repo;

        public CreateAutomovilHandler(IAutomovilRepository repo) => _repo = repo;

        public async Task<AutomovilResultDto> Handle(CreateAutomovilCommand cmd)
        {
            var d = cmd.Dto;
            if (await _repo.ExistsMotorAsync(d.NumeroMotor)) throw new InvalidOperationException("NUMERO_MOTOR_DUPLICADO");
            if (await _repo.ExistsChasisAsync(d.NumeroChasis)) throw new InvalidOperationException("NUMERO_CHASIS_DUPLICADO");

            var entity = new Automovil(d.Marca, d.Modelo, d.Color, d.Fabricacion, d.NumeroMotor, d.NumeroChasis);
            await _repo.AddAsync(entity);

            return new AutomovilResultDto(entity.Id, entity.Marca, entity.Modelo, entity.Color, entity.Fabricacion, entity.NumeroMotor, entity.NumeroChasis);
        }
    }
}
