using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaComercial.BLL.Servicios.Contrato;
using SistemaComercial.DAL.Repositorios.Contratos;
using SistemaComercial.DTO;
using SistemaComercial.Model;
using SistemaComercial.Utility;

namespace CRM_Comercial.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventarioController : ControllerBase
    {
        private readonly IInventarioService _inventarioService;
        private readonly IMapper _mapper;

        public InventarioController(IInventarioService inventarioService, IMapper mapper)
        {
            _inventarioService = inventarioService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ListarInventario()
        {
            Response response = new Response();
            try
            {
                var listaInventario = await _inventarioService.ListarInventario();
                response.Success = true;
                response.Message = "Ok";
                response.Value = listaInventario;
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
        public async Task<IActionResult> ListarItemInventario([FromQuery] int id)
        {
            Response response = new Response();
            try
            {
                var clienteEncontrado = await _inventarioService.ListarItemInventario(id);
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
        public async Task<IActionResult> CrearItemInventario([FromBody] InventarioDTO ItemInventario)
        {
            Response response = new Response();
            try
            {
                var ItemCreado = await _inventarioService.CrearItemInventrio(ItemInventario);
                response.Success = true;
                response.Message = "Ok";
                response.Value = ItemCreado;
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
        public async Task<IActionResult> EditarCliente([FromBody] InventarioDTO inventario)
        {
            Response response = new Response();
            try
            {
                var itemEditado = await _inventarioService.EditarItemInventario(inventario);
                response.Success = true;
                response.Message = "Ok";
                response.Value = itemEditado;
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
        public async Task<IActionResult> EliminarItem([FromQuery] int id)
        {
            Response response = new Response();
            try
            {
                var itemEliminado = await _inventarioService.EliminarItemInventario(id);
                response.Success = true;
                response.Message = "Ok";
                response.Value = itemEliminado;
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
