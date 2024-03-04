using SistemaComercial.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.BLL.Servicios.Contrato
{
    public interface ILoginService
    {
        public Task<UsuarioDTO> Login(LoginDTO user);
        public Task<UsuarioDTO> Register(UsuarioDTO usuario);
    }

}
