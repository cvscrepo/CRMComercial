using Microsoft.AspNetCore.Mvc;
using SistemaComercial.BLL.Servicios.Contrato;
using SistemaComercial.BLL.Servicios_Tareas.Contrato;
using SistemaComercial.DTO;
using SistemaComercial.Utility;
using System.Linq.Expressions;

namespace CRM_Comercial.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CotizacionController : ControllerBase
    {
        private ICotizacionService _cotizacionService;
        private ICotizacionLogica _cotizacionLogicaService;

        public CotizacionController(ICotizacionService cotizacionService, ICotizacionLogica cotizacionLogica)
        {
            _cotizacionService = cotizacionService;
            _cotizacionLogicaService = cotizacionLogica;
        }

        [HttpGet]
        public async Task<IActionResult> ListarCotizaciones()
        {
            Response response = new Response();
            try
            {
                var listaCotizaciones = _cotizacionService.ListarCotizaciones();
                response.Success = true;
                response.Message = "Ok";
                response.Value = listaCotizaciones.Result;
                return Ok(response);
            }
            catch (Exception ex) 
            {
                response.Success=false;
                response.Message=ex.Message;
                return BadRequest(response);
            }
        }

        [HttpGet("id")]
        public async Task<IActionResult> ListarCotizacion([FromQuery] int id)
        {
            Response response = new Response();
            try
            {
                var listarCotizacion = await _cotizacionService.ListarCotizacion(id);
                response.Success = true;
                response.Message = "Ok";
                response.Value=listarCotizacion;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message=ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CrearCotizacion([FromBody] CotizacionDTO cotizacion)
        {
            Response response = new Response();
            try
            {
                var cotizacionCreada = await _cotizacionLogicaService.RegistrarCotizacion(cotizacion);
                response.Success = true;
                response.Message = "Ok";
                response.Value = cotizacionCreada;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message=ex.Message;
                return BadRequest(response);    
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditarCotizacion([FromBody] CotizacionDTO cotizacion)
        {
            Response response = new Response();
            try
            {
                var cotizacionEditada = await _cotizacionService.EditarCotizacion(cotizacion);
                response.Success = true;
                response.Message = "Ok";
                response.Value = cotizacionEditada;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message=ex.Message;
                return BadRequest(response);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarCotizacion([FromQuery] int id)
        {
            Response response = new Response();
            try
            {
                var cotizacionEliminada = await _cotizacionService.EliminarCotizacion(id);
                response.Success = true;
                response.Message = "Ok";
                response.Value= cotizacionEliminada;
                return Ok(response);
            }
            catch (Exception ex) 
            {
                response.Success = false;
                response.Message=ex.Message;
                return BadRequest(response);
            }
        }

    }
}
