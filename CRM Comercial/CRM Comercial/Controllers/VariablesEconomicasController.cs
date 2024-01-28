using Microsoft.AspNetCore.Mvc;
using SistemaComercial.BLL.Servicios.Contrato;
using SistemaComercial.DTO;
using SistemaComercial.Utility;

namespace CRM_Comercial.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VariablesEconomicasController : ControllerBase
    {
        private readonly IVariablesEconomicasService _variablesEconomicasService;

        public VariablesEconomicasController(IVariablesEconomicasService variablesEconomicasService)
        {
            _variablesEconomicasService = variablesEconomicasService;
        }

        [HttpGet]
        public async Task<IActionResult> ListarVariables()
        {
            Response response = new Response();
            try
            {
                var variables = await _variablesEconomicasService.ListarVariables();
                response.Success = true;
                response.Message = "Ok";
                response.Value = variables;
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
        public async Task<IActionResult> ListarVariable([FromQuery] int id)
        {
            Response response = new Response();
            try
            {
                var variableBD = await _variablesEconomicasService.ListarVariable(id);
                response.Success = true;
                response.Message = "Ok";
                response.Value = variableBD;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success=false;
                response.Message = ex.Message;
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CrearVariable([FromBody] VariablesEconomicaDTO variable)
        {
            Response response = new Response();
            try
            {
                var variableCreada = await _variablesEconomicasService.CrearVariable(variable);
                response.Success = true;
                response.Message = "Ok";
                response.Value = variableCreada;
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
        public async Task<IActionResult> Editarvariable([FromBody] VariablesEconomicaDTO variable)
        {
            Response response = new Response();
            try
            {
                var variableEditada = await _variablesEconomicasService.EditarVariable(variable);
                response.Success = true;
                response.Message = "Ok";
                response.Value = variableEditada;
                return Ok(response);

            }
            catch (Exception ex)
            {
                response.Success= false;
                response.Message = ex.Message;
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarVariable([FromQuery] int id)
        {
            Response response = new Response();
            try
            {
                var variableEliminada = await _variablesEconomicasService.EliminarVariable(id);
                response.Success = true;
                response.Message = "Ok";
                response.Value = variableEliminada;
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
