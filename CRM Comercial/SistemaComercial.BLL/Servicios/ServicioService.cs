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
    public class ServicioService : IServicioService
    {
        private readonly IGenericRepository<Servicio> _servicioRepository;
        private readonly IMapper _mapper;

        public ServicioService(IGenericRepository<Servicio> servicioRepository, IMapper mapper)
        {
            _servicioRepository = servicioRepository;
            _mapper = mapper;
        }

        public async Task<List<ServicioDTO>> ListarServicios()
        {
            try
            {
                var ListaServiciosDB = await _servicioRepository.Consultar();
                var listaUsuarios = ListaServiciosDB.Include("TipoServicioNavigation").AsEnumerable().ToList();
                return _mapper.Map<List<ServicioDTO>>(listaUsuarios);
            }
            catch
            {
                throw;
            }
        }

        public async Task<ServicioDTO> ListarServicio(int id)
        {
            try
            {
                var listarServicioBD = await _servicioRepository.Consultar((service) => service.IdServicio == id) ?? throw new TaskCanceledException("No se encontro registros");
                var listarServicio = listarServicioBD.Include("TipoServicioNavigation").AsEnumerable().ToList();
                return _mapper.Map<ServicioDTO>(listarServicio);
            }
            catch
            {
                throw;
            }
        }

        public async Task<ServicioDTO> CrearServicio(ServicioDTO servicio)
        {
            try
            {
                var servicioCreado = await _servicioRepository.Crear(_mapper.Map<Servicio>(servicio));
                if (servicioCreado.IdServicio == 0)
                {
                    throw new TaskCanceledException("No se pudo crear el servicio");
                }
                var query = await _servicioRepository.Consultar((s) => s.IdServicio == servicio.IdServicio);
                return _mapper.Map<ServicioDTO>(query);
            }
            catch
            {
                throw;
            }
        }
        public async Task<ServicioDTO> EditarServicio(ServicioDTO servicio)
        {
            try
            {
                var servicioModelo = _mapper.Map<Servicio>(servicio);
                var servicioConsultado = await _servicioRepository.Obtener((s)=> s.IdServicio == servicio.IdServicio);
                if (servicioConsultado == null)
                {
                    throw new TaskCanceledException("Servicio no encontrado");
                }
                servicioConsultado.TipoServicio = servicio.TipoServicio;
                servicioConsultado.Nombre = servicio.Nombre;
                servicioConsultado.Descripcion = servicio.DescripcionCategoria;
                servicioConsultado.UpdatedAt = DateTime.Now;
                bool servicioEditado = await _servicioRepository.Editar(servicioConsultado);
                if (!servicioEditado)
                {
                    throw new TaskCanceledException("El servicio no se pudo editar");
                }
                return _mapper.Map<ServicioDTO>(servicioConsultado);

            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> EliminarServicio(int id)
        {
            try
            {
                var servicioConsultado = await _servicioRepository.Obtener((s) => s.IdServicio == id);
                if (servicioConsultado == null)
                {
                    throw new TaskCanceledException("Servicio a eliminar no encontrado");
                }
                var servicioElimado = await _servicioRepository.Eliminar(servicioConsultado);
                return servicioElimado;
            }
            catch (Exception ex) 
            {
                throw;
            }
        }
       
    }
}
