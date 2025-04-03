using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Routes.Api.V1
{
    [ApiController]
    [Route("api/v1/compras")]
    [Authorize]
    public class ComprasController : ControllerBase
    {
        private readonly ComprasService _service;

        public ComprasController(ComprasService service)
        {
            _service = service;
        }

        // GET: /api/v1/compras
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var compras = await _service.GetAll();
            return Ok(compras);
        }

        // GET: /api/v1/compras/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var compra = await _service.GetById(id);
            if (compra == null) return NotFound();
            return Ok(compra);
        }

        // POST: /api/v1/compras
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ComprasDto dto)
        {
            var nuevaCompra = await _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = nuevaCompra.IdCompra }, nuevaCompra);
        }

        // PUT: /api/v1/compras/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ComprasDto dto)
        {
            var actualizado = await _service.Update(id, dto);
            if (!actualizado) return NotFound();
            return NoContent();
        }

        // PUT: /api/v1/compras/{id}/desactivar
        [HttpPut("{id}/desactivar")]
        public async Task<IActionResult> Desactivar(int id)
        {
            var result = await _service.Desactivar(id);
            if (!result) return NotFound();
            return NoContent();
        }

    }
}
