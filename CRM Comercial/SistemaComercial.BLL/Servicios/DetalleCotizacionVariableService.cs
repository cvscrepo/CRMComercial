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
    public class DetalleCotizacionVariableService : IDetalleCotizacionVariableService
    {
        private readonly IGenericRepository<DetalleCotizacionVariable> _detalleVaraibleRepository;
        private readonly IMapper _mapper;

        public DetalleCotizacionVariableService(IGenericRepository<DetalleCotizacionVariable> detalleVaraibleRepository, IMapper mapper)
        {
            _detalleVaraibleRepository = detalleVaraibleRepository;
            _mapper = mapper;
        }
        public async Task<List<DetalleCotizacionVariableDTO>> ListarDetalleVariables()
        {
            try
            {
                var listarDetalleVariables = await _detalleVaraibleRepository.Consultar();
                var quey = listarDetalleVariables.Include(v => v.IdDetalleCotizacionVariables).AsEnumerable().ToList();
                return _mapper.Map<List<DetalleCotizacionVariableDTO>>(listarDetalleVariables);
            }
            catch
            {
                throw;
            }
        }

        public async Task<DetalleCotizacionVariableDTO> ListarDetalleVariable(int idDetalleCotizacion)
        {
            try
            {
                var listarDetalle = await _detalleVaraibleRepository.Consultar(v => v.IdDetalleCotizacion == idDetalleCotizacion);
                var quey = listarDetalle.Include(v => v.IdDetalleCotizacionVariables).First();
                return _mapper.Map<DetalleCotizacionVariableDTO>(listarDetalle);
            }
            catch
            {
                throw;
            }
        }

        public async Task<DetalleCotizacionVariableDTO> CrearDetalleVariable(DetalleCotizacionVariableDTO detalle)
        {
            try
            {
                var detalleCreado = await _detalleVaraibleRepository.Crear(_mapper.Map<DetalleCotizacionVariable>(detalle));
                if (detalleCreado != null) throw new TaskCanceledException("No se pudo crear el detalle cotización variable");
                var consultarDetalle = await _detalleVaraibleRepository.Consultar(v => v.IdDetalleCotizacionVariables == detalleCreado.IdDetalleCotizacionVariables);
                var query = consultarDetalle.Include(v => v.IdVariablesEconomicas).First();
                return _mapper.Map<DetalleCotizacionVariableDTO>(query);
            }
            catch
            {
                throw;
            }
        }

        public async Task<DetalleCotizacionVariableDTO> EditarDetalleVariable(DetalleCotizacionVariableDTO detalle)
        {
            try
            {
                var consultarEditado = await _detalleVaraibleRepository.Obtener(v => v.IdDetalleCotizacionVariables == detalle.IdDetalleCotizacionVariables);
                if (consultarEditado != null) throw new TaskCanceledException("No se encontró el detalle cotización variable a editar");
                consultarEditado.Valor = Convert.ToDecimal(detalle.Valor);
                var editarVariable = await _detalleVaraibleRepository.Editar(consultarEditado);
                return _mapper.Map<DetalleCotizacionVariableDTO>(consultarEditado);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> EliminarDetalleVariable(int idDetalleCotizacion)
        {
            try
            {
                var consultarEditado = await _detalleVaraibleRepository.Obtener(v => v.IdDetalleCotizacion == idDetalleCotizacion);
                if (consultarEditado != null) throw new TaskCanceledException("No se encontró el detalle cotización variable a editar");
                var detalleVariableEliminado = await _detalleVaraibleRepository.Eliminar(consultarEditado);
                return detalleVariableEliminado;
            }
            catch
            {
                throw;
            }
        }

    }
}
