using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Routes.Api.V1
{
    [ApiController]
    [Route("api/v1/detallecompra")]
    [Authorize]
    public class DetalleCompraController : ControllerBase
    {
        private readonly DetalleCompraService _service;

        public DetalleCompraController(DetalleCompraService service)
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
            var result = await _service.GetById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DetalleCompraDto dto)
        {
            var created = await _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.IdDetalleCompra }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DetalleCompraDto dto)
        {
            var updated = await _service.Update(id, dto);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpPut("{id}/desactivar")]
        public async Task<IActionResult> Desactivar(int id)
        {
            var success = await _service.Desactivar(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
