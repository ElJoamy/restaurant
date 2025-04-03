// Controllers/ReservasController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Routes.Api.V1
{
    [ApiController]
    [Route("api/v1/reservas")]
    [Authorize]
    public class ReservasController : ControllerBase
    {
        private readonly ReservasService _service;

        public ReservasController(ReservasService service)
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
            var reserva = await _service.GetById(id);
            if (reserva == null) return NotFound();
            return Ok(reserva);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReservasDto dto)
        {
            var reserva = await _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = reserva.IdReserva }, reserva);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ReservasDto dto)
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
