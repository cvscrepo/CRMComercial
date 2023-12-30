﻿using Microsoft.AspNetCore.Mvc;
using SistemaComercial.BLL.Servicios.Contrato;
using SistemaComercial.DAL.DBDatos.Contrato;
using SistemaComercial.DTO;
using SistemaComercial.Utility;

namespace CRM_Comercial.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsurioService _usuarioService;

        public UsuarioController(IUsurioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public IActionResult ListarUsuarios()
        {
            Response response = new Response();
            try
            {
                var usuariosEncotrados = _usuarioService.ListarUsuarios();
                response.Success = true;
                response.Message = "Ok";
                response.Value = usuariosEncotrados.Result;
                return Ok(response);

            }
            catch (Exception ex)
            {
                response.Message += ex.ToString();
                return BadRequest(response);
            }
        }

        [HttpGet("id")]
        public IActionResult ObtenerUsuario([FromQuery] int id)
        {
            Response response = new Response();
            try
            {
                var usuarioEncontrado = _usuarioService.ListarUsuario(id);
                response.Success = true;
                response.Message = "Ok";
                response.Value = usuarioEncontrado.Result;
                return Ok(response);
            }
            catch (Exception ex) 
            {
                response.Message+= ex.ToString();
                return BadRequest(response);
            }
        }

        [HttpPost]
        public IActionResult CrearUsuario([FromBody] UsuarioDTO usuario)
        {
            Response response = new Response();
            try
            {
                var usuarioCreado = _usuarioService.CrearUsuario(usuario); 
                response.Success = true;
                response.Message = "Ok";
                response.Value = usuarioCreado;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
                return BadRequest(response);
            }
        }

        [HttpPut]
        public IActionResult EditarUsuario([FromBody] UsuarioDTO usuario)
        {
            Response response = new Response();
            try
            {
                var usuarioEditado = _usuarioService.EditarUsuario(usuario);
                response.Success = true;
                response.Message = "Ok";
                response.Value = usuarioEditado;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Message=ex.ToString(); 
                return BadRequest(response);
            }
        }

        [HttpDelete]
        public IActionResult EliminarUsuario([FromQuery] int id)
        {
            Response response = new Response(); 
            try
            {
                var usuarioEliminado = _usuarioService.EliminarUsuario(id);
                response.Success = true;
                response.Message = "Ok";
                response.Value = usuarioEliminado;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Message=(ex.ToString());
                return BadRequest(response);
            }
        }
        
    }
}