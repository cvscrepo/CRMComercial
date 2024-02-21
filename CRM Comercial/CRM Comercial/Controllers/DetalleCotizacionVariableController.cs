using Microsoft.AspNetCore.Mvc;
using SistemaComercial.BLL.Servicios.Contrato;
using SistemaComercial.DTO;
using SistemaComercial.Utility;

namespace CRM_Comercial.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetalleCotizacionVariableController : ControllerBase
    {
        private readonly IDetalleCotizacionVariableService _detalleVariableService;

        public DetalleCotizacionVariableController(IDetalleCotizacionVariableService detalleVariableService)
        {
            _detalleVariableService = detalleVariableService;
        }

        [HttpGet]
        public async Task<IActionResult> ListarDetalles()
        {
            Response response = new Response();
            try
            {
                var listaDetalle = await _detalleVariableService.ListarDetalleVariables();
                response.Success = true;
                response.Message = "Ok";
                response.Value = listaDetalle;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }

        }

        [HttpGet("id")]
        public async Task<IActionResult> ListarDetalle([FromQuery] int id)
        {
            Response response = new Response();
            try
            {
                var detalleEncontrado = await _detalleVariableService.ListarDetalleVariable(id);
                response.Success = true;
                response.Message = "Ok";
                response.Value = detalleEncontrado;
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
        public async Task<IActionResult> CrearDetalle([FromBody] DetalleCotizacionVariableDTO detalle)
        {
            Response response = new Response();
            try
            {
                var detalleCreado = await _detalleVariableService.CrearDetalleVariable(detalle);
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
        public async Task<IActionResult> EditarDetalle([FromBody] DetalleCotizacionVariableDTO detalle)
        {
            Response response = new Response();
            try
            {
                var detalleEditado = await _detalleVariableService.EditarDetalleVariable(detalle);
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
                var detalleEliminado = await _detalleVariableService.EliminarDetalleVariable(id);
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
