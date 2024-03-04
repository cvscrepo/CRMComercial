using AutoMapper;
using Microsoft.Data.SqlClient;
using SistemaComercial.BLL.Servicios.Contrato;
using SistemaComercial.DAL;
using SistemaComercial.DAL.DBDatos.Contrato;
using SistemaComercial.DAL.Repositorios.Contratos;
using SistemaComercial.DTO;
using SistemaComercial.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.BLL.Servicios
{
    public class RoleService : IRolService
    {
        private readonly IDBDatos<Rol> _roldBDatos;
        private readonly IGenericRepository<Rol> _rolRepository;
        private readonly IMapper _mapper;

        public RoleService(IGenericRepository<Rol> rolRepository, IDBDatos<Rol> roldBDatos, IMapper mapper)
        {
            _rolRepository = rolRepository;
            _roldBDatos = roldBDatos;
            _mapper = mapper;
        }

        public async Task<List<RolDTO>> ListRoles()
        {
            try
            {
                var listarRoles = await _rolRepository.Consultar();
                return _mapper.Map<List<RolDTO>>(listarRoles);
            }
            catch
            {
                throw;
            }
        }

        public async Task<RolDTO> ListRole(int id)
        {
            try
            {
                var listarRol = await _rolRepository.Obtener(rol => rol.IdRol == id);
                return _mapper.Map<RolDTO>(listarRol);
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> CreateRol(RolDTO rol)
        {
            try
            {
                
                if (string.IsNullOrEmpty(rol.Nombre))
                {
                    throw new ArgumentException($"El nombre no puede ser nulo o vacío.", nameof(rol.Nombre));
                }
                var rolCreado = await _rolRepository.Crear(_mapper.Map<Rol>(rol));
                return rolCreado != null ? 1 : 0;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> UpdateRole(RolDTO rol)
        {
            try
            {
                
                var rolEditado = await _rolRepository.Editar(_mapper.Map<Rol>(rol));
                return rolEditado != null;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteRole(int id )
        {
            try
            {
                var rol = await _rolRepository.Obtener(rol => rol.IdRol == id);
                var rolEliminado = await _rolRepository.Eliminar(rol);
                return rolEliminado != null;

            }
            catch 
            {
                throw;
            }
        }
    }
}
