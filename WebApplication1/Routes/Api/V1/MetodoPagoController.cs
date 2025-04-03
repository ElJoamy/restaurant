using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Routes.Api.V1
{
    [ApiController]
    [Route("api/v1/metodopago")]
    [Authorize]
    public class MetodoPagoController : ControllerBase
    {
        private readonly MetodoPagoService _service;

        public MetodoPagoController(MetodoPagoService service)
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
        public async Task<IActionResult> Create([FromBody] MetodoPagoDto dto)
        {
            var result = await _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.IdMetodoPago }, result);
        }
    }
}
