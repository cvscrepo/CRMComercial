using Microsoft.AspNetCore.Mvc;
using SistemaComercial.BLL.Servicios;
using SistemaComercial.BLL.Servicios.Contrato;
using SistemaComercial.DTO;
using SistemaComercial.Utility;

namespace CRM_Comercial.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetalleCotizacionInventarioController : ControllerBase
    {
        private readonly IDetalleCotizacionInventarioService _detalleCotizacionInventarioService;

        public DetalleCotizacionInventarioController(IDetalleCotizacionInventarioService detalleCotizacionInventarioService)
        {
            _detalleCotizacionInventarioService = detalleCotizacionInventarioService;
        }

       

        [HttpGet("id")]
        public async Task<IActionResult> ListarDetalle([FromQuery] int idDetalleCotizacion = 0, int idInventario = 0)
        {
            Response response = new Response();
            try
            {
                var listaDetalleCotizacionInventario = await _detalleCotizacionInventarioService.ListarDetalleCotizacionInventario(idDetalleCotizacion, idInventario);
                response.Success = true;
                response.Message = "Ok";
                response.Value = listaDetalleCotizacionInventario;
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
        public async Task<IActionResult> CrearDetalle([FromBody] DetalleCotizacionInventarioDTO detalleCotizacionInventario)
        {
            Response response = new Response();
            try
            {
                var detalleCotizacionInventarioCreado = await _detalleCotizacionInventarioService.CrearDetalleCotizacionInventario(detalleCotizacionInventario);
                response.Success = true;
                response.Message = "Ok";
                response.Value = detalleCotizacionInventarioCreado;
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
        public async Task<IActionResult> EditarDetalle([FromBody] DetalleCotizacionInventarioDTO detalleCotizacionInventario)
        {
            Response response = new Response();
            try
            {
                var detalleCotizacionInventarioEditado = await _detalleCotizacionInventarioService.EditarDetalleCotizacionInventario(detalleCotizacionInventario);
                response.Success = true;
                response.Message = "Ok";
                response.Value = detalleCotizacionInventarioEditado;
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
                var detalleCotizacionInventarioEliminado = await _detalleCotizacionInventarioService.EliminarDetalleCotizacionInventario(id);
                response.Success = true;
                response.Message = "Ok";
                response.Value = detalleCotizacionInventarioEliminado;
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
