using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Routes.Api.V1
{
    [ApiController]
    [Route("api/v1/compras/flujo")]
    [Authorize]
    public class FlujoCompletoCompraController : ControllerBase
    {
        private readonly FlujoCompletoCompraService _service;

        public FlujoCompletoCompraController(FlujoCompletoCompraService service)
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
        public async Task<IActionResult> Procesar([FromBody] CompraCompletaDto dto)
        {
            var result = await _service.ProcesarCompra(dto);
            return result
                ? Ok(new { message = "Compra registrada correctamente." })
                : BadRequest(new { message = "Error al registrar la compra." });
        }
    }
}
