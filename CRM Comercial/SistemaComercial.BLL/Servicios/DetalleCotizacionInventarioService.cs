using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
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
    public class DetalleCotizacionInventarioService : IDetalleCotizacionInventarioService
    {
        private readonly IGenericRepository<DetalleCotizacionInventario> _detalleCotizacionInventarioRepository;
        private readonly IInventarioService _inventarioService;
        private readonly IMapper _mapper;

        public DetalleCotizacionInventarioService(IGenericRepository<DetalleCotizacionInventario> detalleCotizacionInventarioRepository, IMapper mapper, IInventarioService inventarioService)
        {
            _detalleCotizacionInventarioRepository = detalleCotizacionInventarioRepository;
            _mapper = mapper;
            _inventarioService = inventarioService;
        }

        public async Task<List<DetalleCotizacionInventarioDTO>> ListarDetalleCotizacionInventario(int idDetalleCotizacion = 0, int idInventario = 0)
        {
            try
            {
                if(idDetalleCotizacion == 0 && idInventario == 0)
                {
                    var listarDetalle = await _detalleCotizacionInventarioRepository.Consultar();
                    var query = listarDetalle.Include("IdDetalleCotizacionNavigation").Include("IdInventarioNavigation").Include(v => v.IdInventarioNavigation.IdCategoriaInventarioNavigation).AsEnumerable().ToList();
                    return _mapper.Map<List<DetalleCotizacionInventarioDTO>>(query);
                }else if(idDetalleCotizacion != 0 && idInventario == 0)
                {
                    var listarDetalle = await _detalleCotizacionInventarioRepository.Consultar((d) => d.Id == idDetalleCotizacion);
                    var query = listarDetalle.Include("IdDetalleCotizacionNavigation").Include("IdInventarioNavigation").Include(v => v.IdInventarioNavigation.IdCategoriaInventarioNavigation).AsEnumerable().ToList();
                    return _mapper.Map<List<DetalleCotizacionInventarioDTO>>(query);
                }else if(idInventario != 0 && idDetalleCotizacion == 0)
                {
                    var listarDetalle = await _detalleCotizacionInventarioRepository.Consultar((d) => d.Id == idInventario);
                    var query = listarDetalle.Include("IdDetalleCotizacionNavigation").Include("IdInventarioNavigation").Include(v => v.IdInventarioNavigation.IdCategoriaInventarioNavigation).AsEnumerable().ToList();
                    return _mapper.Map<List<DetalleCotizacionInventarioDTO>>(query);
                }
                else
                {
                    throw new TaskCanceledException("No puedes enviar dos id´s a la vez");
                } 
                

            }
            catch
            {
                throw;
            }
        }
        public async Task<DetalleCotizacionInventarioDTO> CrearDetalleCotizacionInventario(DetalleCotizacionInventarioDTO detalleCotizacionInventario)
        {
            try
            {
                var detalleCreado = await _detalleCotizacionInventarioRepository.Crear(_mapper.Map<DetalleCotizacionInventario>(detalleCotizacionInventario)) ?? throw new TaskCanceledException("No se pudo crear el detalle");
                var detalleConsulta = await _detalleCotizacionInventarioRepository.Consultar((d) => d.Id == detalleCreado.Id);
                var query = detalleConsulta.Include("IdDetalleCotizacionNavigation").Include("IdInventarioNavigation").First();
                // Al momento de crear un detalle se debe hacer el descuento de cantidad disponible y sumar en canidad aásignada al inventario
                var inventarioDTO = await _inventarioService.ListarItemInventario(detalleCotizacionInventario.IdInventario);
                // Validaciones
                if (inventarioDTO.CantidadTotal < detalleCotizacionInventario.CantidadAsignada) throw new TaskCanceledException("No se puede asignar ");
                if (inventarioDTO.CantidadDisponible - detalleCotizacionInventario.CantidadAsignada < 0) throw new TaskCanceledException("No puedes asignar una cantidad mayor al valor disponible");

                // Operación
                inventarioDTO.CantidadDisponible -= detalleCotizacionInventario.CantidadAsignada;
                inventarioDTO.CantidadAsignada += detalleCotizacionInventario.CantidadAsignada;
                var inventarioEditado = await _inventarioService.EditarItemInventario(inventarioDTO);
                return _mapper.Map<DetalleCotizacionInventarioDTO>(query);
            }
            catch
            {
                throw;
            }
        }

        public async Task<DetalleCotizacionInventarioDTO> EditarDetalleCotizacionInventario(DetalleCotizacionInventarioDTO detalleCotizacionInventario)
        {
            try
            {
                var detalleConsultado = await _detalleCotizacionInventarioRepository.Obtener(c => c.Id == detalleCotizacionInventario.Id);
                if(detalleConsultado == null) throw new TaskCanceledException("No se encontró el detalle cotización inventario a editar");
                detalleConsultado.IdDetalleCotizacion = detalleCotizacionInventario.IdDetalleCotizacion;
                detalleConsultado.IdInventario = detalleCotizacionInventario.IdInventario;
                detalleConsultado.CantidadAsignada = detalleCotizacionInventario.CantidadAsignada;
                // Al momento de editar un detalle se debe hacer el descuento de cantidad disponible y sumar en cantidad asignada al inventario;
                var inventarioDTO = await _inventarioService.ListarItemInventario(detalleCotizacionInventario.IdInventario);
                // Validaciones
                if (inventarioDTO.CantidadTotal < detalleCotizacionInventario.CantidadAsignada) throw new TaskCanceledException("No se puede asignar ");
                if (inventarioDTO.CantidadDisponible - detalleCotizacionInventario.CantidadAsignada < 0) throw new TaskCanceledException("No puedes asignar una cantidad mayor al valor disponible");

                inventarioDTO.CantidadDisponible -= detalleCotizacionInventario.CantidadAsignada;
                inventarioDTO.CantidadAsignada += detalleCotizacionInventario.CantidadAsignada;

                var detalleEditado = await _detalleCotizacionInventarioRepository.Editar(detalleConsultado);
                var inventarioEditado = await _inventarioService.EditarItemInventario(inventarioDTO);
                return _mapper.Map<DetalleCotizacionInventarioDTO>(detalleConsultado);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> EliminarDetalleCotizacionInventario(int idDetalleCotizacionInventario)
        {
            try
            {
                var detalleConsultado = await _detalleCotizacionInventarioRepository.Obtener(c => c.Id == idDetalleCotizacionInventario) ?? throw new TaskCanceledException("Detalle a eliminar no se encontró");
                var detalleEliminado = await _detalleCotizacionInventarioRepository.Eliminar(detalleConsultado);
                // Al momento de editar un detalle se debe hacer el descuento de cantidad disponible y sumar en cantidad asignada al inventario;
                var inventarioDTO = await _inventarioService.ListarItemInventario(detalleConsultado.IdInventario);
                inventarioDTO.CantidadDisponible += detalleConsultado.CantidadAsignada;
                inventarioDTO.CantidadAsignada -= detalleConsultado.CantidadAsignada;
                var inventarioEditado = await _inventarioService.EditarItemInventario(inventarioDTO);
                return detalleEliminado;
            }
            catch
            {
                throw;
            }
        }
    }
}
