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
        // inyectá tus handlers o ICommandQueryBus

        public AutomovilController(CreateAutomovilHandler create /*, ...*/)
        {
            _create = create;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AutomovilCreateDto dto)
        {
            var created = await _create.Handle(new CreateAutomovilCommand(dto));
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) { /* llamar handler QueryById */ return Ok(); }

        [HttpGet("chasis/{numeroChasis}")]
        public async Task<IActionResult> GetByChasis(string numeroChasis) { /* ... */ return Ok(); }

        [HttpGet]
        public async Task<IActionResult> GetAll() { /* ... */ return Ok(); }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] AutomovilUpdateDto dto) { /* ... */ return Ok(); }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) { /* ... */ return NoContent(); }
    }
}
