using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Routes.Api.V1
{
    [ApiController]
    [Route("api/v1/pagos")]
    [Authorize]
    public class PagoController : ControllerBase
    {
        private readonly PagoService _service;

        public PagoController(PagoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pagos = await _service.GetAll();
            return Ok(pagos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pago = await _service.GetById(id);
            if (pago == null) return NotFound();
            return Ok(pago);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PagoDto dto)
        {
            var nuevo = await _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = nuevo.IdPago }, nuevo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PagoDto dto)
        {
            var success = await _service.Update(id, dto);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
