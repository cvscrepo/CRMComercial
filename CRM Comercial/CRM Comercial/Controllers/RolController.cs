using Microsoft.AspNetCore.Mvc;
using SistemaComercial.BLL.Servicios.Contrato;
using SistemaComercial.DTO;
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
        public IActionResult ObtenerRoles()
        {
            Response response = new Response();
            try
            {
                var roles = _rolService.ListRoles();
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
        public IActionResult ObtenerRol([FromQuery] int id)
        {
            Response response = new Response();
            try
            {
                var role = _rolService.ListRole(id);
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

        [HttpPost]
        public IActionResult CrearRol([FromBody] RolDTO rolDTO)
        {
            Response response = new Response(); 
            try
            {
                var RolCreado = _rolService.CreateRol(rolDTO);
                response.Success = true;
                response.Message = "OK";
                response.Value = RolCreado;
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
        public IActionResult EditarRol([FromBody] RolDTO rol)
        {
            Response response = new Response();
            try
            {
                var rolEditado = _rolService.UpdateRole(rol);
                response.Success = true;
                response.Message= "OK";
                response.Value= rolEditado;
                return Ok(response);

            }catch(Exception ex)
            {
                response.Success = false;
                response.Message= ex.Message;
                return BadRequest(response);
            }
        }

        [HttpDelete]
        public IActionResult EliminarRol([FromQuery] int rolId)
        {
            Response response = new Response();
            try
            {
                var rolEliminado = _rolService.DeleteRole(rolId);
                
                response.Success = true;
                response.Message = rolEliminado ? "Ok" : "Rol no encontrado";
                response.Value = rolEliminado;
                return Ok(response);

            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message= ex.Message;
                return BadRequest(response); 
            }
        }
    }
}
