using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Routes.Api.V1
{
    [ApiController]
    [Route("api/v1/mesas")]
    [Authorize]
    public class MesasController : ControllerBase
    {
        private readonly MesasService _service;

        public MesasController(MesasService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var mesa = await _service.GetById(id);
            if (mesa == null) return NotFound();
            return Ok(mesa);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MesasDto dto)
        {
            var mesa = await _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = mesa.IdMesa }, mesa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MesasDto dto)
        {
            var success = await _service.Update(id, dto);
            if (!success) return NotFound();
            return NoContent();
        }
        // PUT: /api/v1/mesas/{id}/desactivar
        [HttpPut("{id}/desactivar")]
        public async Task<IActionResult> Desactivar(int id)
        {
            var result = await _service.Desactivar(id);
            if (!result) return NotFound();
            return NoContent();
        }

    }
}
