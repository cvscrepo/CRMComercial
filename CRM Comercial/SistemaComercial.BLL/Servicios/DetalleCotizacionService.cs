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

namespace SistemaComercial.BLL.Servicios
{
    public class DetalleCotizacionService : IDetalleCotizacionService
    {
        private readonly IGenericRepository<DetalleCotizacion> _detalleCotizacionRepository;
        private readonly IMapper _mapper;

        public DetalleCotizacionService(IGenericRepository<DetalleCotizacion> detalleCotizacionRepository, IMapper mapper)
        {
            _detalleCotizacionRepository = detalleCotizacionRepository;
            _mapper = mapper;
        }

        public async Task<List<DetalleCotizacionDTO>> ListarDetalleCotizacion(int id)
        {
            try
            {
                var listaDetalleCotizacion = await _detalleCotizacionRepository.Consultar(c => c.IdCotizacion == id);
                if(!listaDetalleCotizacion.Any()) throw new TaskCanceledException("No se encontró el detalle cotización");
                var query = listaDetalleCotizacion.Include(d => d.IdCotizacionNavigation)
                                                  .Include(c => c.IdServicioNavigation)
                                                  .Include(c => c.IdSucursalNavigation).AsEnumerable().ToList();

                return _mapper.Map<List<DetalleCotizacionDTO>>(query); 
            }
            catch
            {
                throw;
            }
        }

        public async Task<DetalleCotizacionDTO> CrearDetalleCotizacion(DetalleCotizacionDTO detalleCotizacion)
        {
            try
            {
                var detalleCotizacionCreada = await _detalleCotizacionRepository.Crear(_mapper.Map<DetalleCotizacion>(detalleCotizacion));
                if(detalleCotizacionCreada == null) throw new TaskCanceledException("No se pudo crear el detalle de la cotización");
                var cotizacionDetalleCreada = await _detalleCotizacionRepository.Consultar(c => c.IdDetalleCotizacion == detalleCotizacionCreada.IdDetalleCotizacion);
                var query = cotizacionDetalleCreada.Include(d => d.IdCotizacionNavigation)
                                                         .Include(c => c.IdServicioNavigation)
                                                         .Include(c => c.IdSucursalNavigation).First();
                return _mapper.Map<DetalleCotizacionDTO>(query);
            }
            catch
            {
                throw;
            }
        }

        public async Task<DetalleCotizacionDTO> EditarDetalleCotizacion(DetalleCotizacionDTO detalleCotizacion)
        {
            try
            {
                var detalleCotizacionEncontrada = await _detalleCotizacionRepository.Obtener((c) => c.IdDetalleCotizacion == detalleCotizacion.IdDetalleCotizacion);
                if(detalleCotizacionEncontrada == null) throw new TaskCanceledException("Detalle Cotización a editar no encontrado");
                detalleCotizacionEncontrada.IdServicio = detalleCotizacion.IdServicio;
                detalleCotizacionEncontrada.IdSucursal = detalleCotizacion.IdSucursal;
                detalleCotizacionEncontrada.CantidadServicios = detalleCotizacion.CantidadServicios;
                detalleCotizacionEncontrada.DetalleServicio = detalleCotizacion.DetalleServicio;
                detalleCotizacionEncontrada.Total = Convert.ToDecimal(detalleCotizacion.Total);
                detalleCotizacionEncontrada.UpdatedAt = DateTime.Now;
                var detalleEditado = await _detalleCotizacionRepository.Editar(detalleCotizacionEncontrada);
                if (!detalleEditado) throw new TaskCanceledException("No se pudo editar el detalle de cotización");
                return _mapper.Map<DetalleCotizacionDTO>(detalleEditado);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> EliminarDetalleCotizacion(int id)
        {
            try
            {
                var detalleCotizacionEncontrada = await _detalleCotizacionRepository.Obtener((c) => c.IdDetalleCotizacion == id);
                if (detalleCotizacionEncontrada == null) throw new TaskCanceledException("Detalle Cotización a eliminar no encontrado");
                var detalleEliminad = await _detalleCotizacionRepository.Eliminar(detalleCotizacionEncontrada);
                return detalleEliminad;
            }
            catch
            {
                throw;
            }
        }

    }
}
