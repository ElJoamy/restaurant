using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Routes.Api.V1
{
    [ApiController]
    [Route("api/v1/flujo-completo")]
    [Authorize]
    public class FlujoCompletoController : ControllerBase
    {
        private readonly FlujoCompletoService _service;

        public FlujoCompletoController(FlujoCompletoService service)
        {
            _service = service;
        }

        [HttpGet("datos-iniciales")]
        public async Task<IActionResult> ObtenerDatosIniciales()
        {
            var result = await _service.ObtenerDatosIniciales();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Procesar([FromBody] FlujoCompletoDto dto)
        {
            var result = await _service.ProcesarFlujoCompleto(dto);
            return result
                ? Ok(new { message = "Flujo de pedido completado exitosamente." })
                : BadRequest(new { message = "Hubo un error al procesar el flujo." });
        }
    }
}
