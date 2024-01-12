using Microsoft.AspNetCore.Mvc;
using SistemaComercial.BLL.Servicios.Contrato;
using SistemaComercial.DTO;
using SistemaComercial.Model;
using SistemaComercial.Utility;

namespace CRM_Comercial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        public IRolService _rolService { get; set; }
        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }



        [HttpGet("")]
        public async Task<IActionResult> ObtenerRoles()
        {
            Response response = new Response();
            try
            {
                var roles = await _rolService.ListRoles();
                response.Message = "OK";
                response.Value = roles;
                return Ok(response);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("id")]
        public async Task<IActionResult> ObtenerRol([FromQuery] int id)
        {
            Response response = new Response();
            try
            {
                var role = await _rolService.ListRole(id);
                response.Success = true;
                response.Message = "OK";
                response.Value = role;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
