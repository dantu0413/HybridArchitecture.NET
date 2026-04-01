using Application.DataTransferObjects;
using Application.UseCases.Automoviles.Commands;
using Application.UseCases.Automoviles.Handlers;
using Application.UseCases.Automoviles.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [Route("api/v1/automovil")]
    public class AutomovilController : ControllerBase
    {
        private readonly CreateAutomovilHandler _create;
        private readonly UpdateAutomovilHandler _update;
        private readonly DeleteAutomovilHandler _delete;
        private readonly GetAutomovilByIdHandler _getById;
        private readonly GetAutomovilByChasisHandler _getByChasis;
        private readonly GetAllAutomovilesHandler _getAll;

        public AutomovilController(
            CreateAutomovilHandler create,
            UpdateAutomovilHandler update,
            DeleteAutomovilHandler delete,
            GetAutomovilByIdHandler getById,
            GetAutomovilByChasisHandler getByChasis,
            GetAllAutomovilesHandler getAll)
        {
            _create = create;
            _update = update;
            _delete = delete;
            _getById = getById;
            _getByChasis = getByChasis;
            _getAll = getAll;
        }

        // POST /api/v1/automovil
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AutomovilCreateDto dto)
        {
            try
            {
                var created = await _create.Handle(new CreateAutomovilCommand(dto));
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (InvalidOperationException ex) when (ex.Message == "NUMERO_MOTOR_DUPLICADO" || ex.Message == "NUMERO_CHASIS_DUPLICADO")
            {
                return Conflict(new { error = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // PUT /api/v1/automovil/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] AutomovilUpdateDto dto)
        {
            try
            {
                var updated = await _update.Handle(new UpdateAutomovilCommand(id, dto));
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { error = "AUTOMOVIL_NO_ENCONTRADO" });
            }
            catch (InvalidOperationException ex) when (ex.Message == "NUMERO_MOTOR_DUPLICADO" || ex.Message == "NUMERO_CHASIS_DUPLICADO")
            {
                return Conflict(new { error = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // DELETE /api/v1/automovil/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _delete.Handle(new DeleteAutomovilCommand(id));
                return Ok(new { deleted = id }); // 200 OK (podrías usar 204 NoContent si preferís)
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { error = "AUTOMOVIL_NO_ENCONTRADO" });
            }
        }

        // GET /api/v1/automovil/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _getById.Handle(new GetAutomovilByIdQuery(id));
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { error = "AUTOMOVIL_NO_ENCONTRADO" });
            }
        }

        // GET /api/v1/automovil/chasis/{numeroChasis}
        [HttpGet("chasis/{numeroChasis}")]
        public async Task<IActionResult> GetByChasis(string numeroChasis)
        {
            try
            {
                var result = await _getByChasis.Handle(new GetAutomovilByChasisQuery(numeroChasis));
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { error = "AUTOMOVIL_NO_ENCONTRADO" });
            }
        }

        // GET /api/v1/automovil
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _getAll.Handle(new GetAllAutomovilesQuery());
            return Ok(list);
        }
    }
}
