using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SistemaComercial.BLL.Servicios.Contrato;
using SistemaComercial.DTO;
using SistemaComercial.Model;
using SistemaComercial.Utility;

namespace CRM_Comercial.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IActionResult> ListarClientes()
        {
            Response response = new Response();
            try
            {
                var listaClientes = await _clienteService.ListarClientes();
                response.Success = true;
                response.Message = "Ok";
                response.Value = listaClientes;
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
        public async Task<IActionResult> ListarCliente([FromQuery] int id)
        {
            Response response = new Response();
            try
            {
                var clienteEncontrado = await _clienteService.ListarCliente(id);
                response.Success = true;
                response.Message = "Ok";
                response.Value = clienteEncontrado;
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
        public async Task<IActionResult> CrearCliente([FromBody] ClienteDTO cliente)
        {
            Response response = new Response();
            try
            {
                var clienteCreado = await _clienteService.CrearCliente(cliente);
                response.Success = true;
                response.Message = "Ok";
                response.Value = clienteCreado;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success= false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditarCliente([FromBody] ClienteDTO cliente)
        {
            Response response = new Response();
            try
            {
                var clienteEditado = await _clienteService.EditarCliene(cliente);
                response.Success = true;
                response.Message = "Ok";
                response.Value = clienteEditado;
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
        public async Task<IActionResult> EliminarCliente([FromQuery] int id)
        {
            Response response = new Response();
            try
            {
                var clienteEliminado = await _clienteService.EliminarCliente(id);
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
