using AutoMapper;
using Microsoft.Data.SqlClient;
using SistemaComercial.BLL.Servicios.Contrato;
using SistemaComercial.DAL;
using SistemaComercial.DAL.DBDatos.Contrato;
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
        private readonly IMapper _mapper;

        public RoleService(IDBDatos<Rol> roldBDatos, IMapper mapper)
        {
            _roldBDatos = roldBDatos;
            _mapper = mapper;
        }

        public List<RolDTO> ListRoles()
        {
            try
            {
                var listarRoles = _roldBDatos.Listar("SelectRol");
                return _mapper.Map<List<RolDTO>>(listarRoles);
            }
            catch
            {
                throw;
            }
        }

        public List<RolDTO> ListRole(int id)
        {
            try
            {
                var parametros = new List<SqlParameter>
                {
                     new SqlParameter("@id", id)
                };
                var listarRol = _roldBDatos.ListarTablas("SelectRol @id", parametros);
                return _mapper.Map<List<RolDTO>>(listarRol);
            }
            catch
            {
                throw;
            }
        }
        public int CreateRol(RolDTO rol)
        {
            try
            {
                if (string.IsNullOrEmpty(rol.Nombre))
                {
                    throw new ArgumentException($"El nombre no puede ser nulo o vacío.", nameof(rol.Nombre));
                }
                SqlParameter[] parametrosCrear =
                {
                     new SqlParameter("@name", SqlDbType.NVarChar) {Value = rol.Nombre}
                };
                var rolCreado = _roldBDatos.Ejecutar("CreateRole @name", parametrosCrear);
                return rolCreado;
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateRole(RolDTO rol)
        {
            try
            {
                SqlParameter[] parametros =
                {
                     new SqlParameter("@id", SqlDbType.Int) { Value = rol.IdRol },
                     new SqlParameter("@name", SqlDbType.NVarChar) { Value = rol.Nombre }
                };
                var rolEditado = _roldBDatos.Ejecutar("CreateRol @id, @name", parametros);
                return rolEditado == 1;
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteRole(int id )
        {
            try
            {
                SqlParameter[] parametros =
                {
                    new SqlParameter("id", SqlDbType.Int) { Value = id }

                };
                var rolEliminado = _roldBDatos.Ejecutar("DeleteRol @id", parametros);
                return rolEliminado == 1;

            }
            catch 
            {
                throw;
            }
        }
    }
}
