using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Routes.Api.V1
{
    [ApiController]
    [Route("api/v1/ventas/flujo")]
    [Authorize]
    public class VentaFlujoController : ControllerBase
    {
        private readonly VentaFlujoService _service;

        public VentaFlujoController(VentaFlujoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Procesar([FromBody] VentaCompletaDto dto)
        {
            var result = await _service.ProcesarVenta(dto);
            return result ? Ok(new { message = "Venta procesada correctamente." }) : BadRequest(new { message = "Error al procesar la venta." });
        }
        [HttpGet("datos-iniciales")]
        public async Task<IActionResult> ObtenerDatosIniciales()
        {
            var result = await _service.ObtenerDatosIniciales();
            return Ok(result);
        }
    }
}
