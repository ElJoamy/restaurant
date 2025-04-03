using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Routes.Api.V1
{
    [ApiController]
    [Route("api/v1/turnos")]
    [Authorize]
    public class TurnoPersonalController : ControllerBase
    {
        private readonly TurnoPersonalService _service;

        public TurnoPersonalController(TurnoPersonalService service)
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
            var turno = await _service.GetById(id);
            if (turno == null) return NotFound();
            return Ok(turno);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TurnoPersonalDto dto)
        {
            var turno = await _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = turno.IdTurnoPersonal }, turno);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TurnoPersonalDto dto)
        {
            var result = await _service.Update(id, dto);
            if (!result) return NotFound();
            return NoContent();
        }

        // PUT: /api/v1/turnos/desactivar-por-personal/{idPersonal}
        [HttpPut("desactivar-por-personal/{idPersonal}")]
        public async Task<IActionResult> DesactivarPorPersonal(int idPersonal)
        {
            var result = await _service.DesactivarPorPersonal(idPersonal);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
