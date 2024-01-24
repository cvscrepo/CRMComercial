using Microsoft.AspNetCore.Mvc;
using SistemaComercial.BLL.Servicios.Contrato;
using SistemaComercial.DTO;
using SistemaComercial.Utility;
using System.Runtime.CompilerServices;

namespace CRM_Comercial.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoServicioController : ControllerBase
    {
        private readonly ITipoService _service;

        public TipoServicioController(ITipoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ListarTipoServicios()
        {
            Response response = new Response();
            try
            {
                var tiposServiciosEncontrados = await _service.ListarTiposServicio();
                response.Success = true;
                response.Message = "Ok";
                response.Value = tiposServiciosEncontrados;
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
        public async Task<IActionResult> CrearTipoServicios([FromBody] TipoServicioDTO tipoServicio)
        {
            Response response = new Response();
            try
            {
                var tipoServicioCreado = await _service.CrearTipoServicio(tipoServicio);
                response.Success = true;
                response.Message = "Ok";
                response.Value = tipoServicioCreado;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message= ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditarTipoServicio([FromBody] TipoServicioDTO tipoServicio)
        {
            Response response = new Response();
            try
            {
                var tipoServicioEditado = await _service.EditarTipoServicio(tipoServicio);
                response.Success = true;
                response.Message = "Ok";
                response.Value= tipoServicioEditado;
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
        public async Task<IActionResult> EliminarTipoServicio([FromBody] TipoServicioDTO tipoServicio)
        {
            Response response = new Response();
            try
            {
                var tipoServicioEliminado = await _service.EliminarTipoServicio(tipoServicio);
                response.Success = true;
                response.Message = "Ok";
                response.Value = tipoServicioEliminado;
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
