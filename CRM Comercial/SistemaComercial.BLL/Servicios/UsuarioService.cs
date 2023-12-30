using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SistemaComercial.BLL.Servicios.Contrato;
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
    public class UsuarioService : IUsurioService
    {
        private readonly IDBDatos<Usuario> _DBDatos;
        private readonly IGenericRepository<Usuario> _usuarioRepositorio;
        private readonly IMapper _mapper;

        public UsuarioService(IDBDatos<Usuario> dBDatos, IMapper mapper, IGenericRepository<Usuario> usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _DBDatos = dBDatos;
            _mapper = mapper;
        }

        public async Task<List<UsuarioDTO>> ListarUsuarios()
        {
            try
            {
                //var listaUsuarios = _DBDatos.ListarTablas("SelectUsers");
                var listarUsuariosBDc = await _usuarioRepositorio.Consultar();
                var lista = listarUsuariosBDc.Include("IdRolNavigation").AsEnumerable().ToList();
                
                return _mapper.Map<List<UsuarioDTO>>(lista);
            }
            catch
            {
                throw;
            }
        }

        public async Task<UsuarioDTO> ListarUsuario(int id)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>
                {
                    new SqlParameter("@id", id)
                };
                //var listarUsuario = _DBDatos.Listar("SelectUsers @id", parametros);
                var listarUsuariosBDc = await _usuarioRepositorio.Consultar((u) => u.IdUsuario == id );
                Usuario devolverUsuario = listarUsuariosBDc.Include(rol => rol.IdRolNavigation).First();
                return _mapper.Map<UsuarioDTO>(devolverUsuario);
            }
            catch
            {
                throw;
            }
        }

        public UsuarioDTO CrearUsuario(UsuarioDTO usuario)
        {
            try
            {
                //CreateUser @idUsuario, @nombre, @email, @contresana, @urlFoto, @esActivo
                SqlParameter[] parametros =
                {
                    new SqlParameter("@idRol", SqlDbType.Int) {Value = usuario.IdRol},
                    new SqlParameter("@usuarioRegistro", SqlDbType.Int) {Value = usuario.UsuarioRegistro },
                    new SqlParameter("@nombre", SqlDbType.NVarChar) {Value = usuario.NombreCompleto},
                    new SqlParameter("@email", SqlDbType.NVarChar) {Value = usuario.Email},
                    new SqlParameter("@contrasena", SqlDbType.NVarChar) {Value = usuario.Contrasena},
                    new SqlParameter("@urlFoto", SqlDbType.NVarChar) {Value = usuario.UrlFoto},
                    new SqlParameter("@esActivo", SqlDbType.Int) {Value = usuario.EsActivo},
                };

                var usuarioCreado = _DBDatos.Ejecutar("CreateUser @idRol, @usuarioRegistro, @nombre, @email, @contrasena, @urlFoto, @esActivo", parametros);
                return _mapper.Map<UsuarioDTO>(usuarioCreado);
            }
            catch
            {
                throw;
            }
        }

        public UsuarioDTO EditarUsuario(UsuarioDTO usuario)
        {
            try
            {
                SqlParameter[] parametros =
                {
                    new SqlParameter("@idUsuario", SqlDbType.Int) {Value = usuario.IdUsuario},
                    new SqlParameter("@nombre", SqlDbType.Int) {Value = usuario.NombreCompleto},
                    new SqlParameter("@email", SqlDbType.NVarChar) {Value = usuario.Email},
                    new SqlParameter("@contresana", SqlDbType.NVarChar) {Value = usuario.Contrasena},
                    new SqlParameter("@urlFoto", SqlDbType.NVarChar) {Value = usuario.UrlFoto},
                    new SqlParameter("@esActivo", SqlDbType.Int) {Value = usuario.EsActivo},
                };

                var usuarioEditado = _DBDatos.Ejecutar("CreateUser @idUsuario, @nombre, @email, @contresana, @urlFoto, @esActivo", parametros);
                return _mapper.Map<UsuarioDTO>(usuarioEditado);             
            }
            catch
            {
                throw;
            }
        }

        public bool EliminarUsuario(int id)
        {
            try
            {
                SqlParameter[] parametros =
                {
                    new SqlParameter("@id", SqlDbType.Int) {Value = id}
                };

                var usuarioEliminado = _DBDatos.Ejecutar("DeleteUser @id", parametros);
                return usuarioEliminado == 1;

            }
            catch
            {
                throw;
            }
        }
    }
}
