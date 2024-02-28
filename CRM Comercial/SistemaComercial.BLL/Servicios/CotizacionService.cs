using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaComercial.BLL.Servicios.Contrato;
using SistemaComercial.DAL.Repositorios.Contratos;
using SistemaComercial.DTO;
using SistemaComercial.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SistemaComercial.BLL.Servicios
{
    public class CotizacionService : ICotizacionService
    {
        private readonly IGenericRepository<Cotizacion> _cotizacionRepository;
        private readonly IMapper _mapper;

        public CotizacionService(IGenericRepository<Cotizacion> cotizacionRepository, IMapper mapper)
        {
            _cotizacionRepository = cotizacionRepository;
            _mapper = mapper;
        }
        public async Task<List<CotizacionDTO>> ListarCotizaciones()
        {
            try
            {
                var listaCotizaciones = await _cotizacionRepository.Consultar();
                var query = listaCotizaciones.Include(c => c.IdClienteNavigation).Include(c => c.IdUsuarioNavigation).Include(c => c.IdClienteNavigation).AsEnumerable().ToList();
                return _mapper.Map<List<CotizacionDTO>>(query);
            }
            catch
            {
                throw;
            }
        }

        public async Task<CotizacionDTO> ListarCotizacion(int id)
        {
            try
            {
                IQueryable<Cotizacion> listarCotizacion = await _cotizacionRepository.Consultar(c => c.IdCotizacion == id);
                var query = listarCotizacion.Include(c => c.IdClienteNavigation).Include(c => c.IdUsuarioNavigation).Include(c => c.IdClienteNavigation).First();
                return _mapper.Map<CotizacionDTO>(query);
            }
            catch
            {
                throw;
            }
        }

        public async Task<CotizacionDTO> CrearCotizacion(CotizacionDTO cotizacion)
        {
            try
            {
                Cotizacion cotizacionModelo = _mapper.Map<Cotizacion>(cotizacion);
                Cotizacion cotizacionCreada = await _cotizacionRepository.Crear(cotizacionModelo) ?? throw new TaskCanceledException("No se pudo crear el cliente");
                IQueryable<Cotizacion> listarCotizacion = await _cotizacionRepository.Consultar(c => c.IdCliente == cotizacion.IdCliente);
                IQueryable<Cotizacion> query = listarCotizacion.Include(c => c.IdClienteNavigation).Include(c => c.IdUsuarioNavigation).Include(c => c.IdClienteNavigation);
                return _mapper.Map<CotizacionDTO>(cotizacionCreada);
                
            }
            catch
            {
                throw;
            }
        }

        public async Task<CotizacionDTO> EditarCotizacion(CotizacionDTO cotizacion)
        {
            try
            {
                Cotizacion listarCotizacion = await _cotizacionRepository.Obtener(c => c.IdCotizacion == cotizacion.IdCotizacion);
                if(listarCotizacion == null)
                {
                    throw new TaskCanceledException("No se encontró la cotización");
                }
                listarCotizacion.IdCliente = cotizacion.IdCliente;
                listarCotizacion.IdUsuario = cotizacion.IdUsuario;
                listarCotizacion.Nombre = cotizacion.Nombre;
                listarCotizacion.EditadoPor = cotizacion.EditadoPor;
                listarCotizacion.Total = Convert.ToDecimal(cotizacion.Total);
                listarCotizacion.Estado = cotizacion.Estado;
                listarCotizacion.UpdatedAt = DateTime.Now;
                bool cotizacionEditada = await _cotizacionRepository.Editar(listarCotizacion);
                               
                if (!cotizacionEditada) 
                {
                    throw new TaskCanceledException("No se pudo editar el la cotización");
                }
                return _mapper.Map<CotizacionDTO>(listarCotizacion);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> EliminarCotizacion(int id)
        {
            try
            {
                Cotizacion listarCotizacion = await _cotizacionRepository.Obtener(c => c.IdCliente == id);
                if (listarCotizacion == null)
                {
                    throw new TaskCanceledException("No se encontró la cotización");
                }
                bool cotizacionEliminada = await _cotizacionRepository.Eliminar(listarCotizacion);
                if(cotizacionEliminada == null)
                {
                    throw new TaskCanceledException("No se pudo eliminar la cotización");
                }
                return cotizacionEliminada;
            }
            catch
            {
                throw;
            }
        }

    }
}
