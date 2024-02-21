using Microsoft.AspNetCore.Mvc;
using SistemaComercial.BLL.Servicios;
using SistemaComercial.BLL.Servicios.Contrato;
using SistemaComercial.DTO;
using SistemaComercial.Utility;

namespace CRM_Comercial.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetalleCotizacionController : ControllerBase
    {
        private readonly IDetalleCotizacionService _detalleCotizacionService;

        public DetalleCotizacionController(IDetalleCotizacionService detalleCotizacionService)
        {
            _detalleCotizacionService = detalleCotizacionService;
        }

        [HttpGet("id")]
        public async Task<IActionResult> ListarDetalles([FromQuery] int id)
        {
            Response response = new Response();
            try
            {
                var cotizacionDetalleEncontrado = await _detalleCotizacionService.ListarDetalleCotizacion(id);
                response.Success = true;
                response.Message = "Ok";
                response.Value = cotizacionDetalleEncontrado;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CrearDetalle([FromBody] DetalleCotizacionDTO detalle)
        {
            Response response = new Response();
            try
            {
                var detalleCreado = await _detalleCotizacionService.CrearDetalleCotizacion(detalle);
                response.Success = true;
                response.Message = "Ok";
                response.Value = detalleCreado;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditarDetalle([FromBody] DetalleCotizacionDTO detalle)
        {
            Response response = new Response();
            try
            {
                var detalleEditado = await _detalleCotizacionService.EditarDetalleCotizacion(detalle);
                response.Success = true;
                response.Message = "Ok";
                response.Value = detalleEditado;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarDetalle([FromQuery] int id)
        {
            Response response = new Response();
            try
            {
                var detalleEliminado = await _detalleCotizacionService.EliminarDetalleCotizacion(id);
                response.Success = true;
                response.Message = "Ok";
                response.Value = detalleEliminado;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }
    }
}
