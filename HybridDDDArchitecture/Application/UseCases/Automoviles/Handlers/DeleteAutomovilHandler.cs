using Application.Repositories;
using Application.UseCases.Automoviles.Commands;

namespace Application.UseCases.Automoviles.Handlers
{
    /// <summary>
    /// Handler del caso de uso: eliminar un Automóvil (DELETE /api/v1/automovil/{id})
    /// Requisitos del parcial:
    /// - 404 si el Id no existe
    /// - 200/204 si se elimina correctamente
    /// 
    /// Convenciones:
    /// - Lanzamos KeyNotFoundException -> la API la mapeará a 404
    /// </summary>
    public class DeleteAutomovilHandler
    {
        private readonly IAutomovilRepository _repo;

        public DeleteAutomovilHandler(IAutomovilRepository repo)
        {
            _repo = repo;
        }

        public async Task Handle(DeleteAutomovilCommand cmd)
        {
            var entity = await _repo.GetByIdAsync(cmd.Id);
            if (entity is null)
                throw new KeyNotFoundException("AUTOMOVIL_NO_ENCONTRADO");

            await _repo.DeleteAsync(cmd.Id);
        }
    }
}
