using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Routes.Api.V1
{
    [ApiController]
    [Route("api/v1/proveedores")]
    [Authorize]
    public class ProveedoresController : ControllerBase
    {
        private readonly ProveedoresService _service;

        public ProveedoresController(ProveedoresService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var proveedores = await _service.GetAll();
            return Ok(proveedores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var proveedor = await _service.GetById(id);
            if (proveedor == null) return NotFound();
            return Ok(proveedor);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProveedoresDto dto)
        {
            var proveedor = await _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = proveedor.IdProveedor }, proveedor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProveedoresDto dto)
        {
            var result = await _service.Update(id, dto);
            if (!result) return NotFound();
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
