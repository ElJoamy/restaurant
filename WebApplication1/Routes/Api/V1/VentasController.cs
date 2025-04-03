using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Routes.Api.V1
{
    [ApiController]
    [Route("api/v1/ventas")]
    [Authorize]
    public class VentasController : ControllerBase
    {
        private readonly VentasService _service;

        public VentasController(VentasService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ventas = await _service.GetAll();
            return Ok(ventas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var venta = await _service.GetById(id);
            if (venta == null) return NotFound();
            return Ok(venta);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VentasDto dto)
        {
            var venta = await _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = venta.IdVenta }, venta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] VentasDto dto)
        {
            var success = await _service.Update(id, dto);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpPut("cancelar/{id}")]
        public async Task<IActionResult> Cancelar(int id)
        {
            var success = await _service.Cancelar(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
