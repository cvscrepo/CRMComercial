using Microsoft.AspNetCore.Mvc;
using SistemaComercial.BLL.Servicios.Contrato;
using SistemaComercial.DTO;
using SistemaComercial.Utility;

namespace CRM_Comercial.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaInventarioController : ControllerBase
    {
        private readonly ICategoriaInventarioService _categoriaInventarioService;

        public CategoriaInventarioController(ICategoriaInventarioService categoriaInventarioService)
        {
            _categoriaInventarioService = categoriaInventarioService;
        }

        [HttpGet]
        public async Task<IActionResult> ListarCategoria()
        {
            Response response = new Response();
            try
            {
                var listaCategoria = await _categoriaInventarioService.ListarCategorias();
                response.Success = true;
                response.Message = "Ok";
                response.Value = listaCategoria;
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
        public async Task<IActionResult> CrearCategoria([FromBody] CategoriaInventarioDTO categoria)
        {
            Response response = new Response();
            try
            {
                var categoriaCreada = await _categoriaInventarioService.CrearCategoria(categoria);
                response.Success = true;
                response.Message = "Ok";
                response.Value = categoriaCreada;
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
        public async Task<IActionResult> EditarCategoria([FromBody] CategoriaInventarioDTO categoria)
        {
            Response response = new Response();
            try
            {
                var categoriaEditada = await _categoriaInventarioService.EditarCategoria(categoria);
                response.Success = true;
                response.Message = "Ok";
                response.Value = categoriaEditada;
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
        public async Task<IActionResult> EliminarCategoria([FromQuery] int id)
        {
            Response response = new Response();
            try
            {
                var categoriaEliminada = await _categoriaInventarioService.EliminarCategoria(id);
                response.Success = true;
                response.Message = "Ok";
                response.Value = categoriaEliminada;
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
