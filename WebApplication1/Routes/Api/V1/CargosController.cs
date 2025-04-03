using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Routes.Api.V1
{
    [ApiController]
    [Route("api/v1/cargos")]
    [Authorize]
    public class CargosController : ControllerBase
    {
        private readonly CargoService _service;

        public CargosController(CargoService service)
        {
            _service = service;
        }

        // GET: /api/v1/cargos
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }

        // GET: /api/v1/cargos/by-name?nombre=Gerente
        [HttpGet("by-name")]
        public async Task<IActionResult> GetByNombre([FromQuery] string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return BadRequest(new { message = "El parámetro 'nombre' es requerido." });

            var result = await _service.GetByNombre(nombre);
            return Ok(result);
        }

        // GET: /api/v1/cargos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cargo = await _service.GetById(id);
            if (cargo == null) return NotFound();
            return Ok(cargo);
        }

        // POST: /api/v1/cargos
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CargoDto dto)
        {
            var cargo = await _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = cargo.IdCargo }, cargo);
        }

        // PUT: /api/v1/cargos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CargoDto dto)
        {
            var updated = await _service.Update(id, dto);
            if (!updated) return NotFound();
            return NoContent();
        }
    }

}
