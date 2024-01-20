using AutoMapper;
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
    public class TipoService : ITipoService
    {
        private readonly IGenericRepository<TipoServicio> _tipoServicioRepositorio;
        private readonly IMapper _mapper;

        public TipoService(IGenericRepository<TipoServicio> tipoServicioRepositorio, IMapper mapper)
        {
            _tipoServicioRepositorio = tipoServicioRepositorio;
            _mapper = mapper;
        }

        public async Task<List<TipoServicioDTO>> ListarTiposServicio()
        {
            try
            {
                var tiposServicios = await _tipoServicioRepositorio.Consultar();
                return _mapper.Map<List<TipoServicioDTO>>(tiposServicios);
            }
            catch
            {
                throw;
            }
        }

        public async Task<TipoServicioDTO> CrearTipoServicio(TipoServicioDTO tipoServicio)
        {
            try
            {
                var tipoServicioCreado = await _tipoServicioRepositorio.Crear(_mapper.Map<TipoServicio>(tipoServicio));
                return _mapper.Map<TipoServicioDTO>(tipoServicioCreado);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> EditarTipoServicio(TipoServicioDTO tipoServicio)
        {
            try
            {
                var tipoServicioencontrado = await _tipoServicioRepositorio.Obtener(t => t.IdTipoServicio == tipoServicio.IdTipoServicio);
                if (tipoServicioencontrado == null)
                {
                    throw new TaskCanceledException("Usuario no encontrado");
                }
                tipoServicioencontrado.Nombre = tipoServicio.Nombre;
                tipoServicioencontrado.Descripcion = tipoServicio.Descripcion;
                tipoServicioencontrado.Formula = tipoServicio.Formula;
                tipoServicioencontrado.UpdateAt = DateTime.Now;
                var tipoServicioActualizado = await _tipoServicioRepositorio.Editar(tipoServicioencontrado);
                return tipoServicioActualizado;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> EliminarTipoServicio(TipoServicioDTO tipoServicio)
        {
            try
            {
                var tipoServicioencontrado = await _tipoServicioRepositorio.Obtener(t => t.IdTipoServicio == tipoServicio.IdTipoServicio);
                if (tipoServicioencontrado == null)
                {
                    throw new TaskCanceledException("Usuario no encontrado");
                }
                var tipoServicioeliminado = await _tipoServicioRepositorio.Eliminar(tipoServicioencontrado);
                return tipoServicioeliminado;

            }
            catch
            {
                throw;
            }
        }

        public Task<TipoServicioDTO> ListarTipoServicio()
        {
            throw new NotImplementedException();
        }
    }
}
