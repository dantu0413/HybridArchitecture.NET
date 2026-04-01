using Application.DataTransferObjects;
using Application.Repositories;
using Application.UseCases.Automoviles.Queries;

namespace Application.UseCases.Automoviles.Handlers
{
    /// <summary>
    /// Handler de la consulta: obtener un Automóvil por Número de Chasis
    /// Requisitos del parcial:
    /// - 404 si el chasis no existe
    /// - 200 con el objeto si existe
    /// 
    /// Convenciones:
    /// - Lanzamos KeyNotFoundException -> la API la mapeará a 404
    /// </summary>
    public class GetAutomovilByChasisHandler
    {
        private readonly IAutomovilRepository _repo;

        public GetAutomovilByChasisHandler(IAutomovilRepository repo)
        {
            _repo = repo;
        }

        public async Task<AutomovilResultDto> Handle(GetAutomovilByChasisQuery query)
        {
            var e = await _repo.GetByChasisAsync(query.NumeroChasis);
            if (e is null)
                throw new KeyNotFoundException("AUTOMOVIL_NO_ENCONTRADO");

            return new AutomovilResultDto(
                e.Id, e.Marca, e.Modelo, e.Color,
                e.Fabricacion, e.NumeroMotor, e.NumeroChasis
            );
        }
    }
}
