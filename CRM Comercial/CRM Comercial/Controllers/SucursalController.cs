using Microsoft.AspNetCore.Mvc;
using SistemaComercial.BLL.Servicios.Contrato;
using SistemaComercial.DTO;
using SistemaComercial.Utility;

namespace CRM_Comercial.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SucursalController : ControllerBase
    {
        private readonly ISucursalService _service;

        public SucursalController(ISucursalService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ListarSusucursales()
        {
            Response response = new Response();
            try
            {
                var listaSucursales = await _service.ListarSucursales();
                response.Success = true;
                response.Message = "Ok";
                response.Value = listaSucursales;
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
        public async Task<IActionResult> ListarSucursal([FromQuery] int id)
        {
            Response response = new Response();
            try
            {
                var listarSucursal = await _service.ListarSucursal(id);
                response.Success = true;
                response.Message = "Ok";
                response.Value = listarSucursal;
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
        public async Task<IActionResult> CrearSucursal([FromBody] SucursalDTO sucursal)
        {
            Response response = new Response();
            try
            {
                var sucursalCreada = await _service.CrearSucursal(sucursal);
                response.Success = true;
                response.Message = "Ok";
                response.Value = sucursalCreada;
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
        public async Task<IActionResult> EditarSucursal([FromBody] SucursalDTO sucursal)
        {
            Response response = new Response();
            try
            {
                var sucursalEditada = await _service.EditarSucursal(sucursal);
                response.Success = true;
                response.Message = "Ok";
                response.Value = sucursalEditada;
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
        public async Task<IActionResult> EliminarSucursal([FromQuery] int id)
        {
            Response response = new Response();
            try
            {
                var sucursalEliminada = await _service.EliminarSucursal(id);
                response.Success = true;
                response.Message = "Ok";
                response.Value = sucursalEliminada;
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
