using AutoMapper;
using Microsoft.Data.SqlClient;
using SistemaComercial.DAL.DBDatos.Contrato;
using SistemaComercial.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.BLL.Servicios.Contrato
{
    public interface IUsurioService
    {

        public Task<List<UsuarioDTO>> ListarUsuarios();

        public Task<UsuarioDTO> ListarUsuario(int id);

        public UsuarioDTO CrearUsuario(UsuarioDTO usuario);

        public UsuarioDTO EditarUsuario(UsuarioDTO usuario);

        public bool EliminarUsuario(int id);
    }
}
