using Microsoft.AspNetCore.Mvc;
using SistemaComercial.BLL.Servicios.Contrato;
using SistemaComercial.DTO;
using SistemaComercial.Utility;

namespace CRM_Comercial.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IUsurioService _usuarioService;
        private readonly IConfiguration _configuration;

        public LoginController(ILoginService loginService, IUsurioService usuarioService, IConfiguration configuration)
        {
            _configuration = configuration;
            _usuarioService = usuarioService;
            _loginService = loginService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            Response response = new Response();
            try
            {
                var user = await _loginService.Login(login);
                if (user == null)
                {
                    throw new TaskCanceledException("Credenciales invalidas");
                }
                //generar un token

                response.Success = true;
                response.Message = "Credenciales correctas, token asignado:";
                response.Value = user;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UsuarioDTO usuario)
        {
            Response response = new Response();
            try
            {
                var usuarioCreado = await _loginService.Register(usuario);
                response.Success = true;
                response.Message = "Ok";
                response.Value = usuarioCreado;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        //private string GenerateToken(UsuarioDTO usuario)
        //{
        //    var claims = new[]
        //    {
        //        new Claim(ClaimTypes.Name, usuario.NombreCompleto),
        //        new Claim(ClaimTypes.Email, usuario.Email)
        //    }
        //}
    }
}
