using Application.DataTransferObjects;
using Application.Repositories;
using Application.UseCases.Automoviles.Queries;

namespace Application.UseCases.Automoviles.Handlers
{
    /// <summary>
    /// Handler de la consulta: obtener todos los Automóviles
    /// Requisitos del parcial:
    /// - Devuelve lista (vacía si no hay registros)
    /// - 200 siempre (lista)
    /// </summary>
    public class GetAllAutomovilesHandler
    {
        private readonly IAutomovilRepository _repo;

        public GetAllAutomovilesHandler(IAutomovilRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<AutomovilResultDto>> Handle(GetAllAutomovilesQuery query)
        {
            var entities = await _repo.GetAllAsync();

            return entities
                .Select(e => new AutomovilResultDto(
                    e.Id, e.Marca, e.Modelo, e.Color,
                    e.Fabricacion, e.NumeroMotor, e.NumeroChasis
                ))
                .ToList();
        }
    }
}
