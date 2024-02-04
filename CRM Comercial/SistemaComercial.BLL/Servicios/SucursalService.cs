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
    public class SucursalService : ISucursalService
    {
        private readonly IGenericRepository<Sucursal> _sucursalRepository;
        private readonly IMapper _mapper;

        public SucursalService(IGenericRepository<Sucursal> sucursalRepository, IMapper mapper)
        {
            _sucursalRepository = sucursalRepository;
            _mapper = mapper;
        }

        public async Task<List<SucursalDTO>> ListarSucursales()
        {
            try
            {
                var listarSucursales = await _sucursalRepository.Consultar();
                return _mapper.Map<List<SucursalDTO>>(listarSucursales);
            }
            catch
            {
                throw;
            }
        }

        public async Task<SucursalDTO> ListarSucursal(int id)
        {
            try
            {
                var sucursalEncontrada = await _sucursalRepository.Obtener((s) => s.IdSucursal == id);
                if(sucursalEncontrada is null)
                {
                    throw new TaskCanceledException("Sucursal no encontrada");
                }
                return _mapper.Map<SucursalDTO>(sucursalEncontrada);
            }
            catch
            {
                throw;
            }
        }

        public async Task<SucursalDTO> CrearSucursal(SucursalDTO sucursal)
        {
            try
            {
                var sucursalCreada = await _sucursalRepository.Crear(_mapper.Map<Sucursal>(sucursal));
                if(sucursalCreada is null)
                {
                    throw new TaskCanceledException("La sucursal no pudo ser creada");
                }
                return _mapper.Map<SucursalDTO>(sucursalCreada);
            }
            catch
            {
                throw;
            }
        }

        public async Task<SucursalDTO> EditarSucursal(SucursalDTO sucursal)
        {
            try
            {
                var sucursalEncontrada = await _sucursalRepository.Obtener(s => s.IdSucursal == sucursal.IdSucursal) ?? throw new TaskCanceledException("Sucursal no encontrada");
                sucursalEncontrada.IdCliente = sucursal.IdCliente;
                sucursalEncontrada.Nombre = sucursal.Nombre;
                sucursalEncontrada.Direccion = sucursal.Direccion;
                sucursalEncontrada.UpdatedAt = DateTime.Now;
                var clienteEditado = await _sucursalRepository.Editar(sucursalEncontrada);
                return clienteEditado ? _mapper.Map<SucursalDTO>(sucursalEncontrada) : throw new TaskCanceledException("No se pudo editar la sucursal");
            }
            catch
            {
                throw;
            }
        }

        public async Task<SucursalDTO> EliminarSucursal(int id)
        {
            try
            {
                var sucursalEncontrada = await _sucursalRepository.Obtener(s => s.IdSucursal == id) ?? throw new TaskCanceledException("Sucursal no encontrada");
                var sucursalEliminada = await _sucursalRepository.Eliminar(sucursalEncontrada);
                return sucursalEliminada ? _mapper.Map<SucursalDTO>(sucursalEncontrada) : throw new TaskCanceledException("No se pudo eliminar la sucursal");
            }
            catch
            {
                throw;
            }
        }
    }
}
