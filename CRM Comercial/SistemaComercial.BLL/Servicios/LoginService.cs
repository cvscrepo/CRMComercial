using SistemaComercial.BLL.Servicios.Contrato;
using SistemaComercial.DTO;
using SistemaComercial.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.BLL.Servicios
{
    public class LoginService : ILoginService
    {
        private readonly IUsurioService _usuarioService;
        private readonly string hash = "cvsccotizador";
        public LoginService(IUsurioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<UsuarioDTO> Login(LoginDTO user)
        {
            try
            {
                List<UsuarioDTO> usuarios = await _usuarioService.ListarUsuarios();
                IEnumerable<UsuarioDTO> usuariosEncontrados = usuarios.Where(usuario => usuario.Email == user.Email);
                foreach (UsuarioDTO usuario in usuariosEncontrados)
                {
                    try
                    {
                        var contrasenaDesncrypt = this.Decrypy(usuario.Contrasena);
                        if (user.Contrasena == contrasenaDesncrypt) return usuario;

                    }
                    catch
                    {
                        continue;
                    }
                }
                throw new TaskCanceledException("Invalid Credentials");
            }
            catch
            {
                throw;
            }
        }

        public async Task<UsuarioDTO> Register(UsuarioDTO usuario)
        {
            try
            {
                usuario.Contrasena = this.Encrypt(usuario.Contrasena);
                var usuarioCreado = await _usuarioService.CrearUsuario(usuario);
                return usuarioCreado;
            }
            catch
            {
                throw;
            }
        }


        public string Encrypt(string mensaje)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(mensaje);

            MD5 md5 = MD5.Create();
            TripleDES tripleDes = TripleDES.Create();

            tripleDes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripleDes.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripleDes.CreateEncryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return Convert.ToBase64String(result);

        }
        
        public string Decrypy (string mensajeEncriptado)
        {
            byte[] data = Convert.FromBase64String(mensajeEncriptado);

            MD5 md5 = MD5.Create();
            TripleDES tripleDes = TripleDES.Create();

            tripleDes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripleDes.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripleDes.CreateDecryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return UTF8Encoding.UTF8.GetString(result);
        }
    }
}
