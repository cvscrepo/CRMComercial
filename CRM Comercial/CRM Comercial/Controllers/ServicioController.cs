using Microsoft.AspNetCore.Mvc;
using SistemaComercial.BLL.Servicios.Contrato;
using SistemaComercial.DTO;
using SistemaComercial.Utility;

namespace CRM_Comercial.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicioController : ControllerBase
    {
        private readonly IServicioService _servicioService;

        public ServicioController(IServicioService servicioService)
        {
            _servicioService = servicioService;
        }

        [HttpGet]
        public async Task<IActionResult> ListarServicios()
        {
            Response response = new Response();
            try
            {
                var listaServicios = await _servicioService.ListarServicios();
                response.Success = true;
                response.Message = "Ok";
                response.Value = listaServicios;
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
        public async Task<IActionResult> ListarServicio([FromQuery] int id)
        {
            Response response = new Response();
            try
            {
                var servicioEncontrado = await _servicioService.ListarServicio(id);
                response.Success = true;
                response.Message = "Ok";
                response.Value = servicioEncontrado;
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
        public async Task<IActionResult> CrearServicio([FromBody] ServicioDTO servicio)
        {
            Response response = new Response();
            try
            {
                var servicioCreado = await _servicioService.CrearServicio(servicio);
                response.Success = true;
                response.Message = "Ok";
                response.Value = servicioCreado;
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
        public async Task<IActionResult> EditarServicio([FromBody] ServicioDTO servicio)
        {
            Response response = new Response();
            try
            {
                var servicioEditado = await _servicioService.EditarServicio(servicio);
                response.Success = true;
                response.Message = "Ok";
                response.Value = servicioEditado;
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
        public async Task<IActionResult> EliminarServicio([FromQuery] int id)
        {
            Response response = new Response();
            try
            {
                var clienteEliminado = await _servicioService.EliminarServicio(id);
                response.Success = true;
                response.Message = "Ok";
                response.Value = clienteEliminado;
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
