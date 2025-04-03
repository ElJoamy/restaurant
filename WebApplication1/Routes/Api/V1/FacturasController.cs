using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Routes.Api.V1
{
    [ApiController]
    [Route("api/v1/facturas")]
    [Authorize]
    public class FacturasController : ControllerBase
    {
        private readonly FacturasService _service;

        public FacturasController(FacturasService service)
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
            var factura = await _service.GetById(id);
            if (factura == null) return NotFound();
            return Ok(factura);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FacturasDto dto)
        {
            var nueva = await _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = nueva.IdFactura }, nueva);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FacturasDto dto)
        {
            var result = await _service.Update(id, dto);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
