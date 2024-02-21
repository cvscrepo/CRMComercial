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
    public class ContratoService : IContratoService
    {
        private readonly IGenericRepository<SistemaComercial.Model.Contrato> _contratoRepository;
        private readonly ICotizacionService _cotizacionService;
        private readonly IMapper _mapper;

        public ContratoService(IGenericRepository<Model.Contrato> contratoRepository, ICotizacionService cotizacionService, IMapper mapper)
        {
            _contratoRepository = contratoRepository;
            _cotizacionService = cotizacionService;
            _mapper = mapper;
        }
        public async Task<List<ContratoDTO>> ListarContratos()
        {
            try
            {
                var listarContratos = await _contratoRepository.Consultar();
                var query = listarContratos.Include(c => c.IdCotizacionNavigation).AsEnumerable().ToList();
                return _mapper.Map<List<ContratoDTO>>(query);

            }
            catch
            {
                throw;
            }
        }

        public async Task<ContratoDTO> ListarContrato(int id)
        {
            try
            {
                if (id == null | id == -0) throw new TaskCanceledException("Proporcione un id válido");
                var listarContrato = await _contratoRepository.Consultar(c => c.id == id);
                if(!listarContrato.Any())
                {
                    throw new TaskCanceledException("Contrato no encontrado");
                }
                var query = listarContrato.Include(c => c.IdCotizacionNavigation).Include(c => c.IdCotizacionNavigation.IdClienteNavigation).Include(c => c.IdCotizacionNavigation.IdUsuarioNavigation).First();
                
                return _mapper.Map<ContratoDTO>(query);
            }
            catch
            {
                throw;
            }
        }
        public async Task<ContratoDTO> CrearContrato(ContratoDTO contratoDTO)
        {
            try
            {
                var listarCotizacion = await _cotizacionService.ListarCotizacion(contratoDTO.IdCotizacion);
                contratoDTO.Nit = listarCotizacion.IdClienteNavigation.Nit;
                var contratoCreado = await _contratoRepository.Crear(_mapper.Map<SistemaComercial.Model.Contrato>(contratoDTO));
                if (contratoCreado == null)
                {
                    throw new TaskCanceledException("El contrato no pudo ser creado");
                }
                var listarContrato = await _contratoRepository.Consultar((c) => c.id == contratoCreado.id);
                var query = listarContrato.Include(c => c.IdCotizacionNavigation).First();
                return _mapper.Map<ContratoDTO>(query);
            }
            catch
            {
                throw;
            }
        }

        public async Task<ContratoDTO> EditarContrato(ContratoDTO contratoDTO)
        {
            try
            {
                var contratoEncontrado = await _contratoRepository.Obtener(c=>c.id == contratoDTO.id);
                if(contratoEncontrado == null)
                {
                    throw new TaskCanceledException("No se pudo encontrar el contrato");
                }
                contratoEncontrado.Nombre = contratoDTO.Nombre;
                contratoEncontrado.RepresentanteLegal = contratoDTO.RepresentanteLegal;
                contratoEncontrado.FechaEdicion = DateTime.Now;
                var contratoEditado = await _contratoRepository.Editar(contratoEncontrado);
                return _mapper.Map<ContratoDTO>(contratoEncontrado);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> EliminarContrato(int id)
        {
            try
            {
                var contratoEncontrado = await _contratoRepository.Obtener(c => c.id == id);
                if (contratoEncontrado == null)
                {
                    throw new TaskCanceledException("Contratos no encontrados");
                }
                var contratoEliminado = await _contratoRepository.Editar(contratoEncontrado);
                return contratoEliminado;
            }
            catch
            { 
                throw;
            }
        }

    }
}
