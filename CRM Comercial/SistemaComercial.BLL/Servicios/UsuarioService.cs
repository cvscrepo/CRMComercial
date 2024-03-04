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
        private readonly IRolService _rolService;
        private readonly IGenericRepository<Usuario> _usuarioRepositorio;
        private readonly IMapper _mapper;

        public UsuarioService( IMapper mapper, IGenericRepository<Usuario> usuarioRepositorio, IRolService roleService)
        {
            _rolService = roleService;
            _usuarioRepositorio = usuarioRepositorio;
            _mapper = mapper;
        }

        public async Task<List<UsuarioDTO>> ListarUsuarios()
        {
            try
            {
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
                var listarUsuariosBDc = await _usuarioRepositorio.Consultar((u) => u.IdUsuario == id );
                Usuario devolverUsuario = listarUsuariosBDc.Include(rol => rol.IdRolNavigation).First();
                return _mapper.Map<UsuarioDTO>(devolverUsuario);
            }
            catch
            {
                throw;
            }
        }

        public async Task<UsuarioDTO> CrearUsuario(UsuarioDTO usuario)
        {
            try
            {
                // validaciones 
                RolDTO validarRol = await _rolService.ListRole(usuario.IdRol);
                if (validarRol == null) throw new TaskCanceledException("Rol no encontrado");
                if (usuario.EsActivo == 0) usuario.EsActivo = 1;
                var usuarioMapeado = _mapper.Map<Usuario>(usuario);
                usuario.LastConnection = DateTime.Now;

                // Crear usuario
                var usuarioCreado = await _usuarioRepositorio.Crear(usuarioMapeado);
                if(usuarioCreado.IdUsuario == 0)throw new TaskCanceledException("No se pudo crear el usuario");
                
                //Retornar usuario
                var query = await _usuarioRepositorio.Consultar(u => u.IdUsuario == usuarioCreado.IdUsuario);
                usuarioCreado = query.Include(rol => rol.IdRolNavigation).FirstOrDefault();


                return _mapper.Map<UsuarioDTO>(usuarioCreado);
            }
            catch
            {
                throw;
            }
        }

        public async Task<UsuarioDTO> EditarUsuario(UsuarioDTO usuario)
        {
            try
            {

                var usuarioModelo = _mapper.Map<Usuario>(usuario);
                var usuarioEncontrado = await _usuarioRepositorio.Obtener(u => u.IdUsuario == usuarioModelo.IdUsuario);
                if(usuarioEncontrado == null)
                {
                    throw new TaskCanceledException("El usuario no existe");
                }

                usuarioEncontrado.NombreCompleto = usuarioModelo.NombreCompleto;
                usuarioEncontrado.Email = usuarioModelo.Email;
                usuarioEncontrado.IdRol = usuarioModelo.IdRol;
                usuarioEncontrado.Contrasena = usuarioModelo.Contrasena;
                usuarioEncontrado.EsActivo = usuarioModelo.EsActivo;
                usuarioEncontrado.UptadedAt = DateTime.Now;

                bool respuesta = await _usuarioRepositorio.Editar(usuarioEncontrado);
                if (!respuesta)
                {
                    throw new TaskCanceledException("El usuario no se pudo editar");
                }

                return _mapper.Map<UsuarioDTO>(usuarioEncontrado);             
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> EliminarUsuario(int id)
        {
            try
            {
                var usuarioEncontrado = await _usuarioRepositorio.Obtener((u)=> u.IdUsuario == id);
                if (usuarioEncontrado == null)
                {
                    throw new TaskCanceledException("Usuario a eliminar no encontrado");
                }
                var respuesta = await _usuarioRepositorio.Eliminar(usuarioEncontrado);
                
                return respuesta;
            }
            catch
            {
                throw;
            }
        }
    }
}
