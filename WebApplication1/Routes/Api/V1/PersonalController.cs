using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Routes.Api.V1
{
    [ApiController]
    [Route("api/v1/personal")]
    [Authorize]
    public class PersonalController : ControllerBase
    {
        private readonly PersonalService _service;

        public PersonalController(PersonalService service)
        {
            _service = service;
        }

        // GET: /api/v1/personal
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var personal = await _service.GetAll();
            return Ok(personal);
        }

        // GET: /api/v1/personal/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var persona = await _service.GetById(id);
            if (persona == null) return NotFound();
            return Ok(persona);
        }

        // POST: /api/v1/personal
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PersonalDto dto)
        {
            var persona = await _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = persona.IdPersonal }, persona);
        }

        // PUT: /api/v1/personal/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PersonalDto dto)
        {
            var success = await _service.Update(id, dto);
            if (!success) return NotFound();
            return NoContent();
        }

        // PUT: /api/v1/personal/{id}/desactivar
        [HttpPut("{id}/desactivar")]
        public async Task<IActionResult> Desactivar(int id)
        {
            var success = await _service.Desactivar(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
