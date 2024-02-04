using Microsoft.AspNetCore.Mvc;
using SistemaComercial.BLL.Servicios;
using SistemaComercial.BLL.Servicios.Contrato;
using SistemaComercial.DTO;
using SistemaComercial.Utility;

namespace CRM_Comercial.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContratoController : ControllerBase
    {
        private readonly IContratoService _contratoService;

        public ContratoController(IContratoService contratoService)
        {
            _contratoService = contratoService;
        }

        [HttpGet]
        public async Task<IActionResult> ListarContrato()
        {
            Response response = new Response();
            try
            {
                var listaContratos = await _contratoService.ListarContratos();
                response.Success = true;
                response.Message = "Ok";
                response.Value = listaContratos;
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
        public async Task<IActionResult> ListarContrato([FromQuery] int id)
        {
            Response response = new Response();
            try
            {
                var contratoEncontrado = await _contratoService.ListarContrato(id);
                response.Success = true;
                response.Message = "Ok";
                response.Value = contratoEncontrado;
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
        public async Task<IActionResult> CrearContrato([FromBody] ContratoDTO contrato)
        {
            Response response = new Response();
            try
            {
                var clienteCreado = await _contratoService.CrearContrato(contrato);
                response.Success = true;
                response.Message = "Ok";
                response.Value = clienteCreado;
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
        public async Task<IActionResult> EditarContrato([FromBody] ContratoDTO contrato)
        {
            Response response = new Response();
            try
            {
                var contratoEditado = await _contratoService.EditarContrato(contrato);
                response.Success = true;
                response.Message = "Ok";
                response.Value = contratoEditado;
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
        public async Task<IActionResult> EliminarContrato([FromQuery] int id)
        {
            Response response = new Response();
            try
            {
                var contratoEliminado = await _contratoService.EliminarContrato(id);
                response.Success = true;
                response.Message = "Ok";
                response.Value = contratoEliminado;
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
